using GameZone.Models;
using GameZone.ViewModels;
using Microsoft.Extensions.Options;

namespace GameZone.Services.Implementation;

public class GamesService : IGamesService
{
    private readonly AppDbContext _context;
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly string _imagesPath;
    private readonly FileConfiguration _fileConfiguration;

    public GamesService(AppDbContext context,
                        IWebHostEnvironment webHostEnvironment,
                        IOptions<FileConfiguration> fileConfiguration)
    {
        _context = context;
        _webHostEnvironment = webHostEnvironment;
        _fileConfiguration = fileConfiguration.Value;
        _imagesPath = $"{_webHostEnvironment.WebRootPath}{_fileConfiguration.ImagePath}";
    }
    public async Task Create(CreateGameFormViewModel model)
    {
        var coverName = $"{Guid.NewGuid()}{Path.GetExtension(model.Cover.FileName)}";

        var path = Path.Combine(_imagesPath, coverName);

        using var stream = File.Create(path);
        await model.Cover.CopyToAsync(stream);
        // We dont need it because we use "using"
        // stream.Dispose();

        Game game = new()
        {
            Name = model.Name,
            Description = model.Description,
            CategoryId = model.CategoryId!.Value,
            Cover = coverName,
            GameDevices = [.. model.SelectedDevices.Select(d => new GameDevice { DeviceId = d })],
        };

        await _context.AddAsync(game);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Game>> GetAll()
    {
        return await _context.Games
                    .Include(g => g.Category)
                    .Include(g => g.GameDevices)
                    .ThenInclude(d => d.Device)
                    .AsNoTracking()
                    .ToListAsync();
    }

    public async Task<Game?> GetById(int id)
    {
        return await _context.Games
                    .Include(g => g.Category)
                    .Include(g => g.GameDevices)
                    .ThenInclude(d => d.Device)
                    .AsNoTracking()
                    .SingleOrDefaultAsync(g => g.Id == id);
    }
}