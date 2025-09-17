using GameZone.Attributes;

namespace GameZone.ViewModels;

public class UpdateGameFormViewModel : GameFormViewModel
{
    public int Id { get; set; }
    [AllowedExtensions]
    [MaxFileSize]
    public IFormFile? Cover { get; set; } = default!;
}