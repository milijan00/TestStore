using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestStore.Domain;

namespace TestStore.Implementation.DataAccess.Configurations
{
    public class ChartConfiguration : EntityConfiguration<Cart>
    {
        public override void ApplyChildConfiguration(EntityTypeBuilder<Cart> builder)
        {
            builder.Property(x => x.UserId).IsRequired();

            builder.HasMany(c => c.Products)
                .WithOne(x => x.Chart)
                .HasForeignKey(x => x.ChartId)
                .OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.Cascade);
        }
    }
}
