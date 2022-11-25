namespace FindATrade.Web.ViewModels.UserAccount
{
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Http;

    public class UserCompanyServices
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public bool IsPremium { get; set; }

        public string Description { get; set; }

        public string CategoryName { get; set; }

        public PaidOrderOutputModel PaidOrder { get; set; }

        public VettingOutputModel Vetting { get; set; }

        public ICollection<PackageOutputModel> Packages { get; set; }

        public ICollection<IFormFile> Images { get; set; }
    }
}