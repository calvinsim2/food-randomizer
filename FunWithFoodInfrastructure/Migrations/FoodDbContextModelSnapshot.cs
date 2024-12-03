﻿// <auto-generated />
using System;
using FunWithFoodInfrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FunWithFoodInfrastructure.Migrations
{
    [DbContext(typeof(FoodDbContext))]
    partial class FoodDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("FunWithFoodDomain.Models.Admin", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("UniqueIdentifier")
                        .HasDefaultValueSql("newsequentialId()");

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("varbinary(MAX)");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("varbinary(MAX)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Admin");
                });

            modelBuilder.Entity("FunWithFoodDomain.Models.Cuisine", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("UniqueIdentifier")
                        .HasDefaultValueSql("newsequentialId()");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Cuisine");
                });

            modelBuilder.Entity("FunWithFoodDomain.Models.Dessert", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("UniqueIdentifier")
                        .HasDefaultValueSql("newsequentialId()");

                    b.Property<Guid>("CuisineId")
                        .HasColumnType("UniqueIdentifier");

                    b.Property<byte[]>("ImageData")
                        .HasColumnType("VARBINARY(MAX)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Dessert");
                });

            modelBuilder.Entity("FunWithFoodDomain.Models.Food", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("UniqueIdentifier")
                        .HasDefaultValueSql("newsequentialId()");

                    b.Property<Guid>("CuisineId")
                        .HasColumnType("UniqueIdentifier");

                    b.Property<byte[]>("ImageData")
                        .HasColumnType("VARBINARY(MAX)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Food");
                });
#pragma warning restore 612, 618
        }
    }
}
