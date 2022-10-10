using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestStore.Domain;

namespace TestStore.Implementation.DataAccess.Configurations
{
    public class UsecaseConfiguration : EntityConfiguration<Usecase>
    {
        public override void ApplyChildConfiguration(EntityTypeBuilder<Usecase> builder)
        {
            builder.Property(x => x.Name).HasMaxLength(100).IsRequired();

            builder.HasIndex(x => x.Name).IsUnique();

            builder.HasMany(x => x.Roles)
                .WithOne(x => x.Usecase)
                .HasForeignKey(x => x.UsecaseId)
                .OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.Cascade);
        }
    }
}
