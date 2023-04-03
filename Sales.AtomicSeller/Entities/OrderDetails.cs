using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sales.AtomicSeller.Entities
{
    public class OrderDetails : BaseEntity
    {
        public decimal UnitPrice { get; set; }
        public int OrderId { get; set; }
        public int Quantity { get; set; }
        public int ProductId { get; set; }
        public virtual Order Order { get; set; }
        public virtual AtomicService Product { get; set; }

    }
}
