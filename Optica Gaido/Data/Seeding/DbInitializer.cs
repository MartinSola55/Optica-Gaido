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
                new Brand() { ID = 1, Name = "Humah" },
                new Brand() { ID = 2, Name = "Vulk" },
                new Brand() { ID = 3, Name = "Rusty" },
                new Brand() { ID = 4, Name = "Hardem" },
                new Brand() { ID = 5, Name = "Nivel Uno" },
                new Brand() { ID = 6, Name = "Mistral" },
                new Brand() { ID = 7, Name = "Prototype" },
                new Brand() { ID = 8, Name = "Prune" },
                new Brand() { ID = 9, Name = "Dotan Vision" },
                new Brand() { ID = 10, Name = "Union Pacific - Tiffany" }
            );
            
            modelBuilder.Entity<Seller>().HasData(
                new Seller() { ID = 1, Name = "Gina", Surname = "Gaido" },
                new Seller() { ID = 2, Name = "Lucy", Surname = "Gaido" }
            );

            modelBuilder.Entity<Doctor>().HasData(
                new Doctor() { ID = 1, Name = "Fernando", Surname = "Roman", License = "5981" },
                new Doctor() { ID = 2, Name = "Fabiani", Surname = "Rosana", License = "11095" },
                new Doctor() { ID = 3, Name = "Casabianca", Surname = "Gonzalo", License = "4165" },
                new Doctor() { ID = 4, Name = "Lopez", Surname = "German", License = "8440" },
                new Doctor() { ID = 5, Name = "Dominguez", Surname = "Jose Luis", License = "22251" }
            );

            modelBuilder.Entity<HealthInsurance>().HasData(
                new HealthInsurance() { ID = 1, Name = "IAPOS" },
                new HealthInsurance() { ID = 2, Name = "OSDE" },
                new HealthInsurance() { ID = 3, Name = "PAMI" },
                new HealthInsurance() { ID = 4, Name = "JERÁRQUICO SALUD" },
                new HealthInsurance() { ID = 5, Name = "SANCOR" },
                new HealthInsurance() { ID = 6, Name = "CAJA INGENIERÍA" },
                new HealthInsurance() { ID = 7, Name = "OSPAC" },
                new HealthInsurance() { ID = 8, Name = "OSPIL - CAJA AYUDA MUTTUA" },
                new HealthInsurance() { ID = 9, Name = "UNL" }
            );

            modelBuilder.Entity<Material>().HasData(
                new Material() { ID = 1, Description = "Metal" },
                new Material() { ID = 2, Description = "Plástico" },
                new Material() { ID = 3, Description = "Madera" }
            );

            modelBuilder.Entity<Provider>().HasData(
                new Provider() { ID = 1, Name = "Humah" },
                new Provider() { ID = 2, Name = "Vulk" },
                new Provider() { ID = 3, Name = "Rusty" },
                new Provider() { ID = 4, Name = "Hardem" },
                new Provider() { ID = 5, Name = "Nivel Uno" },
                new Provider() { ID = 6, Name = "Vision Planet" },
                new Provider() { ID = 7, Name = "Dotan Vision" },
                new Provider() { ID = 8, Name = "Union Pacific - Tiffany" }
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

            modelBuilder.Entity<GlassFocusType>().HasData(
                new GlassFocusType() { ID = 1, Name = "Monofocal" },
                new GlassFocusType() { ID = 2, Name = "Bifocal" },
                new GlassFocusType() { ID = 3, Name = "Multifocal" }
            );

            modelBuilder.Entity<PaymentMethod>().HasData(
                new PaymentMethod() { ID = 1, Name = "Efectivo" },
                new PaymentMethod() { ID = 2, Name = "Transferencia" },
                new PaymentMethod() { ID = 3, Name = "Tarjetas Nacionales" },
                new PaymentMethod() { ID = 4, Name = "Tarjeta Mutual Central" },
                new PaymentMethod() { ID = 5, Name = "Tarjeta Mutual Argentino" }
            );
        }
    }

}
