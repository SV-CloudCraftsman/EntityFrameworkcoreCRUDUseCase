using System;
using System.Linq;
using EntityFrameworkcoreUseCase;
using EntityFrameworkcoreUseCase.Models;

namespace EFCoreConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            int option;

            do
            {
                //Console.Clear(); // Clear the console for better readability
                Console.WriteLine("--------- Choose an option from below ---------");
                Console.WriteLine("1. Display all Employees information");
                Console.WriteLine("2. Insert new employee");
                Console.WriteLine("3. Display specific employee");
                Console.WriteLine("4. Update employee information");
                Console.WriteLine("5. Delete employee");
                Console.WriteLine("6. Exit");

                if (!int.TryParse(Console.ReadLine(), out option))
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                    continue;
                }

                switch (option)
                {
                    case 1:
                        DisplayAllEmployees();
                        break;

                    case 2:
                        InsertNewEmployee();
                        break;

                    case 3:
                        DisplaySpecificEmployee();
                        break;

                    case 4:
                        UpdateEmployeeInformation();
                        break;

                    case 5:
                        DeleteEmployee();
                        break;

                    case 6:
                        Console.WriteLine("Exiting...");
                        break;

                    default:
                        Console.WriteLine("Invalid option. Please choose a valid option.");
                        break;
                }

            } while (option != 6);
        }

        private static void DisplayAllEmployees()
        {
            using var context = new AppDbContext();
            var employees = context.Employees.ToList();

            foreach (var employee in employees)
            {
                Console.WriteLine($"EmployeeID : {employee.EmployeeID}");
                Console.WriteLine($"FirstName : {employee.FirstName}");
                Console.WriteLine($"LastName : {employee.LastName}");
                Console.WriteLine($"Position : {employee.Position}");
                Console.WriteLine($"Department : {employee.Department}");
                Console.WriteLine($"HireDate : {employee.HireDate}");
                Console.WriteLine($"Salary : {employee.Salary:C}");
                Console.WriteLine("--------------------------------------------------");
            }
        }

        private static void InsertNewEmployee()
        {
            Console.WriteLine("------ Enter Employee details to Insert -----");

            Console.Write("Enter FirstName: ");
            var firstName = Console.ReadLine();

            Console.Write("Enter LastName: ");
            var lastName = Console.ReadLine();

            Console.Write("Enter Position: ");
            var position = Console.ReadLine();

            Console.Write("Enter Department: ");
            var department = Console.ReadLine();

            Console.Write("Enter Salary: ");
            if (!decimal.TryParse(Console.ReadLine(), out var salary))
            {
                Console.WriteLine("Invalid salary. Please enter a valid number.");
                return;
            }

            using var context = new AppDbContext();
            var newEmployee = new Employee
            {
                FirstName = firstName,
                LastName = lastName,
                Position = position,
                Department = department,
                Salary = salary
            };
            context.Employees.Add(newEmployee);
            context.SaveChanges();

            Console.WriteLine("New employee data has been inserted.");
        }

        private static void DisplaySpecificEmployee()
        {
            Console.Write("Enter employee ID to display details: ");
            if (!int.TryParse(Console.ReadLine(), out var empID))
            {
                Console.WriteLine("Invalid employee ID. Please enter a valid number.");
                return;
            }

            using var context = new AppDbContext();
            var employee = context.Employees.FirstOrDefault(e => e.EmployeeID == empID);

            if (employee != null)
            {
                Console.WriteLine($"-------- Employee Information of Employee ID {empID} --------");
                Console.WriteLine($"EmployeeID : {employee.EmployeeID}");
                Console.WriteLine($"FirstName : {employee.FirstName}");
                Console.WriteLine($"LastName : {employee.LastName}");
                Console.WriteLine($"Position : {employee.Position}");
                Console.WriteLine($"Department : {employee.Department}");
                Console.WriteLine($"HireDate : {employee.HireDate}");
                Console.WriteLine($"Salary : {employee.Salary:C}");
            }
            else
            {
                Console.WriteLine("Employee not found.");
            }
        }

        private static void UpdateEmployeeInformation()
        {
            Console.Write("Enter employee ID to update details: ");
            if (!int.TryParse(Console.ReadLine(), out var empID))
            {
                Console.WriteLine("Invalid employee ID. Please enter a valid number.");
                return;
            }

            Console.Write("Enter new FirstName: ");
            var newFirstName = Console.ReadLine();

            using var context = new AppDbContext();
            var employee = context.Employees.FirstOrDefault(e => e.EmployeeID == empID);

            if (employee != null)
            {
                employee.FirstName = newFirstName;
                context.Employees.Update(employee);
                context.SaveChanges();
                Console.WriteLine("Employee details have been updated.");
            }
            else
            {
                Console.WriteLine("Employee not found.");
            }
        }

        private static void DeleteEmployee()
        {
            Console.Write("Enter employee ID to delete: ");
            if (!int.TryParse(Console.ReadLine(), out var empID))
            {
                Console.WriteLine("Invalid employee ID. Please enter a valid number.");
                return;
            }

            using var context = new AppDbContext();
            var employee = context.Employees.FirstOrDefault(e => e.EmployeeID == empID);

            if (employee != null)
            {
                context.Employees.Remove(employee);
                context.SaveChanges();
                Console.WriteLine("Employee has been deleted.");
            }
            else
            {
                Console.WriteLine("Employee not found.");
            }
        }
    }
}
