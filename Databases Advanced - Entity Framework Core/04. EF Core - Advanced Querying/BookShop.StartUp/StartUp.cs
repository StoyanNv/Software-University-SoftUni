using System;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using BookShop.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace BookShop
{
    using BookShop.Data;
    using BookShop.Initializer;

    public class StartUp
    {
        static void Main()
        {
            using (var context = new BookShopContext())
            {
                //DbInitializer.ResetDatabase(db);

                // Console.WriteLine(GetBooksByAgeRestriction(context)); ;// 1. Age Restriction
                // Console.WriteLine(GetGoldenBooks(context)); // 2. Golden Books
                // Console.WriteLine(GetBooksByPrice(context)); ; // 3. Books by Price
                // Console.WriteLine(GetBooksNotRealeasedIn(context)); ; // 4. Not Released In
                // Console.WriteLine(GetBooksByCategory(context)); ; // 5. Book Titles by Category
                // Console.WriteLine(GetBooksReleasedBefore(context)); ; // 6. Released Before Date
                // Console.WriteLine(GetAuthorNamesEndingIn(context)); // 7. Author Search
                // Console.WriteLine(GetBookTitlesContaining(context)); // 8. Book Search
                // Console.WriteLine(GetBooksByAuthor(context)); // 9. Book Search by Author
                // Console.WriteLine(CountBooks(context)); // 10. Count Books
                // Console.WriteLine(CountCopiesByAuthor(context)); // 11. Total Book Copies
                // Console.WriteLine(GetTotalProfitByCategory(context)); // 12. Profit by Category
                // Console.WriteLine(GetMostRecentBooks(context)); // 13. Most Recent Books
                // Console.WriteLine(IncreasePrices(context)); // 14. Increase Prices
                Console.WriteLine(RemoveBooks(context)); // 15. Remove Books
            }
        }

        public static int RemoveBooks(BookShopContext context)
        {
            var booksToRemuve = context.Books.Where(x => x.Copies < 4200);
            var result = booksToRemuve.Count();
            context.Books.RemoveRange(booksToRemuve);
            context.SaveChanges();

            return result;
        }

        public static int IncreasePrices(BookShopContext context)
        {
            var books = context.Books.Where(x => x.ReleaseDate.Value.Year < 2010).ToArray();

            foreach (var book in books)
            {
                book.Price += 5;
            }
            return context.SaveChanges();
        }

        public static string GetMostRecentBooks(BookShopContext context)
        {
            return "--" + string.Join(Environment.NewLine + "--", context.Categories
                .OrderBy(x => x.Name)
                .Select(x => $"{x.Name}{Environment.NewLine}{string.Join(Environment.NewLine, x.CategoryBooks.OrderByDescending(y => y.Book.ReleaseDate).Select(y => $"{y.Book.Title} ({y.Book.ReleaseDate.Value.Year})").Take(3))}"));
        }

        public static string GetTotalProfitByCategory(BookShopContext context)
        {
            return string.Join(Environment.NewLine,
                context.Categories
                    .OrderByDescending(x => x.CategoryBooks.Select(y => y.Book.Copies * y.Book.Price).Sum())
                    .Select(x => $"{x.Name} ${x.CategoryBooks.Select(y => y.Book.Copies * y.Book.Price).Sum()}"));
        }

        public static string CountCopiesByAuthor(BookShopContext context)
        {
            return string.Join(Environment.NewLine, context.Authors
                .OrderByDescending(x => x.Books.Sum(y => y.Copies))
                .Select(x => $"{x.FirstName} {x.LastName} - {x.Books.Sum(y => y.Copies)}"));
        }

        public static int CountBooks(BookShopContext context, int? lengthCheck = null)
        {
            if (lengthCheck == null)
            {
                lengthCheck = int.Parse(Console.ReadLine());
            }
            return context.Books.Where(x => x.Title.Length > lengthCheck).Count();
        }

        public static string GetBooksByAuthor(BookShopContext context, string input = null)
        {
            if (input == null)
            {
                input = Console.ReadLine();
            }
            return string.Join(Environment.NewLine,
                context.Books
                .Where(x => x.Author.LastName.ToLower().StartsWith(input.ToLower()))
                .OrderBy(x => x.BookId)
                .Select(x => $"{x.Title} ({x.Author.FirstName} {x.Author.LastName})"));
        }

        public static string GetBookTitlesContaining(BookShopContext context, string input = null)
        {
            if (input == null)
            {
                input = Console.ReadLine();
            }
            return string.Join(Environment.NewLine, context.Books
                .Where(x => x.Title.ToLower().Contains(input.ToLower()))
                .OrderBy(x => x.Title)
                .Select(x => x.Title));
        }

        public static string GetAuthorNamesEndingIn(BookShopContext context, string input = null)
        {
            if (input == null)
            {
                input = Console.ReadLine();
            }
            return string.Join(Environment.NewLine, context.Authors
                .Where(x => x.FirstName.EndsWith(input))
                .OrderBy(x => x.FirstName + x.LastName)
                .Select(x => $"{x.FirstName} {x.LastName}"));
        }

        public static string GetBooksReleasedBefore(BookShopContext context, string date = null)
        {
            if (date == null)
            {
                date = Console.ReadLine();
            }
            return string.Join(Environment.NewLine, context.Books
                .Where(x => x.ReleaseDate < DateTime.ParseExact(date, "dd-MM-yyyy", CultureInfo.InvariantCulture))
                .OrderByDescending(x => x.ReleaseDate)
                .Select(x => $"{x.Title} - {x.EditionType} - ${x.Price:f2}"));
        }

        public static string GetBooksByCategory(BookShopContext context, string input = null)
        {
            if (input == null)
            {
                input = Console.ReadLine();
            }
            var category = input.ToLower().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            return string.Join(Environment.NewLine, context.Books
                .Where(x => x.BookCategories
                    .Select(y => y.Category.Name.ToLower())
                    .Intersect(category)
                    .Any())
                .Select(x => x.Title)
                .OrderBy(x => x));
        }

        public static string GetBooksNotRealeasedIn(BookShopContext context, int? year = null)
        {
            if (year == null)
            {
                year = int.Parse(Console.ReadLine());
            }

            return string.Join(Environment.NewLine, context.Books
                .Where(x => x.ReleaseDate.Value.Year != year)
                .OrderBy(x => x.BookId)
                .Select(x => x.Title));
        }

        public static string GetBooksByPrice(BookShopContext context)
        {
            return string.Join(Environment.NewLine, context.Books
                .Where(b => b.Price > 40)
                .OrderByDescending(b => b.Price)
                .Select(b => $"{b.Title} - ${b.Price:F2}"));
        }

        public static string GetGoldenBooks(BookShopContext context)
        {
            return string.Join(Environment.NewLine, context.Books.Where(x => x.EditionType == EditionType.Gold && x.Copies < 5000).OrderBy(x => x.BookId)
                .Select(x => x.Title).ToArray());
        }

        public static string GetBooksByAgeRestriction(BookShopContext context, string command = null)
        {
            if (command == null)
            {
                command = Console.ReadLine();
            }

            return string.Join(Environment.NewLine, context.Books
                .Where(b => b.AgeRestriction.ToString().Equals(command, StringComparison.OrdinalIgnoreCase))
                .Select(b => b.Title)
                .OrderBy(t => t));
        }
    }
}
