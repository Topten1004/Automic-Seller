using Sales.AtomicSeller.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Sales.AtomicSeller.Data.TypeConfigurations
{
    public class AtomicServiceConfiguration : BaseEntityConfiguration<AtomicService>
    {
        public AtomicServiceConfiguration()
            : base()
        { }

        public override void Configure(EntityTypeBuilder<AtomicService> builder)
        {
            base.Configure(builder);
        }
    }
}
