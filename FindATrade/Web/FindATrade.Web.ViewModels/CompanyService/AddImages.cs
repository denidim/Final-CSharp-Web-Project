namespace FindATrade.Web.ViewModels.CompanyService
{
    using System.Collections.Generic;

    using FindATrade.Common.CustomAttributes;
    using Microsoft.AspNetCore.Http;

    public class AddImages
    {
        [MaxFileSize(1 * 1024 * 1024)]
        [PermittedExtensions(new string[] { ".jpg", ".png", ".gif", ".jpeg" })]
        public ICollection<IFormFile> Images { get; set; }
    }
}
