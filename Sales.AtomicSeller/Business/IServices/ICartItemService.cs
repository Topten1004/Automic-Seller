using Sales.AtomicSeller.Entities;
using Sales.AtomicSeller.Repositories;

namespace Sales.AtomicSeller.Business.IServices
{
    /// <summary>
    /// Cart Item service.
    /// </summary>
    public interface ICartItemService : IRepository<CartItem>
    {
    }
}
