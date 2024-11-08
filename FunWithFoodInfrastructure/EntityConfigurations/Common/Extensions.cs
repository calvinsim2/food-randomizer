using FunWithFoodDomain.Models.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FunWithFoodInfrastructure.EntityConfigurations.Common
{
    public static class Extensions
    {
        public static void ConfigureBaseEntity<T>(EntityTypeBuilder<T> builder) where T : BaseEntity
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .IsRequired(true)
                .HasColumnType("UniqueIdentifier")
                .HasDefaultValueSql("newsequentialId()");

        }
    }
}
