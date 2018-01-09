namespace P03_FootballBetting
{
    using Microsoft.EntityFrameworkCore;
    using P03_FootballBetting.Data;

    class Program
    {
        static void Main()
        {
            using (var context = new FootballBettingContext())
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                context.Database.Migrate();
            }
        }
    }
}
