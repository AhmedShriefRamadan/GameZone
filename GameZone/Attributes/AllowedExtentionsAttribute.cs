using Microsoft.Extensions.Options;

namespace GameZone.Attributes;

public class AllowedExtensionsAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var file = value as IFormFile;

        if (file != null)
        {// Resolve FileConfiguration from DI
            var options = (IOptions<FileConfiguration>)validationContext
                                    .GetRequiredService(typeof(IOptions<FileConfiguration>));
            var allowedExtensions = options.Value.AllowedExtensions;

            var extension = Path.GetExtension(file.FileName);

            var isAllowed = allowedExtensions.Contains(extension, StringComparer.OrdinalIgnoreCase);
            if (!isAllowed)
            {
                return new ValidationResult($"Only {string.Join(", ", allowedExtensions)} files are allowed.");
            }
        }

        return ValidationResult.Success;
    }
}