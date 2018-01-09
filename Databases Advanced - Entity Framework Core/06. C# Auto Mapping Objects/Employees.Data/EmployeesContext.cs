namespace Employees.Data
{
    using System;
    using Microsoft.EntityFrameworkCore;
    using Employees.Models.Models;
    public class EmployeesContext : DbContext
    {
        public EmployeesContext() { }

        public EmployeesContext(DbContextOptions options) : base(options) { }
        public DbSet<Employee> Employees { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(DbConfig.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                .HasKey(p => p.Id);

            modelBuilder.Entity<Employee>()
                .Property(x => x.FirstName) 
                .HasMaxLength(60)
                .IsRequired();

            modelBuilder.Entity<Employee>()
                .Property(x => x.LastName)
                .HasMaxLength(60)
                .IsRequired();

            modelBuilder.Entity<Employee>()
                .Property(x => x.Address)
                .HasMaxLength(250)
                .IsRequired(false);

            modelBuilder.Entity<Employee>()
                .Property(p => p.Salary)
                .IsRequired();

            modelBuilder.Entity<Employee>()
                .Property(p => p.Birthday)
                .IsRequired(false);
        }
    }
}