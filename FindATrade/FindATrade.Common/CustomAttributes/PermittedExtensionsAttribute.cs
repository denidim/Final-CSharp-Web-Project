namespace FindATrade.Common.CustomAttributes
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.IO;
    using System.Linq;

    using Microsoft.AspNetCore.Http;

    public sealed class PermittedExtensionsAttribute : ValidationAttribute
    {
        private readonly string[] permittedExtensions;

        public PermittedExtensionsAttribute(string[] permittedExtensions)
        {
            this.permittedExtensions = permittedExtensions;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            IFormFile file = value as IFormFile;

            if (!(file == null))
            {
                var extension = Path.GetExtension(file.FileName);

                if (!this.permittedExtensions.Contains(extension.ToLower()))
                {
                    return new ValidationResult(ValidationAttributesConstants.PermittedExtensionsMessage);
                }
            }

            return ValidationResult.Success;
        }
    }
}
