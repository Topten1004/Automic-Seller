using Sales.AtomicSeller.Entities;
using Sales.AtomicSeller.Repositories;

namespace Sales.AtomicSeller.Business.IServices
{
    /// <summary>
    /// Cart service.
    /// </summary>
    public interface ILangService : IRepository<Lang>
    {
        Task<List<Lang>> GetAll();
        Task<Lang> GetByToken(string Token);

    }
}
