using Microsoft.Extensions.Options;

namespace GameZone.Attributes;

public class MaxFileSizeAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var file = value as IFormFile;

        if (file != null)
        {
            var options = validationContext
                        .GetRequiredService<IOptions<FileConfiguration>>();
            var maxFileSize = options.Value.MaxFileSizeInBytes;
            if (file == null)
            {
                return new ValidationResult("The file is empty");
            }
            if (file.Length > maxFileSize)
            {
                return new ValidationResult($"Maximum allowed size is {options.Value.MaxFileSizeInMB}MB");
            }
        }

        return ValidationResult.Success;
    }
}