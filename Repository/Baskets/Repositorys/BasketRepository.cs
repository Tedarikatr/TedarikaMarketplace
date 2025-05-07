using Data.Databases;
using Data.Repository;
using Entity.Baskets;
using Repository.Baskets.IRepositorys;

namespace Repository.Baskets.Repositorys
{
    public class BasketRepository : GenericRepository<Basket>, IBasketRepository
    {
        public BasketRepository(ApplicationDbContext context) : base(context) { }
    
    }
}
