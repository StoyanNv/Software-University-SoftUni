namespace Employees.App.Commands
{
    using System;
    using System.Globalization;
    using Employees.Data;
    class SetBirthdayCommand : ICommand
    {
        public string Execute(params string[] args)
        {
            var employeeId = int.Parse(args[0]);
            DateTime date = DateTime.ParseExact(args[1],
                "dd-MM-yyyy",
                CultureInfo.InvariantCulture);
            using (var db = new EmployeesContext())
            {
                db.Employees.Find(employeeId).Birthday = date;
                db.SaveChanges();
            }

            return "Done";
        }
    }
}
