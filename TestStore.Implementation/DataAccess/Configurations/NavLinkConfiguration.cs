using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestStore.Domain;

namespace TestStore.Implementation.DataAccess.Configurations
{
    public class NavLinkConfiguration : EntityConfiguration<NavLink>
    {
        public override void ApplyChildConfiguration(EntityTypeBuilder<NavLink> builder)
        {
            builder.Property(x => x.Name).IsRequired().HasMaxLength(30);
            builder.Property(x => x.Action).IsRequired().HasMaxLength(30);
            builder.Property(x => x.Controller).IsRequired().HasMaxLength(50);

            builder.HasIndex(x => x.Name).IsUnique();
        }
    }
}
