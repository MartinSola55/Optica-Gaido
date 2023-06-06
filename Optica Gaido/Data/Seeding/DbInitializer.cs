using Microsoft.EntityFrameworkCore;
using Optica_Gaido.Models;

namespace Optica_Gaido.Data.Seeding
{
    public class DbInitializer
    {
        private readonly ModelBuilder modelBuilder;

        public DbInitializer(ModelBuilder modelBuilder)
        {
            this.modelBuilder = modelBuilder;
        }

        public void Seed()
        {
            modelBuilder.Entity<Brand>().HasData(
                new Brand() { ID = 1, Name = "RayBan" },
                new Brand() { ID = 2, Name = "Vulk" },
                new Brand() { ID = 3, Name = "UP!" },
                new Brand() { ID = 4, Name = "Oakley" }
            );
            
            modelBuilder.Entity<Seller>().HasData(
                new Seller() { ID = 1, Name = "Gina", Surname = "Gaido" },
                new Seller() { ID = 2, Name = "Lucy", Surname = "Gaido" }
            );

            modelBuilder.Entity<Doctor>().HasData(
                new Doctor() { ID = 1, Name = "Fernando", Surname = "Sola", License = "123123" },
                new Doctor() { ID = 2, Name = "Vanina", Surname = "Chalita", License = "456456" },
                new Doctor() { ID = 3, Name = "Ana María", Surname = "Bertinat", License = "789789" }
            );

            modelBuilder.Entity<HealthInsurance>().HasData(
                new HealthInsurance() { ID = 1, Name = "IAPOS" },
                new HealthInsurance() { ID = 2, Name = "OSDE" },
                new HealthInsurance() { ID = 3, Name = "PAMI" },
                new HealthInsurance() { ID = 4, Name = "OSECAC" }
            );

            modelBuilder.Entity<Material>().HasData(
                new Material() { ID = 1, Description = "Metal" },
                new Material() { ID = 2, Description = "Plástico" },
                new Material() { ID = 3, Description = "Madera" }
            );

            modelBuilder.Entity<Provider>().HasData(
                new Provider() { ID = 1, Name = "Lionel Andrés", Surname = "Messi" },
                new Provider() { ID = 2, Name = "Andrés", Surname = "Iniesta" },
                new Provider() { ID = 3, Name = "Gabriel", Surname = "Batistuta" },
                new Provider() { ID = 4, Name = "Diego Armando", Surname = "Maradona" }
            );

            modelBuilder.Entity<GlassColor>().HasData(
                new GlassColor() { ID = 1, Name = "Blanco" },
                new GlassColor() { ID = 2, Name = "Tinte 50" },
                new GlassColor() { ID = 3, Name = "Foto Grey" },
                new GlassColor() { ID = 4, Name = "Hih H. Lite Blanco" },
                new GlassColor() { ID = 5, Name = "Hih H. Lite Sepia" }
            );

            modelBuilder.Entity<GlassType>().HasData(
                new GlassType() { ID = 1, Name = "Orgánico" },
                new GlassType() { ID = 2, Name = "Mineral" }
            );

            modelBuilder.Entity<PaymentMethod>().HasData(
                new PaymentMethod() { ID = 1, Name = "Efectivo" },
                new PaymentMethod() { ID = 2, Name = "Transferencia" },
                new PaymentMethod() { ID = 3, Name = "Débito" },
                new PaymentMethod() { ID = 4, Name = "Débito (local)" },
                new PaymentMethod() { ID = 5, Name = "Crédito" },
                new PaymentMethod() { ID = 6, Name = "Crédito (local)" }
            );
        }
    }

}
