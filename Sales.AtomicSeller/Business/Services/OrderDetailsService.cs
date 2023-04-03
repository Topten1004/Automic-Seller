using Sales.AtomicSeller.Business.IServices;
using Sales.AtomicSeller.Data;
using Sales.AtomicSeller.Entities;
using Sales.AtomicSeller.Repositories;

namespace Sales.AtomicSeller.Business.Services
{
    public class OrderDetailsService : Repository<OrderDetails>, IOrderDetailsService
    {
        public OrderDetailsService(ApplicationDbContext applicationDbContext)
            : base(applicationDbContext)
        { }

    }
}
