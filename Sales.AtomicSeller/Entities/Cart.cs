namespace Sales.AtomicSeller.Entities
{
    public class Cart : BaseEntity
    {
        public Cart()
        {
            CartItems = new List<CartItem>();
        }
        public string? SessionId { get; set; }
        public string? UserID { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string? TransactionId { get; set; }
        public virtual IList<CartItem> CartItems { get; set; }
        public decimal Total
        {
            get
            {
                if (CartItems == null)
                {
                    return 0;
                }
                return Math.Round(CartItems.Sum(item => item.AtomicService.UnitPriceExclTax * item.Quantity), 2);
            }
        }

    }
}
