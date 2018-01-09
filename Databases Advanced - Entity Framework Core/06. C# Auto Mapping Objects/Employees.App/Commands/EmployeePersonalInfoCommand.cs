namespace Employees.App.Commands
{
    using System.Text;
    using Employees.Data;
    class EmployeePersonalInfoCommand : ICommand
    {
        public string Execute(params string[] args)
        {
            var sb = new StringBuilder();
            var id = int.Parse(args[0]);

            using (var db = new EmployeesContext())
            {
                var employee = db.Employees.Find(id);
                sb.AppendLine($"ID: {employee.Id} - {employee.FirstName} {employee.LastName} - ${employee.Salary:F2}")
                    .Append("Birthday: ")
                    .AppendLine(employee.Birthday == null
                        ? string.Empty
                        : employee.Birthday.Value.ToString("dd-MM-yyyy"))
                    .Append("Address: ")
                    .AppendLine(employee.Address == null
                        ? string.Empty
                        : employee.Address);
            }
            return sb.ToString();
        }
    }
}
