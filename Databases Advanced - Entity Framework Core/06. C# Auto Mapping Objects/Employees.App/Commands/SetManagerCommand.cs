namespace Employees.App.Commands
{
    using System.Linq;
    using Employees.Data;
    class SetManagerCommand : ICommand
    {
        public string Execute(params string[] args)
        {
            using (var context = new EmployeesContext())
            {
                var employee = context.Employees
                    .SingleOrDefault(e => e.Id == int.Parse(args[0]));
                var manager = context.Employees
                    .SingleOrDefault(e => e.Id == int.Parse(args[1]));

                employee.Manager = manager;
                context.SaveChanges();
            }
            return "Done";
        }
    }
}
