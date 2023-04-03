using Sales.AtomicSeller.Data.TypeConfigurations;
using Sales.AtomicSeller.Entities;
using Sales.AtomicSeller.Providers;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Xml.Linq;
using System.Reflection.Emit;

namespace Sales.AtomicSeller.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public IIdentityProvider IdentityProvider { get; set; }
        #region DbSets
        public virtual DbSet<AtomicService> AtomicServices { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderDetails> OrderDetails { get; set; }
        public virtual DbSet<Cart> Carts { get; set; }
        public virtual DbSet<CartItem> CartItems { get; set; }
        public virtual DbSet<Lang> Lang { get; set; }
        #endregion
        private bool isMigrationMode = false;

        [Obsolete("For Migration Usage Only!", true)]
        public ApplicationDbContext()
        {
            isMigrationMode = true;
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IIdentityProvider identityProvider)
          : base(options)
        {
            this.IdentityProvider = identityProvider;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (isMigrationMode)
            {
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-QN643P1;Initial Catalog=sales884_ASPBI;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
                //optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=ASPBI;Trusted_Connection=True;MultipleActiveResultSets=true");
            }
            else
            {
                base.OnConfiguring(optionsBuilder);
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {


            builder.ApplyConfiguration(new AtomicServiceConfiguration());
            builder.ApplyConfiguration(new OrderConfiguration());
            builder.ApplyConfiguration(new OrderDetailsConfiguration());
            builder.ApplyConfiguration(new CartConfiguration());
            builder.ApplyConfiguration(new CartItemConfiguration());

            builder.Entity<Lang>(entity =>
            {
                entity.Property(e => e.ArEg).HasColumnName("Ar-Eg");
                entity.Property(e => e.DeDe).HasColumnName("De-De");
                entity.Property(e => e.ElGr).HasColumnName("El-Gr");
                entity.Property(e => e.EnUs).HasColumnName("En-Us");
                entity.Property(e => e.EsEs).HasColumnName("Es-Es");
                entity.Property(e => e.FrFr).HasColumnName("Fr-Fr");
                entity.Property(e => e.HeIl).HasColumnName("He-Il");
                entity.Property(e => e.HiIn).HasColumnName("Hi-In");
                entity.Property(e => e.IdId).HasColumnName("Id-Id");
                entity.Property(e => e.ItIt).HasColumnName("It-It");
                entity.Property(e => e.JaJp).HasColumnName("Ja-Jp");
                entity.Property(e => e.NlNl).HasColumnName("Nl-Nl");
                entity.Property(e => e.PlPl).HasColumnName("Pl-Pl");
                entity.Property(e => e.PtPt).HasColumnName("Pt-Pt");
                entity.Property(e => e.RuRu).HasColumnName("Ru-Ru");
                entity.Property(e => e.ZhChs).HasColumnName("Zh-Chs");
                entity.Property(e => e.ZhTw).HasColumnName("Zh-Tw");

            });

            builder.ApplyConfiguration(new LangConfiguration());

            base.OnModelCreating(builder);
        }
    }
}