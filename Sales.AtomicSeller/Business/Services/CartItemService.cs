using Sales.AtomicSeller.Business.IServices;
using Sales.AtomicSeller.Data;
using Sales.AtomicSeller.Entities;
using Sales.AtomicSeller.Repositories;

namespace Sales.AtomicSeller.Business.Services
{
    public class CartItemService : Repository<CartItem>, ICartItemService
    {
        public CartItemService(ApplicationDbContext applicationDbContext)
            : base(applicationDbContext)
        { }

    }
}
