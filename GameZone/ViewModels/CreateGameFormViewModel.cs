using GameZone.Attributes;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GameZone.ViewModels;

public class CreateGameFormViewModel()
{
    [MaxLength(255)]
    public string Name { get; set; } = default!;
    [Display(Name = "Category")]
    // Make int optional and add [Required] so if the value is empty it will be null rather than 0 the default of empty for int and then it works correctly in client-side validation
    [Required]
    public int? CategoryId { get; set; }
    public IEnumerable<SelectListItem>? Categories { get; set; } = default!;
    [Display(Name = "Supported Devices")]
    public List<int> SelectedDevices { get; set; } = default!;
    public IEnumerable<SelectListItem>? Devices { get; set; } = default!;
    [MaxLength(2555)]
    public string Description { get; set; } = default!;
    [AllowedExtensions]
    [MaxFileSize]
    public IFormFile Cover { get; set; } = default!;
}