using API.Helpers;
using Data.Dtos.Categories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Categories.IServices;

namespace API.Controllers.Categories
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "admin")]
    [Authorize(Roles = "SuperAdmin, Admin")]
    public class AdminCategorySubController : ControllerBase
    {
        private readonly ICategorySubService _categorySubService;
        private readonly ILogger<AdminCategorySubController> _logger;

        public AdminCategorySubController(ICategorySubService categorySubService, ILogger<AdminCategorySubController> logger)
        {
            _categorySubService = categorySubService;
            _logger = logger;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllCategorySubs()
        {
            try
            {
                _logger.LogInformation("Tüm alt kategoriler listeleniyor.");
                var categorySubs = await _categorySubService.GetAllCategorySubsAsync();
                return Ok(categorySubs);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Alt kategorileri listeleme sırasında hata oluştu.");
                return StatusCode(500, new { Error = "Alt kategorileri listelerken bir hata oluştu." });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategorySubById(int id)
        {
            try
            {
                _logger.LogInformation("Alt kategori detayları alınıyor. Alt Kategori ID: {CategorySubId}", id);
                var categorySub = await _categorySubService.GetCategorySubByIdAsync(id);
                return Ok(categorySub);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Alt kategori bilgileri alınırken hata oluştu. ID: {CategorySubId}", id);
                return StatusCode(500, new { Error = "Alt kategori bilgileri alınırken bir hata oluştu." });
            }
        }

        [HttpGet("by-category/{categoryId}")]
        public async Task<IActionResult> GetCategorySubsByMainCategoryId(int categoryId)
        {
            try
            {
                _logger.LogInformation("Ana kategoriye ait alt kategoriler listeleniyor. Ana Kategori ID: {MainCategoryId}", categoryId);
                var categorySubs = await _categorySubService.GetCategorySubsByMainCategoryIdAsync(categoryId);
                return Ok(categorySubs);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ana kategoriye ait alt kategorileri listeleme sırasında hata oluştu. Ana Kategori ID: {MainCategoryId}", categoryId);
                return StatusCode(500, new { Error = "Ana kategoriye ait alt kategorileri listelerken bir hata oluştu." });
            }
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateCategorySub([FromQuery] CategorySubCreateDto categorySubCreateDto)
        {
            try
            {
                int adminId = AdminUserContextHelper.GetAdminId(User);
                _logger.LogInformation("Yeni alt kategori ekleniyor. Admin ID: {AdminId}, Ad: {CategorySubName}", adminId, categorySubCreateDto.Name);

                var result = await _categorySubService.AddCategorySubAsync(categorySubCreateDto);
                return Ok(new { Message = result });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Alt kategori ekleme sırasında hata oluştu.");
                return StatusCode(500, new { Error = "Alt kategori ekleme sırasında bir hata oluştu." });
            }
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateCategorySub([FromQuery] int id, [FromBody] CategorySubUpdateDto categorySubUpdateDto)
        {
            try
            {
                int adminId = AdminUserContextHelper.GetAdminId(User);
                _logger.LogInformation("Alt kategori güncelleniyor. Admin ID: {AdminId}, ID: {CategorySubId}", adminId, id);

                var result = await _categorySubService.UpdateCategorySubAsync(id, categorySubUpdateDto);
                return Ok(new { Message = result });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Alt kategori güncelleme sırasında hata oluştu. ID: {CategorySubId}", id);
                return StatusCode(500, new { Error = "Alt kategori güncelleme sırasında bir hata oluştu." });
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteCategorySub(int id)
        {
            try
            {
                int adminId = AdminUserContextHelper.GetAdminId(User);
                _logger.LogInformation("Alt kategori siliniyor. Admin ID: {AdminId}, ID: {CategorySubId}", adminId, id);

                var result = await _categorySubService.DeleteCategorySubAsync(id);
                return Ok(new { Message = result });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Alt kategori silme işlemi sırasında hata oluştu. ID: {CategorySubId}", id);
                return StatusCode(500, new { Error = "Alt kategori silme işlemi sırasında bir hata oluştu." });
            }
        }
    }
}
