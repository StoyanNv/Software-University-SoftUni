namespace P02_DatabaseFirst
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using P02_DatabaseFirst.Data.Models;

    class StartUp
    {
        static void Main()
        {
            //3. Employees Full Information
            //var sb = new StringBuilder();

            //using (var context = new SoftUniContext())
            //{
            //    foreach (var employee in context.Employees.OrderBy(e => e.EmployeeId))
            //    {
            //        sb.AppendLine($"{employee.FirstName} {employee.LastName} {employee.MiddleName} {employee.JobTitle} {employee.Salary:F2}");
            //    }
            //}

            //Console.WriteLine(sb.ToString().TrimEnd());
            //4. Employees with Salary Over 50 000
            //int salary = 50000;
            //using (var context = new SoftUniContext())
            //{
            //    Console.WriteLine(string.Join(Environment.NewLine, context.Employees
            //        .Where(e => e.Salary > salary)
            //        .Select(e => e.FirstName)
            //        .OrderBy(e => e)));
            //}
            //5. Employees from Research and Development
            //string department = "Research and Development";
            //using (var context = new SoftUniContext())
            //{
            //    Console.WriteLine(string.Join(Environment.NewLine, context.Employees
            //        .Where(e => e.Department.Name.Equals(department, StringComparison.OrdinalIgnoreCase))
            //        .OrderBy(e => e.Salary)
            //        .ThenByDescending(e => e.FirstName)
            //        .Select(e => $"{e.FirstName} {e.LastName} from {department} - ${e.Salary:F2}")));
            //}
            //6. Adding a New Address and Updating Employee
            //int townId = 4;
            //string addressText = "Vitoshka 15";
            //string assignee = "Nakov";
            //using (var context = new SoftUniContext())
            //{
            //    var address = context.Addresses.FirstOrDefault(a => a.AddressText == addressText);
            //    if (address == null)
            //    {
            //        address = new Address()
            //        {
            //            AddressText = addressText,
            //            TownId = townId
            //        };
            //    }

            //    foreach (var employee in context.Employees.Where(e => e.LastName == assignee))
            //    {
            //        employee.Address = address;
            //    }

            //    context.SaveChanges();
            //    Console.WriteLine(string.Join(Environment.NewLine, context.Employees
            //        .OrderByDescending(e => e.AddressId)
            //        .Take(10)
            //        .Select(e => e.Address.AddressText)));
            //}
            //7. Employees and Projects
            //using (var context = new SoftUniContext())
            //{
            //    var employees = context.Employees
            //        .Where(e => e.EmployeesProjects
            //            .Any(ep => ep.Project.StartDate.Year >= 2001 && ep.Project.StartDate.Year <= 2013))
            //        .Take(30)
            //        .Select(e => new
            //        {
            //            Employee = $"{e.FirstName} {e.LastName}",
            //            Manager = $"{e.Manager.FirstName} {e.Manager.LastName}",
            //            Projects = e.EmployeesProjects
            //                .Select(ep => new
            //                {
            //                    ep.Project.Name,
            //                    ep.Project.StartDate,
            //                    ep.Project.EndDate
            //                })
            //        });

            //    foreach (var emp in employees)
            //    {
            //        Console.WriteLine($"{emp.Employee} - Manager: {emp.Manager}");

            //        foreach (var project in emp.Projects)
            //        {
            //            var endDate = project.EndDate?.ToString("M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture) ?? "not finished";
            //            Console.WriteLine($"--{project.Name} - {project.StartDate.ToString("M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture)} - {endDate}");
            //        }
            //    }
            //}

            //8.Addresses by Town
            //using (var db = new SoftUniContext())
            //{
            //    var addreses = db.Addresses.GroupBy(a => new
            //    {
            //        a.AddressId,
            //        a.AddressText,
            //        a.Town.Name
            //    },
            //    (key, group) => new
            //    {
            //        AddressText = key.AddressText,
            //        Town = key.Name,
            //        Count = group.Sum(a => a.Employees.Count)
            //    }).OrderByDescending(o => o.Count)
            //        .ThenBy(o => o.Town)
            //        .ThenBy(o => o.AddressText)
            //        .Take(10)
            //        .ToList();

            //    foreach (var addres in addreses)
            //    {
            //        Console.WriteLine($"{addres.AddressText}, {addres.Town} - {addres.Count} employees");
            //    }
            //}

            //9.Employee 147
            //using (var db = new SoftUniContext())
            //{
            //    var employee = db.Employees.Where(e => e.EmployeeId == 147).FirstOrDefault();
            //    var projects = db.EmployeesProjects.Where(e => e.EmployeeId == 147).Select(p => new { ProjectName = p.Project.Name }).OrderBy(p => p.ProjectName);
            //    Console.WriteLine($"{employee.FirstName} {employee.LastName} - {employee.JobTitle}");
            //    foreach (var project in projects)
            //    {
            //        Console.WriteLine(project.ProjectName);
            //    }
            //}

            //10.Departments with More Than 5 Employees
            //using (var db = new SoftUniContext())
            //{
            //    var departments = db.Departments.Where(d => d.Employees.Count > 5).OrderBy(d => d.Employees.Count).ThenBy(d=>d.Name).Select(d => new
            //    {
            //        d.Name,
            //        FirstName = d.Manager.FirstName,
            //        d.Manager.LastName,
            //        d.Employees
            //    });
            //    foreach (var department in departments)
            //    {
            //        Console.WriteLine($"{department.Name} - {department.FirstName} {department.LastName}");
            //        foreach (var employee in department.Employees.OrderBy(e => e.FirstName).ThenBy(e => e.LastName))
            //        {
            //            Console.WriteLine($"{employee.FirstName} {employee.LastName} - {employee.JobTitle}");
            //        }
            //        Console.WriteLine("----------");
            //    }
            //}

            //11.Find Latest 10 Projects
            //using (var db = new SoftUniContext())
            //{
            //    var projects = db.Projects.OrderByDescending(x => x.StartDate).Take(10).OrderBy(p =>p.Name);
            //    foreach (var project in projects)
            //    {
            //        Console.WriteLine(project.Name);
            //        Console.WriteLine(project.Description);
            //        Console.WriteLine(project.StartDate.ToString("M/d/yyyy h:mm:ss tt",CultureInfo.InvariantCulture));
            //    }
            //}

            //12.Increase Salaries
            //using (var db = new SoftUniContext())
            //{
            //    var employeesChange = db.Employees
            //        .Where(w => w.Department.Name == "Engineering"
            //                    || w.Department.Name == "Tool Design"
            //                    || w.Department.Name == "Marketing"
            //                    || w.Department.Name == "Information Services");
            //    foreach (var emp in employeesChange)
            //    {
            //        emp.Salary = emp.Salary * 1.12m;
            //    }

            //    db.SaveChanges();

            //    var employees = db.Employees
            //        .Where(w => w.Department.Name == "Engineering"
            //                    || w.Department.Name == "Tool Design"
            //                    || w.Department.Name == "Marketing"
            //                    || w.Department.Name == "Information Services")
            //        .Select(x => new
            //        {
            //            x.FirstName,
            //            x.LastName,
            //            x.Salary
            //        }).OrderBy(x => x.FirstName).ThenBy(x => x.LastName)
            //        .ToList();
            //    for (int i = 0; i < employees.Count(); i++)
            //    {
            //        Console.WriteLine($"{employees[i].FirstName} {employees[i].LastName} (${employees[i].Salary:f2})");
            //    }
            //}

            //13.Find Employees by First Name Starting With Sa
            //using (var db = new SoftUniContext())
            //{
            //    var employees = db.Employees.Where(e => e.FirstName.StartsWith("Sa")).OrderBy(x => x.FirstName).ThenBy(x => x.LastName);

            //    foreach (var employee in employees)
            //    {
            //        Console.WriteLine($"{employee.FirstName} {employee.LastName} - {employee.JobTitle} - (${employee.Salary:f2})");
            //    }
            //}

            //14.Delete Project by Id
            //using (var db = new SoftUniContext())
            //{
            //    var toRemuve = db.EmployeesProjects.Where(x => x.ProjectId == 2);
            //    foreach (var projectToRemuve in toRemuve)
            //    {
            //        db.EmployeesProjects.Remove(projectToRemuve);
            //    }
            //    var project = db.Projects.Find(2);
            //    db.Projects.Remove(project);
            //    db.SaveChanges();
            //    var res = db.Projects.Take(10);
            //    foreach (var projectt in res)
            //    {
            //        Console.WriteLine(projectt.Name);
            //    }
            //}

            //15.Remove Towns
            using (var context = new SoftUniContext())
            {
                string[] names = new string[] { "Sofia", "Seattle" };
                foreach (var name in names)
                {
                    var town = context.Towns
                        .FirstOrDefault(t => t.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

                    if (town == null)
                    {
                        Console.WriteLine($"There is not town with name: {name}");
                        continue;
                    }

                    context.Employees
                        .Where(e => e.Address.Town.TownId == town.TownId)
                        .ToList()
                        .ForEach(e => e.Address = null);

                    var addresses = context.Addresses
                        .Where(a => a.TownId == town.TownId)
                        .ToArray();

                    var addressesCount = addresses.Length;

                    context.Addresses.RemoveRange(addresses);
                    context.Towns.Remove(town);
                    context.SaveChanges();

                    string addressPluralisation = addressesCount == 1
                        ? "address"
                        : "addresses";

                    Console.WriteLine($"{addressesCount} {addressPluralisation} in {name} was deleted");
                }
            }
        }
    }
}
