using FunWithFoodDomain.Models;
using FunWithFoodInfrastructure.EntityConfigurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FunWithFoodInfrastructure.EntityConfigurations
{
    public class CuisineEntityConfiguration : IEntityTypeConfiguration<Cuisine>
    {
        public void Configure(EntityTypeBuilder<Cuisine> builder)
        {
            Extensions.ConfigureBaseEntity(builder);

            builder.Property(e => e.Type)
                .IsRequired(true)
                .HasColumnType("nvarchar(max)");


        }
    }
}
