using System;
using System.Linq;
using AutoMapper.QueryableExtensions;
using Employees.App.DTO;
using Employees.Data;

namespace Employees.App.Commands
{
    class ManagerInfoCommand:ICommand
    {
        public string Execute(params string[] args)
        {
            var id = int.Parse(args[0]);
            using (var db = new EmployeesContext())
            {
                var manager = db.Employees
                    .ProjectTo<ManagerDTO>()
                    .SingleOrDefault(e => e.Id == id);
                return manager.ToString();
            }
        }
    }
}
