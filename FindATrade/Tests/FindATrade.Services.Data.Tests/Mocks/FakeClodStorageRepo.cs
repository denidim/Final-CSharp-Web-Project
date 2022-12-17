using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace FindATrade.Services.Data.Tests.Mocks
{
    public class FakeClodStorageRepo : ICloudStorageService
    {
        public Task<string> GetSignedUrlAsync(string fileNameToRead, int timeOutInMinutes = 30)
        {
            return this.GetResult();
        }

        public Task<string> UploadFileAsync(IFormFile fileToUpload, string fileNameToSave)
        {
            return this.GetResult();
        }

        public Task DeleteFileAsync(string fileNameToDelete)
        {
            return this.GetResult();
        }

        private async Task<string> GetResult()
        {
            return "ok";
        }
    }
}
