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

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        public bool IsPremium { get; set; }

        [Required]
        public string Description { get; set; }

        public int CategoryId { get; set; }

        public IEnumerable<Category> Categories { get; set; }

        public IEnumerable<IFormFile> Images { get; set; }

        public IEnumerable<CreatePackageInputModel> Packages { get; set; }
    }
}
