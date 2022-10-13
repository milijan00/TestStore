using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestStore.Domain;

namespace TestStore.Implementation.DataAccess
{
    public class TestStoreDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartProduct> CartsProducts { get; set; }
        public DbSet<Specification> Specifications { get; set; }
        public DbSet<SpecificationValue> SpecificationsValues { get; set; }
        public DbSet<Usecase> Usecases { get; set; }
        public DbSet<RoleUsecase> RoleUsecases { get; set; }
        public DbSet<NavLink> NavLinks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
            modelBuilder.Entity<CartProduct>().HasKey(x => new { x.ChartId, x.ProductId });
            modelBuilder.Entity<CartProduct>().Property(x => x.Quantity).IsRequired();
            modelBuilder.Entity<CartProduct>().Property(x => x.UnitPrice).IsRequired();
            modelBuilder.Entity<ProductPrice>().HasKey(x => new { x.ProductId, x.Value });
            modelBuilder.Entity<ProductPrice>().Property(x => x.CreatedAt).HasDefaultValueSql("GETDATE()");
            modelBuilder.Entity<ProductPrice>().Property(x => x.IsActive).HasDefaultValue(true);
            modelBuilder.Entity<ProductSpecification>().HasKey(x => new { x.ProductId, x.SpecificationId });
            modelBuilder.Entity<SpecificationValue>().HasKey(x => new { x.SpecificationId, x.Value });
            modelBuilder.Entity<RoleUsecase>().HasKey(x => new { x.RoleId, x.UsecaseId });
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-39RJT5L\SQLEXPRESS;Database=TestStore;Trusted_Connection=true;");
        }
    }
}
