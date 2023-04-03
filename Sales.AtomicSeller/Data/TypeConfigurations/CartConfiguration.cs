using Sales.AtomicSeller.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Sales.AtomicSeller.Data.TypeConfigurations
{
    public class CartConfiguration : BaseEntityConfiguration<Cart>
    {
        public CartConfiguration()
            : base()
        { }

        public override void Configure(EntityTypeBuilder<Cart> builder)
        {
            base.Configure(builder);
        }
    }
}
