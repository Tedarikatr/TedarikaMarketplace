using Data.Dtos.Product;
using Microsoft.AspNetCore.Http;

namespace Services.Product.IServices
{
    public interface IProductService
    {
        // Temel CRUD İşlemleri
        Task<string> CreateProductAsync(ProductCreateDto dto);
        Task<string> UpdateProductAsync(int productId, ProductUpdateDto dto);
        Task<string> UpdateProductImageAsync(int productId, IFormFile imageFile);
        Task<string> DeleteProductAsync(int productId);
        Task<IEnumerable<ProductDto>> GetAllProductsAsync();
        Task<ProductDto> GetProductByIdAsync(int productId);

        // Filtreleme & Arama
        Task<IEnumerable<ProductDto>> SearchProductsAsync(string query);
        Task<IEnumerable<ProductDto>> GetProductsByCategoryAsync(int categoryId);
        Task<IEnumerable<ProductDto>> GetProductsByStoreIdAsync(int storeId);
        Task<IEnumerable<ProductDto>> GetProductsByTagAsync(string tag);

        // Etiketleme
        Task TagProductAsync(int productId, string tag);

        // Stok & Uygunluk
        Task<bool> IsProductAvailableAsync(int productId);
        Task<IEnumerable<ProductDto>> GetRecentlyUpdatedProductsAsync(DateTime since);

        // Analitik ve Raporlama
        Task<int> GetTotalProductCountAsync();
        Task<IDictionary<string, int>> GetProductCountByCategoryAsync();

        // Versiyonlama / Değişiklik Geçmişi
        Task<IEnumerable<ProductChangeLogDto>> GetProductChangeLogsAsync(int productId);

        // Toplu İşlemler
        Task<string> UpdateMultipleProductsAsync(IEnumerable<ProductUpdateDto> dtos);
        Task<string> DeleteMultipleProductsAsync(IEnumerable<int> productIds);

        // Öneri Sistemi
        Task<IEnumerable<ProductDto>> GetSuggestedProductsForUserAsync(int userId);

        // Çoklu Dil / Bölge Desteği
        Task<ProductDto> GetProductLocalizedAsync(int productId, string culture);
    }
}
