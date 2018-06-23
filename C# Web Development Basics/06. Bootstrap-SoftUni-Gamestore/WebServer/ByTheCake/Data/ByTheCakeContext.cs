namespace HTTPServer.ByTheCake.Data
{
    using Models;
    using Microsoft.EntityFrameworkCore;

    public class ByTheCakeContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<ProductOrder> ProductOrders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=DESKTOP-V3JOVJU\SQLEXPRESS;Database=ByTheCakeDb;Integrated Security=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasMany(p => p.Orders)
                .WithOne(o => o.Product)
                .HasForeignKey(po => po.ProductId);

            modelBuilder.Entity<Order>()
                .HasMany(o => o.Products)
                .WithOne(p => p.Order)
                .HasForeignKey(po => po.OrderId);

            modelBuilder.Entity<User>()
                .HasMany(o => o.Orders);

            modelBuilder.Entity<Order>()
                .HasOne(u => u.User);

            modelBuilder
                .Entity<ProductOrder>()
                .HasKey(po => new
                {
                    po.ProductId,
                    po.OrderId
                });
        }
    }
}