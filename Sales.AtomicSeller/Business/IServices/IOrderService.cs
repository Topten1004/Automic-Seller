using Sales.AtomicSeller.Entities;
using Sales.AtomicSeller.Repositories;

namespace Sales.AtomicSeller.Business.IServices
{
    public interface IOrderService : IRepository<Order>
    {
        Task<Order> Genereate(Cart cart, string transactionId);
        Task<List<Order>> GetMine();
        Task<List<Order>> GetAll();
        Task<Order> GetMine(int id);
    }
}
