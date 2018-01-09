namespace Employees.App.Commands
{
    using System;
    using Employees.Data;
    using System.Linq;

    class SetAddressCommand : ICommand
    {
        public string Execute(params string[] args)
        {
            var id = int.Parse(args[0]);
            var address = string.Join(" ", args.Skip(1));
            using (var db = new EmployeesContext())
            {
                var employee = db.Employees.Find(id);
                employee.Address = address;
                db.SaveChanges();
            }
            return "Done";
        }
    }
}
