using Data.Databases;
using Data.Repository;
using Entity.Payments;
using Repository.Payments.IRepositorys;

namespace Repository.Payments.Repositorys
{
    public class PaymentRepository : GenericRepository<Payment>, IPaymentRepository
    {
        public PaymentRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
