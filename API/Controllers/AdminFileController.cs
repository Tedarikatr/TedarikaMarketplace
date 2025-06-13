using Microsoft.AspNetCore.Mvc;
using Services.Files.IServices;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "admin")]
    public class AdminFileController : ControllerBase
    {
        private readonly IFilesService _filesService;
        private readonly ILogger<AdminFileController> _logger;
        private readonly string _defaultContainer = "admin-files";

        public AdminFileController(IFilesService filesService, ILogger<AdminFileController> logger)
        {
            _filesService = filesService;
            _logger = logger;
        }


        [HttpPost("upload")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("Dosya yüklenemedi.");

            var fileUrl = await _filesService.UploadFileAsync(file, "your-container-name");
            return Ok(new { FileUrl = fileUrl.Url });
        }

        [HttpGet("download/{fileName}")]
        public async Task<IActionResult> DownloadFile(string fileName)
        {
            try
            {
                var stream = await _filesService.DownloadFileAsync(fileName, _defaultContainer);
                return File(stream, "application/octet-stream", fileName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Dosya indirme sırasında hata oluştu: {FileName}", fileName);
                return StatusCode(500, "Dosya indirilemedi.");
            }
        }

        [HttpDelete("delete/{fileName}")]
        public async Task<IActionResult> DeleteFile(string fileName)
        {
            try
            {
                await _filesService.DeleteFileAsync(fileName, _defaultContainer);
                _logger.LogInformation("Dosya silindi: {FileName}", fileName);
                return Ok($"Dosya silindi: {fileName}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Dosya silme sırasında hata oluştu: {FileName}", fileName);
                return StatusCode(500, "Dosya silinemedi.");
            }
        }
    }
}
