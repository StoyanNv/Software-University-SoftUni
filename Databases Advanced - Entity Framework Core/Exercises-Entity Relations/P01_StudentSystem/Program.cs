namespace P01_StudentSystem
{
    using System;
    using Microsoft.EntityFrameworkCore;
    using P01_StudentSystem.Data.Models;
    class Program
    {
        static void Main()
        {

            using (var context = new StudentSystemContext())
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                context.Database.Migrate();
            }
        }
    }
}
