using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Configuration;
using Services.Files.IServices;

namespace Services.Files.Services
{
    public class AzureBlobPdfService : IPdfService
    {
        private readonly BlobServiceClient _blobServiceClient;
        private readonly string _containerName;

        public AzureBlobPdfService(IConfiguration configuration)
        {
            var connectionString = configuration.GetSection("AzureBlobStorage:ConnectionString").Value;
            _containerName = configuration.GetSection("AzureBlobStorage:ContainerName").Value;

            if (string.IsNullOrEmpty(connectionString) || string.IsNullOrEmpty(_containerName))
            {
                throw new ArgumentNullException("Azure Blob ConnectionString veya ContainerName boş olamaz.");
            }

            _blobServiceClient = new BlobServiceClient(connectionString);
        }

        public async Task<string> UploadPdfAsync(Stream pdfStream, string fileName)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(_containerName);

            await containerClient.CreateIfNotExistsAsync(PublicAccessType.Blob);

            var blobClient = containerClient.GetBlobClient(fileName);

            await blobClient.UploadAsync(pdfStream, new BlobHttpHeaders { ContentType = "application/pdf" });

            return blobClient.Uri.ToString();
        }

        public async Task DeletePdfAsync(string fileName)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(_containerName);
            var blobClient = containerClient.GetBlobClient(fileName);

            await blobClient.DeleteIfExistsAsync();
        }
    }
}
