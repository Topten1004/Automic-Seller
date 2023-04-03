using Microsoft.EntityFrameworkCore;
using Sales.AtomicSeller.Business.IServices;
using Sales.AtomicSeller.Data;
using Sales.AtomicSeller.Entities;
using Sales.AtomicSeller.Repositories;

namespace Sales.AtomicSeller.Business.Services
{
    public class AtomicServiceService : Repository<AtomicService>, IAtomicServiceService
    {
        public AtomicServiceService(ApplicationDbContext applicationDbContext)
            : base(applicationDbContext)
        { }

        public async Task<AtomicService> GetById(int Id)
        {
            return await Context.AtomicServices.FirstAsync(o => o.Id == Id);
        }

        public async Task<List<AtomicService>> GetAll()
        {
            return await Context.AtomicServices.ToListAsync();
        }

        public async Task<AtomicService> GetBySku(string Sku)
        {
            return await Context.AtomicServices.FirstAsync(o => o.ServiceSKU == Sku);
        }

    }
}
