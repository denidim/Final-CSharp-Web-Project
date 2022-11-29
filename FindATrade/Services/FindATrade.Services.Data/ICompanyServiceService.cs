namespace FindATrade.Services.Data
{
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using System.Threading.Tasks;

    using FindATrade.Data.Models;
    using FindATrade.Web.ViewModels.Company;
    using FindATrade.Web.ViewModels.CompanyService;

    public interface ICompanyServiceService
    {
        IEnumerable<CompanyServiceOutputModel> GetAllCompanyServices(params object[] objects);

        Task CreateAsync(CreateCompanyServiceInputModel input, int id);

        Task<IEnumerable<Category>> GetGategoriesAsync();

        Task<T> GetByIdAsync<T>(int id);

        Task UpdateAsync(int id, EditServiceViewModel model);
    }
}
