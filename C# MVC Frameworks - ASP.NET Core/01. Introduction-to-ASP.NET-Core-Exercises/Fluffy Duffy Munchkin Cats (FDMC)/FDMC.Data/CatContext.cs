namespace FDMC.Data
{
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class CatContext : DbContext
    {
        public DbSet<Cat> Cats { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.;Database=Cats_MVC;Integrated Security=True;");
            }
            base.OnConfiguring(optionsBuilder);
        }
    }
}