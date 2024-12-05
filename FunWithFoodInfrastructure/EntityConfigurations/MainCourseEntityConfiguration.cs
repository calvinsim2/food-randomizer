﻿using FunWithFoodDomain.Models;
using FunWithFoodInfrastructure.EntityConfigurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FunWithFoodInfrastructure.EntityConfigurations
{
    public class MainCourseEntityConfiguration : IEntityTypeConfiguration<MainCourse>
    {
        public void Configure(EntityTypeBuilder<MainCourse> builder)
        {
            Extensions.ConfigureBaseEntity(builder);

            builder.Property(e => e.CuisineId)
                .HasColumnType("UniqueIdentifier")
                .IsRequired(true);

            builder.Property(e => e.Name)
                .IsRequired(true)
                .HasColumnType("nvarchar(max)");

            builder.Property(e => e.ImageData)
                .HasColumnType("VARBINARY(MAX)");
        }
    }
}