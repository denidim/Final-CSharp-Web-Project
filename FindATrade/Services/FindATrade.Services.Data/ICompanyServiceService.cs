namespace FindATrade.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using FindATrade.Data.Models;
    using FindATrade.Web.ViewModels.CompanyService;

    public interface ICompanyServiceService
    {
        Task CreateAsync(CreateCompanyServiceInputModel input, int id);

        Task<IEnumerable<Category>> GetGategoriesAsync();
    }
}
