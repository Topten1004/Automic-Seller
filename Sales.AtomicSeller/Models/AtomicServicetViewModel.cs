using Sales.AtomicSeller.Entities;

namespace Sales.AtomicSeller.Models
{
    public class AtomicServiceViewModel
    {
        public AtomicService AtomicService { get; set; }

        public List<AtomicService> AddOns { get; set; }
    }
}