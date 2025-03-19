using Entity.Files;
using Microsoft.AspNetCore.Http;

namespace Services.Files.IServices
{
    public interface IFilesService
    {
        Task<FileUploadResult> UploadFileAsync(IFormFile file, string containerName);

        Task<Stream> DownloadFileAsync(string fileName, string containerName);

        Task DeleteFileAsync(string fileName, string containerName);
    }
}
