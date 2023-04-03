using Microsoft.EntityFrameworkCore;
using Sales.AtomicSeller.Business.IServices;
using Sales.AtomicSeller.Data;
using Sales.AtomicSeller.Entities;
using Sales.AtomicSeller.Repositories;

namespace Sales.AtomicSeller.Business.Services
{
    public class LangService : Repository<Lang>, ILangService
    {
        public LangService(ApplicationDbContext applicationDbContext)
            : base(applicationDbContext)
        { }

        public async Task<List<Lang>> GetAll()
        {
            return await Context.Lang.ToListAsync();
        }

        public async Task<Lang> GetByToken(string Token)
        {
            return await Context.Lang.FirstAsync(o => o.Token == Token);
        }


    }
}
