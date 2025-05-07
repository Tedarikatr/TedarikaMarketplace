using Data.Databases;
using Data.Repository;
using Entity.Orders;
using Repository.Orders.IRepositorys;

namespace Repository.Orders.Repositorys
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        public OrderRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
