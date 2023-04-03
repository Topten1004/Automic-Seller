using Sales.AtomicSeller.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Sales.AtomicSeller.Data.TypeConfigurations
{
    public class OrderConfiguration : BaseEntityConfiguration<Order>
    {
        public OrderConfiguration()
            : base()
        { }

        public override void Configure(EntityTypeBuilder<Order> builder)
        {
            base.Configure(builder);
        }
    }
}
