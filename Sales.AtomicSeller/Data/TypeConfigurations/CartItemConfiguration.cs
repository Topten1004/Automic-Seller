using Sales.AtomicSeller.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Sales.AtomicSeller.Data.TypeConfigurations
{
    public class CartItemConfiguration : BaseEntityConfiguration<CartItem>
    {
        public CartItemConfiguration()
            : base()
        { }

        public override void Configure(EntityTypeBuilder<CartItem> builder)
        {
            base.Configure(builder);
        }
    }
}
