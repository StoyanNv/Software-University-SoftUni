namespace BookLibrary.Data
{
    using Models;
    using Microsoft.EntityFrameworkCore;

    public class BookLibraryContext : DbContext
    {
        public BookLibraryContext(DbContextOptions<BookLibraryContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Borrower> Borrowers { get; set; }
        public DbSet<BorrowersBooks> BorrowedBooks { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Director> Directors { get; set; }
        public DbSet<BorrowersMovies> BorrowedMovies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();

            modelBuilder.Entity<Book>()
                .HasMany(book => book.Borrowers)
                .WithOne(borrower => borrower.Book)
                .HasForeignKey(b => b.BookId);

            modelBuilder.Entity<Borrower>()
                .HasMany(borrower => borrower.BorrowedBooks)
                .WithOne(book => book.Borrower)
                .HasForeignKey(b => b.BorrowerId);

            modelBuilder.Entity<Movie>()
                .HasMany(movie => movie.Borrowers)
                .WithOne(borrower => borrower.Movie)
                .HasForeignKey(b => b.MovieId);

            modelBuilder.Entity<Borrower>()
                .HasMany(borrower => borrower.BorrowedMovies)
                .WithOne(movie => movie.Borrower)
                .HasForeignKey(b => b.BorrowerId);
            base.OnModelCreating(modelBuilder);
        }
    }
}