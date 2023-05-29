using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Optica_Gaido.Models;

namespace Optica_Gaido.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Configuraciones adicionales aquí

            modelBuilder.Entity<SalePaymentMethod>()
                .HasKey(sp => new { sp.SaleID, sp.PaymentMethodID });
        }

        public DbSet<Brand> Brands { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<Frame> Frames { get; set; }
        public DbSet<GlassColor> GlassColors { get; set; }
        public DbSet<GlassFormat> GlassFormats { get; set; }
        public DbSet<GlassType> GlassTypes { get; set; }
        public DbSet<HealthInsurance> HealthInsurances { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }
        public DbSet<Provider> Providers { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<SalePaymentMethod> SalePaymentMethods { get; set; }
        public DbSet<Seller> Sellers { get; set; }
    }
}