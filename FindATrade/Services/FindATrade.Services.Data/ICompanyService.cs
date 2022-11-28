namespace FindATrade.Services.Data
{
    using System.Threading.Tasks;

    using FindATrade.Data.Models;
    using FindATrade.Web.ViewModels.Company;

    public interface ICompanyService
    {
        Task CreateAsync(CreateCompanyInputModel input, ApplicationUser currentUser);

        Task<T> GetCompanyByIdAsync<T>(int id);

        Task<T> GetCompanyByUserIdAsync<T>(string id);

        Task UpdateAsync(int id, EditCompanyViewModel model);

        Task<T> GetPopular<T>();
    }
}
