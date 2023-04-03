using Sales.AtomicSeller.Entities;
using Sales.AtomicSeller.Repositories;

namespace Sales.AtomicSeller.Business.IServices
{
    public interface IInvoiceService 
    {
        Task<decimal> CalculateTax(decimal total);
        Task<bool> Generate(Order order, bool sendByEmail);
    }
}
