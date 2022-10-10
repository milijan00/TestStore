using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestStore.Domain;

namespace TestStore.Implementation.DataAccess.Configurations
{
    public class SpecificationConfiguration : EntityConfiguration<Specification>
    {
        public override void ApplyChildConfiguration(EntityTypeBuilder<Specification> builder)
        {
            builder.Property(x => x.Name).HasMaxLength(30).IsRequired();

            builder.HasIndex(x => x.Name).IsUnique();

            builder.HasMany(x => x.SpecificationValues)
                .WithOne(x => x.Specification)
                .HasForeignKey(x => x.SpecificationId)
                .OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.Cascade);
        }
    }
}
