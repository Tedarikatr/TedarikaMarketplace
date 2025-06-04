using Data.Dtos.Stores.Products;
using Microsoft.AspNetCore.Http;

namespace Services.Stores.Product.IServices
{
    public interface IStoreProductService
    {
        Task<IEnumerable<StoreProductDto>> GetAllProductsByShopDirectIdAsync(int storeId);
        Task<string> AddProductToStoreAsync(int storeId, int productId);
        Task<string> UpdateStoreProductPriceAsync(int storeId, int productId, decimal price);
        Task<string> SetProductActiveStatusAsync(int storeId, int productId, bool isActive);
        Task<string> SetProductOnSaleStatusAsync(int storeId, int productId, bool isOnSale);
        Task<string> SetMinMaxQuantityAsync(int storeId, int productId, int minQty, int maxQty);
        Task<string> UploadAndSetStoreImageAsync(int storeId, int productId, IFormFile file);
        Task<bool> UpdateMinMaxOrderQuantityAsync(int shopDirectId, int productId, int minOrderQuantity, int maxOrderQuantity);
    }
}
