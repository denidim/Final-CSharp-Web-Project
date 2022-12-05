namespace FindATrade.Services.Data
{
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using System.Threading.Tasks;

    using FindATrade.Data.Models;
    using FindATrade.Web.ViewModels.CompanyService;

    public interface ICompanyServiceService
    {
        Task<IEnumerable<CompanyServiceOutputModel>> GetAllByUserIdOrCompanyId(params object[] objects);

        Task CreateAsync(CreateCompanyServiceInputModel input, int id);

        Task<IEnumerable<Category>> GetGategoriesAsync();

        Task<T> GetByIdAsync<T>(int id);

        Task UpdateAsync(int id, EditServiceViewModel model);

        IEnumerable<int> GetAllForVettingIds();

        Task<IEnumerable<CompanyServiceByCategoryModel>> GetAllByCategory(string categoryName);

        bool IsUsersCompany(int serviceId, string userId);

        
    }
}
