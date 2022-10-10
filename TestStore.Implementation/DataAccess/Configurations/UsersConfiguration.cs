using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestStore.Domain;

namespace TestStore.Implementation.DataAccess.Configurations
{
    public class UsersConfiguration : EntityConfiguration<User>
    {
        public override void ApplyChildConfiguration(EntityTypeBuilder<User> builder)
        {
            builder.Property(x => x.FirstName).HasMaxLength(30).IsRequired();
            builder.Property(x => x.LastName).HasMaxLength(30).IsRequired();
            builder.Property(x => x.Email).HasMaxLength(100).IsRequired();
            builder.Property(x => x.Username).HasMaxLength(30).IsRequired();
            builder.Property(x => x.Password).HasMaxLength(100).IsRequired();

            builder.HasIndex(x => x.Email).IsUnique();
            builder.HasIndex(x => x.Username).IsUnique();

            builder.HasOne(x => x.Chart)
                .WithOne(x => x.User)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
