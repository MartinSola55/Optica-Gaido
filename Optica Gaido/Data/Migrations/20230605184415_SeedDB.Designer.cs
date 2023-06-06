﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Optica_Gaido.Data;

#nullable disable

namespace Optica_Gaido.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230605184415_SeedDB")]
    partial class SeedDB
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Optica_Gaido.Models.Brand", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("ID"));

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("ID");

                    b.ToTable("Brands");

                    b.HasData(
                        new
                        {
                            ID = 1L,
                            IsActive = true,
                            Name = "RayBan"
                        },
                        new
                        {
                            ID = 2L,
                            IsActive = true,
                            Name = "Vulk"
                        },
                        new
                        {
                            ID = 3L,
                            IsActive = true,
                            Name = "UP!"
                        },
                        new
                        {
                            ID = 4L,
                            IsActive = true,
                            Name = "Oakley"
                        });
                });

            modelBuilder.Entity("Optica_Gaido.Models.Client", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("ID"));

                    b.Property<string>("Adress")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Debt")
                        .HasColumnType("money");

                    b.Property<short?>("HealthInsuranceID")
                        .HasColumnType("smallint");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(12)
                        .HasColumnType("nvarchar(12)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("ID");

                    b.HasIndex("HealthInsuranceID");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("Optica_Gaido.Models.Doctor", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("ID"));

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("License")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("ID");

                    b.ToTable("Doctors");

                    b.HasData(
                        new
                        {
                            ID = 1L,
                            IsActive = true,
                            License = "123123",
                            Name = "Fernando",
                            Surname = "Sola"
                        },
                        new
                        {
                            ID = 2L,
                            IsActive = true,
                            License = "456456",
                            Name = "Vanina",
                            Surname = "Chalita"
                        },
                        new
                        {
                            ID = 3L,
                            IsActive = true,
                            License = "789789",
                            Name = "Ana María",
                            Surname = "Bertinat"
                        });
                });

            modelBuilder.Entity("Optica_Gaido.Models.Expense", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("ID"));

                    b.Property<decimal>("Amount")
                        .HasColumnType("money");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("ID");

                    b.ToTable("Expenses");
                });

            modelBuilder.Entity("Optica_Gaido.Models.Frame", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("ID"));

                    b.Property<long>("BrandID")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<long>("MaterialID")
                        .HasColumnType("bigint");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("ID");

                    b.HasIndex("BrandID");

                    b.HasIndex("MaterialID");

                    b.ToTable("Frames");
                });

            modelBuilder.Entity("Optica_Gaido.Models.GlassColor", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("ID"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("ID");

                    b.ToTable("GlassColors");

                    b.HasData(
                        new
                        {
                            ID = 1L,
                            Name = "Blanco"
                        },
                        new
                        {
                            ID = 2L,
                            Name = "Tinte 50"
                        },
                        new
                        {
                            ID = 3L,
                            Name = "Foto Grey"
                        },
                        new
                        {
                            ID = 4L,
                            Name = "Hih H. Lite Blanco"
                        },
                        new
                        {
                            ID = 5L,
                            Name = "Hih H. Lite Sepia"
                        });
                });

            modelBuilder.Entity("Optica_Gaido.Models.GlassFormat", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("ID"));

                    b.Property<decimal>("Axis")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Cilindric")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("Distance")
                        .HasColumnType("int");

                    b.Property<decimal>("Esferic")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Eye")
                        .HasColumnType("int");

                    b.Property<long>("SaleID")
                        .HasColumnType("bigint");

                    b.HasKey("ID");

                    b.HasIndex("SaleID");

                    b.ToTable("GlassFormats");
                });

            modelBuilder.Entity("Optica_Gaido.Models.GlassType", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("ID"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("ID");

                    b.ToTable("GlassTypes");

                    b.HasData(
                        new
                        {
                            ID = 1L,
                            Name = "Orgánico"
                        },
                        new
                        {
                            ID = 2L,
                            Name = "Mineral"
                        });
                });

            modelBuilder.Entity("Optica_Gaido.Models.HealthInsurance", b =>
                {
                    b.Property<short>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("smallint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<short>("ID"));

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("ID");

                    b.ToTable("HealthInsurances");

                    b.HasData(
                        new
                        {
                            ID = (short)1,
                            IsActive = true,
                            Name = "IAPOS"
                        },
                        new
                        {
                            ID = (short)2,
                            IsActive = true,
                            Name = "OSDE"
                        },
                        new
                        {
                            ID = (short)3,
                            IsActive = true,
                            Name = "PAMI"
                        },
                        new
                        {
                            ID = (short)4,
                            IsActive = true,
                            Name = "OSECAC"
                        });
                });

            modelBuilder.Entity("Optica_Gaido.Models.Material", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("ID"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.HasKey("ID");

                    b.ToTable("Materials");

                    b.HasData(
                        new
                        {
                            ID = 1L,
                            Description = "Metal",
                            IsActive = true
                        },
                        new
                        {
                            ID = 2L,
                            Description = "Plástico",
                            IsActive = true
                        },
                        new
                        {
                            ID = 3L,
                            Description = "Madera",
                            IsActive = true
                        });
                });

            modelBuilder.Entity("Optica_Gaido.Models.PaymentMethod", b =>
                {
                    b.Property<short>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("smallint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<short>("ID"));

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("ID");

                    b.ToTable("PaymentMethods");

                    b.HasData(
                        new
                        {
                            ID = (short)1,
                            IsActive = true,
                            Name = "Efectivo"
                        },
                        new
                        {
                            ID = (short)2,
                            IsActive = true,
                            Name = "Transferencia"
                        },
                        new
                        {
                            ID = (short)3,
                            IsActive = true,
                            Name = "Débito"
                        },
                        new
                        {
                            ID = (short)4,
                            IsActive = true,
                            Name = "Débito (local)"
                        },
                        new
                        {
                            ID = (short)5,
                            IsActive = true,
                            Name = "Crédito"
                        },
                        new
                        {
                            ID = (short)6,
                            IsActive = true,
                            Name = "Crédito (local)"
                        });
                });

            modelBuilder.Entity("Optica_Gaido.Models.Provider", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("ID"));

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("ID");

                    b.ToTable("Providers");

                    b.HasData(
                        new
                        {
                            ID = 1L,
                            Name = "Lionel Andrés",
                            Surname = "Messi"
                        },
                        new
                        {
                            ID = 2L,
                            Name = "Andrés",
                            Surname = "Iniesta"
                        },
                        new
                        {
                            ID = 3L,
                            Name = "Gabriel",
                            Surname = "Batistuta"
                        },
                        new
                        {
                            ID = 4L,
                            Name = "Diego Armando",
                            Surname = "Maradona"
                        });
                });

            modelBuilder.Entity("Optica_Gaido.Models.Sale", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("ID"));

                    b.Property<long>("ClientID")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DeliveryDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal?>("Deposit")
                        .HasColumnType("money");

                    b.Property<string>("Dip")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<long>("DoctorID")
                        .HasColumnType("bigint");

                    b.Property<long>("FrameID")
                        .HasColumnType("bigint");

                    b.Property<long>("GlassColorID")
                        .HasColumnType("bigint");

                    b.Property<long>("GlassTypeID")
                        .HasColumnType("bigint");

                    b.Property<bool>("IsAr")
                        .HasColumnType("bit");

                    b.Property<decimal>("MovieHeight")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Observation")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<decimal>("Price")
                        .HasColumnType("money");

                    b.Property<short>("SellerID")
                        .HasColumnType("smallint");

                    b.HasKey("ID");

                    b.HasIndex("ClientID");

                    b.HasIndex("DoctorID");

                    b.HasIndex("FrameID");

                    b.HasIndex("GlassColorID");

                    b.HasIndex("GlassTypeID");

                    b.HasIndex("SellerID");

                    b.ToTable("Sales");
                });

            modelBuilder.Entity("Optica_Gaido.Models.SalePaymentMethod", b =>
                {
                    b.Property<long>("SaleID")
                        .HasColumnType("bigint");

                    b.Property<short>("PaymentMethodID")
                        .HasColumnType("smallint");

                    b.Property<decimal>("Amount")
                        .HasColumnType("money");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("SaleID", "PaymentMethodID");

                    b.HasIndex("PaymentMethodID");

                    b.ToTable("SalePaymentMethods");
                });

            modelBuilder.Entity("Optica_Gaido.Models.Seller", b =>
                {
                    b.Property<short>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("smallint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<short>("ID"));

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("ID");

                    b.ToTable("Sellers");

                    b.HasData(
                        new
                        {
                            ID = (short)1,
                            IsActive = true,
                            Name = "Gina",
                            Surname = "Gaido"
                        },
                        new
                        {
                            ID = (short)2,
                            IsActive = true,
                            Name = "Lucy",
                            Surname = "Gaido"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Optica_Gaido.Models.Client", b =>
                {
                    b.HasOne("Optica_Gaido.Models.HealthInsurance", "HealthInsurance")
                        .WithMany()
                        .HasForeignKey("HealthInsuranceID");

                    b.Navigation("HealthInsurance");
                });

            modelBuilder.Entity("Optica_Gaido.Models.Frame", b =>
                {
                    b.HasOne("Optica_Gaido.Models.Brand", "Brand")
                        .WithMany()
                        .HasForeignKey("BrandID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Optica_Gaido.Models.Material", "Material")
                        .WithMany()
                        .HasForeignKey("MaterialID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Brand");

                    b.Navigation("Material");
                });

            modelBuilder.Entity("Optica_Gaido.Models.GlassFormat", b =>
                {
                    b.HasOne("Optica_Gaido.Models.Sale", "Sale")
                        .WithMany("GlassFormats")
                        .HasForeignKey("SaleID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Sale");
                });

            modelBuilder.Entity("Optica_Gaido.Models.Sale", b =>
                {
                    b.HasOne("Optica_Gaido.Models.Client", "Client")
                        .WithMany()
                        .HasForeignKey("ClientID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Optica_Gaido.Models.Doctor", "Doctor")
                        .WithMany("Sales")
                        .HasForeignKey("DoctorID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Optica_Gaido.Models.Frame", "Frame")
                        .WithMany("Sales")
                        .HasForeignKey("FrameID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Optica_Gaido.Models.GlassColor", "GlassColor")
                        .WithMany("Sales")
                        .HasForeignKey("GlassColorID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Optica_Gaido.Models.GlassType", "GlassType")
                        .WithMany("Sales")
                        .HasForeignKey("GlassTypeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Optica_Gaido.Models.Seller", "Seller")
                        .WithMany("Sales")
                        .HasForeignKey("SellerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");

                    b.Navigation("Doctor");

                    b.Navigation("Frame");

                    b.Navigation("GlassColor");

                    b.Navigation("GlassType");

                    b.Navigation("Seller");
                });

            modelBuilder.Entity("Optica_Gaido.Models.SalePaymentMethod", b =>
                {
                    b.HasOne("Optica_Gaido.Models.PaymentMethod", "PaymentMethod")
                        .WithMany()
                        .HasForeignKey("PaymentMethodID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Optica_Gaido.Models.Sale", null)
                        .WithMany("SalePaymentMethods")
                        .HasForeignKey("SaleID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PaymentMethod");
                });

            modelBuilder.Entity("Optica_Gaido.Models.Doctor", b =>
                {
                    b.Navigation("Sales");
                });

            modelBuilder.Entity("Optica_Gaido.Models.Frame", b =>
                {
                    b.Navigation("Sales");
                });

            modelBuilder.Entity("Optica_Gaido.Models.GlassColor", b =>
                {
                    b.Navigation("Sales");
                });

            modelBuilder.Entity("Optica_Gaido.Models.GlassType", b =>
                {
                    b.Navigation("Sales");
                });

            modelBuilder.Entity("Optica_Gaido.Models.Sale", b =>
                {
                    b.Navigation("GlassFormats");

                    b.Navigation("SalePaymentMethods");
                });

            modelBuilder.Entity("Optica_Gaido.Models.Seller", b =>
                {
                    b.Navigation("Sales");
                });
#pragma warning restore 612, 618
        }
    }
}
