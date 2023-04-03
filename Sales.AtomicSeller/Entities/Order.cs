using Sales.AtomicSeller.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sales.AtomicSeller.Entities
{
    public class Order : BaseEntity
    {
        public Order()
        {
            OrderDetails = new HashSet<OrderDetails>();
        }
        public string? Number { get; set; }
        public DateTime CreatedOn { get; set; }
        public decimal Total { get; set; }
        public string? PaymentTransactionId { get; set; }
        public PaymentSourceEnum? PaymentSource { get; set; }
        public string Currency { get; set; } = "USD";
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
        public virtual ICollection<OrderDetails> OrderDetails { get; set; }
    }
}
