using Sales.AtomicSeller.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Sales.AtomicSeller.Data.TypeConfigurations
{
    public class OrderDetailsConfiguration : BaseEntityConfiguration<OrderDetails>
    {
        public OrderDetailsConfiguration()
            : base()
        { }

        public override void Configure(EntityTypeBuilder<OrderDetails> builder)
        {
            base.Configure(builder);
        }
    }
}
