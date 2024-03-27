using System;
using System.Collections.Generic;

public enum Department
{
    IT,
    HR,
    Finance
}

public class Employee
{
    public int EmployeeId { get; }
    public string FirstName { get; }
    public string LastName { get; }
    public double Salary { get; set; }
    public Department Department { get; set; }

    public Employee(int employeeId, string firstName, string lastName, double salary, Department department)
    {
        EmployeeId = employeeId;
        FirstName = firstName;
        LastName = lastName;
        Salary = salary;
        Department = department;
    }
}

public class EmployeeManager
{
    private List<Employee> employees = new List<Employee>();

    public void AddEmployee(Employee employee)
    {
        if (employees.Exists(emp => emp.EmployeeId == employee.EmployeeId))
        {
            Console.WriteLine("Employee ID already exists. Please use a unique ID.");
            return;
        }
        employees.Add(employee);
    }

    public void RemoveEmployee(int employeeId)
    {
        var employeeToRemove = employees.Find(emp => emp.EmployeeId == employeeId);
        if (employeeToRemove != null)
            employees.Remove(employeeToRemove);
        else
            Console.WriteLine("Employee not found.");
    }

    public void DisplayEmployees()
    {
        foreach (var emp in employees)
        {
            Console.WriteLine($"ID: {emp.EmployeeId}, Name: {emp.FirstName} {emp.LastName}, Salary: {emp.Salary}");
        }
    }

    public void DisplayTotalSalary()
    {
        double totalSalary = 0;
        foreach (var emp in employees)
        {
            totalSalary += emp.Salary;
        }
        Console.WriteLine($"Total Salary: {totalSalary}");
    }

    public bool CheckEmployeeIdExists(int employeeId)
    {
        return employees.Exists(emp => emp.EmployeeId == employeeId);
    }
}

class Program
{
    static void Main(string[] args)
    {
        EmployeeManager manager = new EmployeeManager();

        Employee emp1 = new Employee(1, "John", "Doe", 50000, Department.IT);
        manager.AddEmployee(emp1);

        Employee emp2 = new Employee(2, "Jane", "Smith", 60000, Department.HR);
        manager.AddEmployee(emp2);

        bool exit = false;
        while (!exit)
        {
            Console.WriteLine("\nEmployee Management System Menu:");
            Console.WriteLine("1. Add Employee");
            Console.WriteLine("2. Remove Employee");
            Console.WriteLine("3. Display Employees");
            Console.WriteLine("4. Display Total Salary");
            Console.WriteLine("5. Exit");
            Console.Write("Enter your choice: ");
            int choice;
            if (int.TryParse(Console.ReadLine(), out choice))
            {
                switch (choice)
                {
                    case 1:
                        AddEmployee(manager);
                        break;
                    case 2:
                        RemoveEmployee(manager);
                        break;
                    case 3:
                        manager.DisplayEmployees();
                        break;
                    case 4:
                        manager.DisplayTotalSalary();
                        break;
                    case 5:
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please enter a valid option.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid integer.");
            }
        }
    }

    static void AddEmployee(EmployeeManager manager)
    {
        Console.Write("Enter Employee ID: ");
        int employeeId = int.Parse(Console.ReadLine());
        if (manager.CheckEmployeeIdExists(employeeId))
        {
            Console.WriteLine("Employee ID already exists. Please use a unique ID.");
            return;
        }
        Console.Write("Enter First Name: ");
        string firstName = Console.ReadLine();
        Console.Write("Enter Last Name: ");
        string lastName = Console.ReadLine();
        Console.Write("Enter Salary: ");
        double salary = double.Parse(Console.ReadLine());
        Console.WriteLine("Choose Department: ");
        foreach (Department dept in Enum.GetValues(typeof(Department)))
        {
            Console.WriteLine($"{(int)dept}. {dept}");
        }
        Console.Write("Enter Department Number: ");
        Department department = (Department)int.Parse(Console.ReadLine());
        Employee newEmployee = new Employee(employeeId, firstName, lastName, salary, department);
        manager.AddEmployee(newEmployee);
        Console.WriteLine("Employee added successfully.");
    }

    static void RemoveEmployee(EmployeeManager manager)
    {
        Console.Write("Enter Employee ID to remove: ");
        if (int.TryParse(Console.ReadLine(), out int empIdToRemove))
            manager.RemoveEmployee(empIdToRemove);
        else
            Console.WriteLine("Invalid input. Please enter a valid integer.");
    }
}
