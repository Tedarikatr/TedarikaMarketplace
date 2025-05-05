using Data.Dtos.Product;
using Microsoft.AspNetCore.Http;

namespace Services.Product.IServices
{
    public interface IProductService
    {
        Task<string> CreateProductAsync(ProductCreateDto dto);
        Task<string> UpdateProductAsync(int productId, ProductUpdateDto dto);
        Task<string> UpdateProductImageAsync(int productId, IFormFile imageFile);
        Task<string> DeleteProductAsync(int productId);
        Task<IEnumerable<ProductDto>> GetAllProductsAsync();
        Task<ProductDto> GetProductByIdAsync(int productId);
    }
}
