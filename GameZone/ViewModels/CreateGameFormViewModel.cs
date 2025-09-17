using GameZone.Attributes;

namespace GameZone.ViewModels;

public class CreateGameFormViewModel : GameFormViewModel
{

    [AllowedExtensions]
    [MaxFileSize]
    public IFormFile Cover { get; set; } = default!;
}