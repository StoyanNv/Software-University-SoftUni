namespace HTTPServer.GameStore.Data
{
    using Models;
    using Microsoft.EntityFrameworkCore;

    public class GameStoreContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<GameOrder> GameOrders { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=DESKTOP-V3JOVJU\SQLEXPRESS;Database=GameStoreDb;Integrated Security=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(e => e.Email)
                .IsUnique();

            modelBuilder.Entity<Game>()
                .HasMany(p => p.Orders)
                .WithOne(o => o.Game)
                .HasForeignKey(po => po.GameId);

            modelBuilder.Entity<Order>()
                .HasMany(o => o.Games)
                .WithOne(p => p.Order)
                .HasForeignKey(po => po.OrderId);

            modelBuilder.Entity<User>()
                .HasMany(o => o.Orders);

            modelBuilder
                .Entity<GameOrder>()
                .HasKey(po => new
                {
                    po.GameId,
                    po.OrderId
                });
        }
    }
}