namespace FindATrade.Web.ViewModels.CompanyService
{
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Http;

    public class AddImages
    {
        public IEnumerable<IFormFile> Images { get; set; }
    }
}
