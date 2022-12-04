namespace FindATrade.Services
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;

    public interface IColudStorageService
    {
        Task<string> GetSignedUrlAsync(string fileNameToRead, int timeOutInMinutes = 30);

        Task<string> UploadFileAsync(IFormFile fileToUpload, string fileNameToSave);

        Task DeleteFileAsync(string fileNameToDelete);
    }
}
