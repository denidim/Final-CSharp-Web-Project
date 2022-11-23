namespace FindATrade.Services.Data
{
    using System.Threading.Tasks;

    using FindATrade.Data.Models;
    using FindATrade.Web.ViewModels.Company;

    public interface ICompanyService
    {
        Task CreateAsync(CreateCompanyInputModel input, ApplicationUser currentUser);
    }
}
