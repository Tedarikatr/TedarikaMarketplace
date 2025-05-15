using Data.Dtos.Product;

namespace Services.Product.IServices
{
    public interface IProductExportBannedService
    {
        Task AddRestrictionAsync(ProductExportBannedCreateDto dto);
        Task RemoveRestrictionAsync(int restrictionId);
        Task<List<ProductExportBannedDto>> GetRestrictionsByProductIdAsync(int productId);
    }
}
