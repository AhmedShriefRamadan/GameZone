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
}