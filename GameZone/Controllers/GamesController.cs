using GameZone.Data;
using GameZone.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GameZone.Controllers;

public class GamesController : Controller
{
    private readonly AppDbContext _Context;

    public GamesController(AppDbContext context)
    {
        _Context = context;
    }
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Create()
    {
        CreateGameFormViewModel viewModel = new()
        {
            Categories = _Context.Categories
                                    .Select(c => new SelectListItem
                                    {
                                        Value = c.Id.ToString(),
                                        Text = c.Name
                                    })
                                    .OrderBy(c => c.Text)
                                    .ToList(),
            Devices = _Context.Devices
                                .Select(d => new SelectListItem
                                {
                                    Value = d.Id.ToString(),
                                    Text = d.Name
                                })
                                .OrderBy(d => d.Text)
                                .ToList()

        };
        return View(viewModel);
    }
}