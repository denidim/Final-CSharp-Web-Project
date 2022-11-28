namespace FindATrade.Web.ViewModels.CompanyService
{
    using Microsoft.AspNetCore.Http;
    using System.Collections.Generic;

    public class CompanyServiceOutputModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public bool IsPremium { get; set; }

        public string Description { get; set; }

        public string CategoryName { get; set; }

        public PaidOrderOutputModel PaidOrder { get; set; }

        public VettingOutputModel Vetting { get; set; }

        public ICollection<PackageModel> Packages { get; set; }

        public ICollection<IFormFile> Images { get; set; }
    }
}