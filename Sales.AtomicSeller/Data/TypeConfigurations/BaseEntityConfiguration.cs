using Sales.AtomicSeller.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Sales.AtomicSeller.Data.TypeConfigurations
{
    public class BaseEntityConfiguration<T> : IEntityTypeConfiguration<T> where T : BaseEntity
    {
        public BaseEntityConfiguration()
        {
        }
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
         
        }
    }
}
