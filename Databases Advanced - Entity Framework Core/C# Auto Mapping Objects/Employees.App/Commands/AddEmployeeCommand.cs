namespace Employees.App.Commands
{
    using AutoMapper;
    using Employees.Data;
    using Employees.App.DTO;
    using Employees.Models.Models;

    class AddEmployeeCommand : ICommand
    {
        public string Execute(params string[] args)
        {
            var firstName = args[0];
            var lastName = args[1];
            var salary = decimal.Parse(args[2]);

            using (var db  = new EmployeesContext())
            {
                var dto = new EmployeeDTO(firstName, lastName, salary);
                var employee = Mapper.Map<Employee>(dto);

                db.Employees.Add(employee);
                db.SaveChanges();
            }
            return "Done";
        }
    }
}