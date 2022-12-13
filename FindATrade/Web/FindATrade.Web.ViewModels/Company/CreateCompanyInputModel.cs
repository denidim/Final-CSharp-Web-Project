namespace FindATrade.Web.ViewModels.Company
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using FindATrade.Common;
    using FindATrade.Common.CustomAttributes;
    using Microsoft.AspNetCore.Http;

    public class CreateCompanyInputModel
    {
        [Required]
        [StringLength(CompanyConstants.NameMax, MinimumLength = CompanyConstants.NameMin, ErrorMessage = CompanyConstants.NameMessage)]
        public string Name { get; set; }

        [StringLength(CompanyConstants.WebsiteMax, MinimumLength = CompanyConstants.WebsiteMin, ErrorMessage = CompanyConstants.WebsiteMessage)]
        public string Website { get; set; }

        [Required]
        [StringLength(CompanyConstants.EmailMax, MinimumLength = CompanyConstants.EmailMax, ErrorMessage = CompanyConstants.EmailMessage)]
        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        [Required]
        [StringLength(CompanyConstants.PhoneNumberMax, MinimumLength = CompanyConstants.PhoneNumberMin, ErrorMessage = CompanyConstants.PhoneNumberMessage)]
        [Display(Name = CompanyConstants.PhoneNumberName)]
        public string PhoneNumber { get; set; }

        [Required]
        [MinLength(CompanyConstants.DescriptionMin, ErrorMessage = CompanyConstants.DescriptionMessage)]
        [Display(Name = CompanyConstants.DescriptionName)]
        public string Description { get; set; }

        [MaxFileSize(1 * 1024 * 1024)]
        [PermittedExtensions(new string[] {".jpg", ".png", ".gif", ".jpeg" })]
        public virtual IFormFile Image { get; set; }

        public CreateCompanyAddressInputModel Address { get; set; }

        public List<SkillModel> Skills { get; set; }
    }
}
