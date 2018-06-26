namespace SimpleMvc.Data
{
    using Domain;
    using Microsoft.EntityFrameworkCore;

    public class NotesDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Note> Notes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=DESKTOP-V3JOVJU\\SQLEXPRESS;Database=Notes;Integrated Security=True;");
            }
            base.OnConfiguring(optionsBuilder);
        }
    }
}