namespace P03_SalesDatabase
{
    using P03_SalesDatabase.Data;

    class Program
    {
        static void Main()
        {
            using (var db = new SalesContext())
            {
                db.Database.EnsureCreated();
            }
        }
    }
}
