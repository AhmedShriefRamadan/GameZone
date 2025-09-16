using Microsoft.Extensions.Options;

namespace GameZone.Configurations;

public class FileConfiguration
{
    [Required]
    [MinLength(1)]
    public string ImagePath = string.Empty;
    [Required]
    [MinLength(1)]
    public List<string> AllowedExtensions = [];
    [Range(1, int.MaxValue)]
    public int MaxFileSizeInMB { get; set; }
    public int MaxFileSizeInBytes { get; set; }

    public FileConfiguration()
    {
        MaxFileSizeInBytes = MaxFileSizeInMB * 1024;
    }
}