using Microsoft.Extensions.Options;

namespace GameZone.Configurations;

public class FileConfiguration
{
    // In C#, the configuration binder (Bind) only works with public properties with get; set;.
    [Required]
    [MinLength(1)]
    public string ImagePath { get; set; } = string.Empty;
    [Required]
    [MinLength(1)]
    public List<string> AllowedExtensions { get; set; } = [];
    [Range(1, int.MaxValue)]
    public int MaxFileSizeInMB { get; set; }
    public int MaxFileSizeInBytes => MaxFileSizeInMB * 1024 * 1024;
}