using System;
using System.ComponentModel.DataAnnotations;

namespace EntityFrameworkcoreUseCase.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeID { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "First name cannot exceed 50 characters.")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Last name cannot exceed 50 characters.")]
        public string LastName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Position cannot exceed 100 characters.")]
        public string Position { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Department cannot exceed 100 characters.")]
        public string Department { get; set; }

        public DateTime? HireDate { get; set; } = DateTime.Now;

        [Range(0, double.MaxValue, ErrorMessage = "Salary must be a positive value.")]
        public decimal Salary { get; set; }

        // Parameterless constructor for EF Core
        public Employee() { }

        // Constructor with parameters for convenience
        public Employee(string firstName, string lastName, string position, string department, decimal salary, DateTime? hireDate = null)
        {
            if (string.IsNullOrWhiteSpace(firstName))
                throw new ArgumentException("First name is required.", nameof(firstName));
            if (string.IsNullOrWhiteSpace(lastName))
                throw new ArgumentException("Last name is required.", nameof(lastName));
            if (string.IsNullOrWhiteSpace(position))
                throw new ArgumentException("Position is required.", nameof(position));
            if (string.IsNullOrWhiteSpace(department))
                throw new ArgumentException("Department is required.", nameof(department));

            FirstName = firstName;
            LastName = lastName;
            Position = position;
            Department = department;
            Salary = salary;
            HireDate = hireDate ?? DateTime.Now; // Default to now if no date provided
        }
    }
}
