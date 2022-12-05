using System.Collections.Generic;
using System.Threading.Tasks;

namespace FindATrade.Services.Data
{
    public interface IImageService
    {
        Task<string> GenerateSingleImageUrlForCompany(int companyId);

        Task<IEnumerable<string>> GenerateImageUrlsForService(int serviceId);
    }
}
