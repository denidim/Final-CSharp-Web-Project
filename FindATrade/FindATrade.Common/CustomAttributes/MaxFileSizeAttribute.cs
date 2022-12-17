namespace FindATrade.Common.CustomAttributes
{
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;

    public sealed class MaxFileSizeAttribute : ValidationAttribute
    {
        private readonly int maxFileSize;

        public MaxFileSizeAttribute(int maxFileSize)
        {
            this.maxFileSize = maxFileSize;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            IFormFile file = value as IFormFile;

            if (file != null)
            {
                if (file.Length > this.maxFileSize)
                {
                    return new ValidationResult(string.Format(ValidationAttributesConstants.MaxSizeAttributeMessage, this.maxFileSize));
                }
            }

            return ValidationResult.Success;
        }
    }
}
