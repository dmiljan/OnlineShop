using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Models;
using Attribute = OnlineShop.Models.Attribute;

namespace OnlineShop.Data
{
    public class DataContext : IdentityDbContext<User>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<ProductType> ProductType { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderProduct> OrderProduct { get; set; } //Order-Product n:m
        public DbSet<UserProductSave> UserProductSave { get; set; } //User-Product n:m
        public DbSet<ProductImage> ProductImage { get; set; }
        public DbSet<Attribute> Attribute { get; set; }
        public DbSet<ProductTypeAttribute> ProductTypeAttribute { get; set; } //ProductType-Attribute n:m
        public DbSet<AttributeValue> AttributeValue { get; set; }
        public DbSet<ProductTypeAttributeValue> ProductTypeAttributeValue { get; set; } //ProductType-AttributeValue n:m
        public DbSet<ProductAttribute> ProductAttribute { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<OrderProduct>() //n:m
                .HasKey(x => new {x.OrderId, x.ProductId});

            modelBuilder.Entity<UserProductSave>() //n:m
                .HasKey(x => new {x.UserId, x.ProductId});

            modelBuilder.Entity<ProductTypeAttribute>() //n:m
                .HasKey(x => new { x.ProductTypeId, x.AttributeId });

            modelBuilder.Entity<ProductTypeAttributeValue>() //n:m
               .HasKey(x => new { x.ProductTypeId, x.AttributeValueId });
        }
    }
}
