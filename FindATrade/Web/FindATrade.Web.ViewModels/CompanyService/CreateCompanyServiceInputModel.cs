namespace FindATrade.Web.ViewModels.CompanyService
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using FindATrade.Common;
    using FindATrade.Common.CustomAttributes;
    using FindATrade.Data.Models;
    using Microsoft.AspNetCore.Http;

    public class CreateCompanyServiceInputModel
    {
        public CreateCompanyServiceInputModel()
        {
            this.Categories = new List<Category>();
        }

        public int CompanyId { get; set; }

        [Required]
        [StringLength(ServiceConstants.TitleMax, MinimumLength = ServiceConstants.TitleMin, ErrorMessage = ServiceConstants.TitleMessage)]
        public string Title { get; set; }

        public bool IsPremium { get; set; }

        [Required]
        [MinLength(ServiceConstants.DescriptionMin, ErrorMessage = ServiceConstants.DescriptionMessage)]
        public string Description { get; set; }

        public int CategoryId { get; set; }

        public IEnumerable<Category> Categories { get; set; }

        [MaxFileSize(1 * 1024 * 1024)]
        [PermittedExtensions(new string[] { ".jpg", ".png", ".gif", ".jpeg" })]
        public IEnumerable<IFormFile> Images { get; set; }

        public List<PackageModel> Packages { get; set; }
    }
}
