using System.ComponentModel.DataAnnotations;

namespace WebBanHang.Repository.Validation
{
    public class FileExtensionAtrribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is IFormFile file)
            {
                var extension = Path.GetExtension(file.FileName);
                string[] extensions = { "jpg", "png", "jpeg" };

                bool result = extensions.Any(e => extension.EndsWith(e));

                if (!result)
                {
                    return new ValidationResult("Allowed extensions are jpg or png or jpeg");
                }

            }
            return ValidationResult.Success;
        }
    }
}