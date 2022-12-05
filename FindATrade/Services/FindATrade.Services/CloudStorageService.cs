namespace FindATrade.Services
{
    using System;
    using System.IO;
    using System.Threading.Tasks;

    using FindATrade.Data.Configurations;
    using Google.Apis.Auth.OAuth2;
    using Google.Cloud.Storage.V1;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;

    public class CloudStorageService : ICloudStorageService
    {
        private readonly GCSConfigOptions options;
        private readonly ILogger<CloudStorageService> logger;
        private readonly GoogleCredential googleCredential;

        public CloudStorageService(IOptions<GCSConfigOptions> options, ILogger<CloudStorageService> logger)
        {
            this.options = options.Value;
            this.logger = logger;

            try
            {
                var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
                if (environment == Environments.Production)
                {
                    // Store the json file in Secrets.
                    this.googleCredential = GoogleCredential.FromJson(this.options.GCPStorageAuthFile);
                }
                else
                {
                    this.googleCredential = GoogleCredential.FromFile(this.options.GCPStorageAuthFile);
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError($"{ex.Message}");
                throw;
            }
        }

        public async Task DeleteFileAsync(string fileNameToDelete)
        {
            try
            {
                using (var storageClient = StorageClient.Create(this.googleCredential))
                {
                    await storageClient.DeleteObjectAsync(this.options.GoogleCloudStorageBucketName, fileNameToDelete);
                }

                this.logger.LogInformation($"File {fileNameToDelete} deleted");
            }
            catch (Exception ex)
            {
                this.logger.LogError($"Error occured while deleting file {fileNameToDelete}: {ex.Message}");
                throw;
            }
        }

        public async Task<string> GetSignedUrlAsync(string fileNameToRead, int timeOutInMinutes = 30)
        {
            try
            {
                var sac = this.googleCredential.UnderlyingCredential as ServiceAccountCredential;
                var urlSigner = UrlSigner.FromServiceAccountCredential(sac);

                // provides limited permission and time to make a request: time here is mentioned for 30 minutes.
                var signedUrl = await urlSigner.SignAsync(this.options.GoogleCloudStorageBucketName, fileNameToRead, TimeSpan.FromMinutes(timeOutInMinutes));
                this.logger.LogInformation($"Signed url obtained for file {fileNameToRead}");
                return signedUrl.ToString();
            }
            catch (Exception ex)
            {
                this.logger.LogError($"Error occured while obtaining signed url for file {fileNameToRead}: {ex.Message}");
                throw;
            }
        }

        public async Task<string> UploadFileAsync(IFormFile fileToUpload, string fileNameToSave)
        {
            try
            {
                this.logger.LogInformation($"Uploading: file {fileNameToSave} to storage {this.options.GoogleCloudStorageBucketName}");
                using (var memoryStream = new MemoryStream())
                {
                    await fileToUpload.CopyToAsync(memoryStream);

                    // Create Storage Client from Google Credential
                    using (var storageClient = StorageClient.Create(this.googleCredential))
                    {
                        // upload file stream
                        var uploadedFile = await storageClient.UploadObjectAsync(this.options.GoogleCloudStorageBucketName, fileNameToSave, fileToUpload.ContentType, memoryStream);
                        this.logger.LogInformation($"Uploaded: file {fileNameToSave} to storage {this.options.GoogleCloudStorageBucketName}");
                        return uploadedFile.MediaLink;
                    }
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError($"Error while uploading file {fileNameToSave}: {ex.Message}");
                throw;
            }
        }
    }
}
