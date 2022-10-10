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
    public abstract class EntityConfiguration<T> : IEntityTypeConfiguration<T> where T : Entity
    {
        public void Configure(EntityTypeBuilder<T> builder)
        {
            builder.Property(x => x.CreatedAt).IsRequired(true).HasDefaultValueSql("GETDATE()");
            builder.Property(x => x.IsActive).IsRequired(true).HasDefaultValue(true);
            this.ApplyChildConfiguration(builder);
        }
        public abstract void  ApplyChildConfiguration(EntityTypeBuilder<T> builder);
    }
}
