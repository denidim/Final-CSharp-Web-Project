namespace FindATrade.Web.ViewModels.CompanyService
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using FindATrade.Common;
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

        public IEnumerable<IFormFile> Images { get; set; }

        public List<PackageModel> Packages { get; set; }
    }
}
