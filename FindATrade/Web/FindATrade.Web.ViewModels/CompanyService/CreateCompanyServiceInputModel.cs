namespace FindATrade.Web.ViewModels.CompanyService
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

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
        [StringLength(100, MinimumLength = 3, ErrorMessage = "{0} must be between {2} and {1} characters")]
        public string Title { get; set; }

        public bool IsPremium { get; set; }

        [Required]
        [MinLength(20, ErrorMessage = "{0} must be between at least {1} characters")]
        public string Description { get; set; }

        public int CategoryId { get; set; }

        public IEnumerable<Category> Categories { get; set; }

        public IEnumerable<IFormFile> Images { get; set; }

        public List<PackageModel> Packages { get; set; }
    }
}
