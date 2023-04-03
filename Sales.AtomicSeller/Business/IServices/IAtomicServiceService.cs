using Sales.AtomicSeller.Entities;
using Sales.AtomicSeller.Repositories;

namespace Sales.AtomicSeller.Business.IServices
{
    public interface IAtomicServiceService : IRepository<AtomicService>
    {
        Task<List<AtomicService>> GetAll();
        Task<AtomicService> GetById(int id);
    }
}
