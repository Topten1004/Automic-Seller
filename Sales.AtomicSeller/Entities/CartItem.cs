namespace Sales.AtomicSeller.Entities
{
    public class CartItem : BaseEntity
    {
        public int Quantity { get; set; }
        public int ProductId { get; set; }
        public int AtomicServiceId { get; set; }
        public virtual AtomicService AtomicService{ get; set; }
    }
}
