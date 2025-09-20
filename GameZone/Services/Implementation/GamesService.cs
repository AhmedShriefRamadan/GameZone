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
        var coverName = await SaveCover(model.Cover);

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

    public async Task<Game?> Update(UpdateGameFormViewModel model)
    {
        var game = await _context.Games
                                    .Include(g => g.GameDevices)
                                    .SingleOrDefaultAsync(g => g.Id == model.Id);

        if (game == null)
            return null;

        bool hasNewCover = model.Cover != null;
        var oldCover = game.Cover;
        string newCover = string.Empty;

        game.Name = model.Name;
        game.Description = model.Description;
        game.CategoryId = model.CategoryId!.Value;
        game.GameDevices = model
                            .SelectedDevices
                            .Select(d => new GameDevice { DeviceId = d }).ToList();

        if (hasNewCover)
        {
            newCover = await SaveCover(model.Cover!);
            game.Cover = newCover;
        }

        var effectedRows = await _context.SaveChangesAsync();
        if (effectedRows > 0)
        {
            if (hasNewCover)
            {
                var cover = Path.Combine(_imagesPath, oldCover);
                File.Delete(cover);
            }

            return game;
        }
        else
        {
            if (newCover != string.Empty)
            {
                var cover = Path.Combine(_imagesPath, newCover);
                File.Delete(cover);
            }
            return null;
        }
    }

    public async Task<bool> Delete(int id)
    {
        bool isDeleted = false;

        var game = await _context.Games.SingleOrDefaultAsync(g => g.Id == id);
        if (game == null)
            return isDeleted;

        _context.Remove(game);
        int effectedRows = await _context.SaveChangesAsync();
        if (effectedRows > 0)
        {
            isDeleted = true;
            var cover = Path.Combine(_imagesPath, game.Cover);
            File.Delete(cover);
        }

        return isDeleted;
    }

    private async Task<string> SaveCover(IFormFile cover)
    {
        var coverName = $"{Guid.NewGuid()}{Path.GetExtension(cover.FileName)}";

        var path = Path.Combine(_imagesPath, coverName);

        using var stream = File.Create(path);
        await cover.CopyToAsync(stream);
        // We dont need it because we use "using"
        // stream.Dispose();

        return coverName;
    }
}