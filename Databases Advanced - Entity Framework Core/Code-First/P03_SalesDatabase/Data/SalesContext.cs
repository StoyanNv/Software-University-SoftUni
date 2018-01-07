namespace P03_SalesDatabase.Data
{
    using Microsoft.EntityFrameworkCore;
    using P03_SalesDatabase.Data.Models;

    class SalesContext : DbContext
    {
        public SalesContext() { }
        public SalesContext(DbContextOptions options)
            : base(options) { }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<Store> Stores { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration.ConnectionString);
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.ProductId);

                entity.Property(e => e.ProductId).IsRequired();

                entity.Property(e => e.Description)
                    .HasMaxLength(250)
                    .IsUnicode(true)
                    .IsRequired(true)
                    .HasDefaultValue("No description");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .IsUnicode()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.CustomerId);

                entity.Property(e => e.CustomerId).IsRequired();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .IsUnicode()
                    .HasMaxLength(100);
                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(80);
            });

            modelBuilder.Entity<Store>(entity =>
            {
                entity.HasKey(e => e.StoreId);

                entity.Property(e => e.StoreId).IsRequired();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .IsUnicode()
                    .HasMaxLength(80);
            });

            modelBuilder.Entity<Sale>(entity =>
            {
                entity.HasKey(e => e.SaleId);

                entity.Property(e => e.Date)
                    .HasDefaultValueSql("GETDATE()");

                entity.Property(e => e.SaleId)
                    .IsRequired(true);

                entity.Property(e => e.ProductId)
                    .IsRequired(true);

                entity.HasOne(e => e.Product)
                    .WithMany(m => m.Sales)
                    .HasForeignKey(e => e.ProductId);

                entity.Property(e => e.StoreId)
                    .IsRequired(true);

                entity.HasOne(e => e.Customer)
                    .WithMany(m => m.Sales)
                    .HasForeignKey(e => e.CustomerId);

                entity.Property(e => e.StoreId)
                    .IsRequired(true);

                entity.HasOne(e => e.Store)
                    .WithMany(m => m.Sales)
                    .HasForeignKey(e => e.StoreId);
            });
        }
    }
}
