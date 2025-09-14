using GameZone.Data;
using GameZone.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GameZone.Controllers;

public class GamesController : Controller
{
    private readonly ICategoriesService _categoriesService;
    private readonly IDevicesService _devicesService;


    public GamesController(ICategoriesService categoriesService, IDevicesService devicesService)
    {
        _categoriesService = categoriesService;
        _devicesService = devicesService;
    }
    public IActionResult Index()
    {
        return View();
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
    public IActionResult Create(CreateGameFormViewModel model)
    {
        if (!ModelState.IsValid)
        {
            model.Devices = _devicesService.GetSelectListDevices();
            model.Categories = _categoriesService.GetSelectListCategories();

            return View(model);
        }
        return View();
    }
}