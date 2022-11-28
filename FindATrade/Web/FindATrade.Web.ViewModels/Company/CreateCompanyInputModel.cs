namespace FindATrade.Web.ViewModels.Company
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;

    public class CreateCompanyInputModel
    {
        [Required]
        [StringLength(100, MinimumLength =3)]
        public string Name { get; set; }

        [StringLength(50, MinimumLength = 6)]
        public string Website { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 6)]
        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        [Required]
        [StringLength(15, MinimumLength = 7)]
        [Display(Name = "Company Phone")]
        public string PhoneNumber { get; set; }

        [Required]
        [Display(Name = "Company Description")]
        public string Description { get; set; }

        [Display(Name = "Company Logo")]
        public IFormFile Image { get; set; }

        public CreateCompanyAddressInputModel Address { get; set; }

        public IEnumerable<SkillModel> Skills { get; set; }
    }
}
