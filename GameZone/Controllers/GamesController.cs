using GameZone.Data;
using GameZone.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GameZone.Controllers;

public class GamesController : Controller
{
    private readonly ICategoriesService _categoriesService;
    private readonly IDevicesService _devicesService;
    private readonly IGamesService _gamesService;

    public GamesController(ICategoriesService categoriesService,
                            IDevicesService devicesService,
                            IGamesService gamesService)
    {
        _categoriesService = categoriesService;
        _devicesService = devicesService;
        _gamesService = gamesService;
    }
    public async Task<IActionResult> Index()
    {
        var games = await _gamesService.GetAll();
        return View(games);
    }

    public async Task<IActionResult> Details(int id)
    {
        var game = await _gamesService.GetById(id);

        if (game == null)
            return NotFound();

        return View(game);
    }

    public IActionResult Create()
    {
        CreateGameFormViewModel viewModel = new()
        {
            Categories = _categoriesService.GetSelectListCategories(),
            Devices = _devicesService.GetSelectListDevices(),

        };
        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateGameFormViewModel model)
    {
        if (!ModelState.IsValid)
        {
            model.Devices = _devicesService.GetSelectListDevices();
            model.Categories = _categoriesService.GetSelectListCategories();

            return View(model);
        }

        await _gamesService.Create(model);

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Update(int id)
    {
        var game = await _gamesService.GetById(id);

        if (game == null)
            return NotFound();

        UpdateGameFormViewModel viewModel = new()
        {
            Id = game.Id,
            Name = game.Name,
            Description = game.Description,
            CategoryId = game.CategoryId,
            SelectedDevices = game.GameDevices.Select(d => d.DeviceId).ToList(),
            Categories = _categoriesService.GetSelectListCategories(),
            Devices = _devicesService.GetSelectListDevices(),
            CurrentCover = game.Cover
        };

        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(UpdateGameFormViewModel model)
    {
        if (!ModelState.IsValid)
        {
            model.Devices = _devicesService.GetSelectListDevices();
            model.Categories = _categoriesService.GetSelectListCategories();

            return View(model);
        }

        var game = await _gamesService.Update(model);

        if (game == null)
            return BadRequest();

        return RedirectToAction(nameof(Index));
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(int id)
    {
        bool isDeleted = await _gamesService.Delete(id);

        if (!isDeleted)
            return BadRequest();
        return Ok();
    }
}