using Sales.AtomicSeller.Entities;

namespace Sales.AtomicSeller.Models
{
    public class PaymentCompleteViewModel
    {
        public bool SuccessufulPayment { get; set; }
        public Order Order { get; set; }
        public Cart Cart { get; set; }
        public string Message { get; set; }
    }
}