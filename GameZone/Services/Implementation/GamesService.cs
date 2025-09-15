using GameZone.Models;
using GameZone.ViewModels;

namespace GameZone.Services.Implementation;

public class GamesService : IGamesService
{
    private readonly AppDbContext _context;
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly string _imagesPath;

    public GamesService(AppDbContext context, IWebHostEnvironment webHostEnvironment)
    {
        _context = context;
        _webHostEnvironment = webHostEnvironment;
        _imagesPath = $"{_webHostEnvironment.WebRootPath}/assets/images/games";
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
}