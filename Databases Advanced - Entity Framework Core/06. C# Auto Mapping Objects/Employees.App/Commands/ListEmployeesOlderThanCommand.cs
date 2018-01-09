namespace Employees.App.Commands
{
    using System;
    using System.Linq;
    using System.Text;
    using AutoMapper.QueryableExtensions;
    using Employees.App.DTO;
    using Employees.Data;

    class ListEmployeesOlderThanCommand : ICommand
    {
        public string Execute(params string[] args)
        {
            var age = int.Parse(args[0]);
            var sb = new StringBuilder();
            using (var db = new EmployeesContext())
            {
                var employees = db.Employees
                    .Where(a => a.Birthday != null && Math.Ceiling((DateTime.Now - a.Birthday.Value).TotalDays / 365.2422) > age)
                    .ProjectTo<EmployeeDTO>()
                    .ToArray();
                foreach (var employee in employees)
                {
                    sb.AppendLine(employee.ToString());
                }
            }
            return sb.ToString().Trim();
        }
    }
}
