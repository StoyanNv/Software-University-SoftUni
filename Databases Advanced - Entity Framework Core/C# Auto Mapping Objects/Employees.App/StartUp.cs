namespace Employees.App
{
    using System;
    using System.Linq;
    using AutoMapper;
    using Employees.Data;
    class StartUp
    {
        static void Main()
        {
           // ResetDatabase();
            InitializeMapper();

            while (true)
            {
                var input = Console.ReadLine().Split(' ').ToArray();
                var commandParser = new CommandParser();
                var command = commandParser.DispatchCommand(input);
                Console.WriteLine(command);
            }
        }
        private static void InitializeMapper()
        {
            Mapper.Initialize(cfg => cfg.AddProfile<MapperProfile>());
        }
        private static void ResetDatabase()
        {
            using (var db = new EmployeesContext())
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();
            }
        }
    }
}