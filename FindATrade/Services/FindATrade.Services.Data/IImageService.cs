namespace FindATrade.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using FindATrade.Web.ViewModels.CompanyService;

    public interface IImageService
    {
        Task<string> GenerateSingleImageUrlForCompanyAsync(int companyId);

        Task<IEnumerable<string>> GenerateImageUrlsForServiceAsync(int serviceId);

        Task<IEnumerable<AllPicturesModel>> GetAllPicturesAsync(int serviceId);

        Task CloudDelete(string name);

        Task Delete(string name);

        Task Add(AddImages images, int id);
    }
}
