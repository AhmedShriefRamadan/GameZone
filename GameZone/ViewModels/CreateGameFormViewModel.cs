using Microsoft.AspNetCore.Mvc.Rendering;

namespace GameZone.ViewModels;

public class CreateGameFormViewModel
{
    [MaxLength(255)]
    public string Name { get; set; } = default!;
    [Display(Name = "Category")]
    public int CategoryId { get; set; }
    public IEnumerable<SelectListItem> Categories { get; set; } = default!;
    [Display(Name = "Supported Devices")]
    public List<int> SelectedDevices { get; set; } = default!;
    public IEnumerable<SelectListItem> Devices { get; set; } = default!;
    [MaxLength(2555)]
    public string Description { get; set; } = default!;
    public IFormFile Cover { get; set; } = default!;
}