namespace FindATrade.Web.ViewModels.Company
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;

    public class CreateCompanyInputModel
    {
        [Required]
        [StringLength(100, MinimumLength =3, ErrorMessage = "{0} must be between {2} and {1} characters.")]
        public string Name { get; set; }

        [StringLength(50, MinimumLength = 6, ErrorMessage = "{0} must be between {2} and {1} characters.")]
        public string Website { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "{0} must be between {2} and {1} characters.")]
        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        [Required]
        [StringLength(15, MinimumLength = 7, ErrorMessage = "{0} must be between {2} and {1} characters.")]
        [Display(Name = "Company Phone")]
        public string PhoneNumber { get; set; }

        [Required]
        [MinLength(50, ErrorMessage = "{0} must be at least {1} characters.")]
        [Display(Name = "Company Description")]
        public string Description { get; set; }

        [Display(Name = "Image File")]
        public virtual IFormFile Image { get; set; }

        public CreateCompanyAddressInputModel Address { get; set; }

        public List<SkillModel> Skills { get; set; }
    }
}
