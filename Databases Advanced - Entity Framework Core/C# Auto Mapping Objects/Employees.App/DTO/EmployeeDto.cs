using System.Text;

namespace Employees.App.DTO
{
    class EmployeeDTO
    {
        public EmployeeDTO(string firstName, string lastName, decimal salary)
        {
            FirstName = firstName;
            LastName = lastName;
            Salary = salary;
        }

        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public decimal Salary { get; set; }

        public ManagerDTO Manager { get; set; }

        public override string ToString()
        {
            if (Manager == null)
            {
                return $"{FirstName} {LastName} - ${Salary:f2} - Manager: [no manager]";
            }
            return $"{FirstName} {LastName} - ${Salary:f2} - Manager: {Manager.FirstName}";
        }
    }
}
