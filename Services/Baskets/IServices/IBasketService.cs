using Data.Dtos.Baskets;

namespace Services.Baskets.IServices
{
    public interface IBasketService
    {
        Task<BasketDto> GetBasketAsync(int userId);
        Task AddItemAsync(int userId, BasketAddToDto dto);
        Task<BasketDto> IncreaseQuantityAsync(int userId, int productId);
        Task<BasketDto> DecreaseQuantityAsync(int userId, int productId);
        Task RemoveItemAsync(int userId, int productId);
        Task ClearBasketAsync(int userId);
        Task<decimal> GetTotalAsync(int userId);
    }
}
