using Sales.AtomicSeller.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Sales.AtomicSeller.Data.TypeConfigurations
{
    public class LangConfiguration : BaseEntityConfiguration<Lang>
    {
        public LangConfiguration()
            : base()
        { }

        public override void Configure(EntityTypeBuilder<Lang> builder)
        {
            base.Configure(builder);
        }
    }
}
