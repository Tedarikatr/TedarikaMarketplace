using API.Helpers;
using Data.Dtos.Categories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Categories.IServices;
using Services.Files.IServices;

namespace API.Controllers.Categories
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "admin")]
    //[Authorize]
    public class AdminCategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly AdminUserContextHelper _adminUserContextHelper;
        private readonly ILogger<AdminCategoryController> _logger;
        private readonly IFilesService _filesService;

        public AdminCategoryController(ICategoryService categoryService, AdminUserContextHelper adminUserContextHelper, ILogger<AdminCategoryController> logger, IFilesService filesService)
        {
            _categoryService = categoryService;
            _adminUserContextHelper = adminUserContextHelper;
            _logger = logger;
            _filesService = filesService;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllCategories()
        {
            try
            {
                _logger.LogInformation("Tüm kategoriler listeleniyor.");
                var categories = await _categoryService.GetAllCategoriesAsync();
                return Ok(categories);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Tüm kategorileri listeleme sırasında hata oluştu.");
                return StatusCode(500, new { Error = "Kategorileri listelerken bir hata oluştu." });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            try
            {
                _logger.LogInformation("Kategori detayları alınıyor. Kategori ID: {CategoryId}", id);
                var category = await _categoryService.GetCategoryByIdAsync(id);
                return Ok(category);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Kategori bilgileri alınırken hata oluştu. ID: {CategoryId}", id);
                return StatusCode(500, new { Error = "Kategori bilgileri alınırken bir hata oluştu." });
            }
        }

        //[HttpPost("create")]
        //public async Task<IActionResult> CreateCategory([FromForm] CategoryCreateDto categoryCreateDto, [FromForm] IFormFile file)
        //{
        //    try
        //    {
        //        int adminId = AdminUserContextHelper.GetAdminId(User);
        //        _logger.LogInformation("Yeni kategori ekleniyor. Admin ID: {AdminId}, Kategori Adı: {CategoryName}", adminId, categoryCreateDto.CategoryName);

        //        if (file == null || file.Length == 0)
        //            return BadRequest("Dosya yüklenemedi.");

        //        var fileUploadResult = await _filesService.UploadFileAsync(file, "sector");
        //        categoryCreateDto.CategoryImage = fileUploadResult.Url;

        //        var result = await _categoryService.CreateCategoryAsync(categoryCreateDto, file);
        //        return Ok(new { Message = result });
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "Kategori ekleme işlemi sırasında hata oluştu.");
        //        return StatusCode(500, new { Error = "Kategori ekleme işlemi sırasında bir hata oluştu." });
        //    }
        //}

        [HttpPut("update")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] CategoryUpdateDto categoryUpdateDto)
        {
            try
            {
                int adminId = _adminUserContextHelper.GetAdminId(User);
                _logger.LogInformation("Kategori güncelleniyor. Admin ID: {AdminId}, Kategori ID: {CategoryId}", adminId, id);

                var result = await _categoryService.UpdateCategoryAsync(id, categoryUpdateDto);
                return Ok(new { Message = result });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Kategori güncelleme sırasında hata oluştu. ID: {CategoryId}", id);
                return StatusCode(500, new { Error = "Kategori güncelleme sırasında bir hata oluştu." });
            }
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            try
            {
                int adminId = _adminUserContextHelper.GetAdminId(User);
                _logger.LogInformation("Kategori siliniyor. Admin ID: {AdminId}, Kategori ID: {CategoryId}", adminId, id);

                var result = await _categoryService.DeleteCategoryAsync(id);
                return Ok(new { Message = result });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Kategori silme işlemi sırasında hata oluştu. ID: {CategoryId}", id);
                return StatusCode(500, new { Error = "Kategori silme işlemi sırasında bir hata oluştu." });
            }
        }

        //[HttpPut("update-image")]
        //public async Task<IActionResult> UpdateCategoryImage([FromQuery] int id, IFormFile file)
        //{
        //    try
        //    {
        //        int adminId = AdminUserContextHelper.GetAdminId(User);
        //        _logger.LogInformation("Kategori resmi güncelleniyor. Admin ID: {AdminId}, Kategori ID: {CategoryId}", adminId, id);

        //        if (file == null || file.Length == 0)
        //            return BadRequest("Dosya yüklenemedi.");
        //        var result = await _categoryService.UpdateCategoryImageAsync(id, file);

        //        return Ok(new { Message = result });
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "Kategori resmi güncellenirken hata oluştu. ID: {CategoryId}", id);
        //        return StatusCode(500, new { Error = "Kategori resmi güncellenirken bir hata oluştu." });
        //    }
        //}
    }
}
