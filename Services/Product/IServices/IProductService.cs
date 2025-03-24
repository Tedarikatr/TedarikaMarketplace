using Data.Dtos.Products;

namespace Services.Products.IServices
{
    public interface IProductService
    {
        Task<string> CreateProductAsync(ProductCreateDto dto);
        Task<string> UpdateProductAsync(int productId, ProductUpdateDto dto);
        Task<string> DeleteProductAsync(int productId);
        Task<IEnumerable<ProductDto>> GetAllProductsAsync();
        Task<ProductDto> GetProductByIdAsync(int productId);
    }
}
