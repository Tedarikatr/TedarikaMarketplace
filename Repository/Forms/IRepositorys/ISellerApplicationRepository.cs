using Data.Repository;
using Entity.Forms;

namespace Repository.Forms.IRepositorys
{
    public interface ISellerApplicationRepository : IGenericRepository<SellerApplication>
    {
        Task<SellerApplication?> GetByGuidAsync(Guid guidId);
    }
}
