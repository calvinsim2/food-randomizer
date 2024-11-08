using FunWithFoodDomain.Models;
using FunWithFoodInfrastructure.EntityConfigurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FunWithFoodInfrastructure.EntityConfigurations
{
    public class AdminEntityConfiguration : IEntityTypeConfiguration<Admin>
    {
        public void Configure(EntityTypeBuilder<Admin> builder)
        {
            Extensions.ConfigureBaseEntity(builder);

            builder.Property(e => e.Username)
                .IsRequired(true)
                .HasColumnType("nvarchar(max)");

            builder.Property(e => e.PasswordHash)
                .IsRequired()
                .HasColumnType("varbinary(MAX)");

            builder.Property(e => e.PasswordSalt)
                .IsRequired()
                .HasColumnType("varbinary(MAX)");

        }
    }
}
