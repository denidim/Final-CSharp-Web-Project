namespace FindATrade.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using FindATrade.Data.Models;
    using FindATrade.Web.ViewModels.Company;
    using FindATrade.Web.ViewModels.Home;
    using FindATrade.Web.ViewModels.Review;

    public interface ICompanyService
    {
        Task CreateAsync(CreateCompanyInputModel input, ApplicationUser currentUser);

        Task<T> GetCompanyByIdAsync<T>(int id);

        Task<T> GetCompanyByUserIdAsync<T>(string id);

        Task UpdateAsync(int id, EditCompanyViewModel model);

        Task<IEnumerable<IndexPageOutputViewModel>> GetSubscribed();

        Task DeleteAsync(int id);

        bool IsUsersCompany(string userId, int companyId);

        Task<int?> GetCompanyByServiceId(int serviceId);

        Task<IEnumerable<IndexPageOutputViewModel>> GetAll(int pageNumber, int itemsPerPage = 12);

        int GetCount();
    }
}
