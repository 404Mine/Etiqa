using System.Collections.Generic;
using System.Linq;
using EmployeeAPI.Models;
using EmployeeAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace EmployeeAPI.Services
{
    public class EmployeeService
    {
        private readonly EmployeeDbContext _context;

        public EmployeeService(EmployeeDbContext context)
        {
            _context = context;
        }

        //Get Employees
        public IEnumerable<Employee> GetAll() => _context.Employees.ToList();

        //Get Employee with EmpNum
        public Employee? GetByNumber(string employeeNumber) =>
            _context.Employees.FirstOrDefault(e => e.EmployeeNumber == employeeNumber);

        //Create Employee
        public Employee Create(Employee employee)
        {
            // Generate EmployeeNumber
            string namePart = (employee.LastName.Length >= 3 ? employee.LastName.Substring(0, 3) : employee.LastName).ToUpper();
            Random rnd = new Random();
            int randNum = rnd.Next(0, 100000);
            string numberPart = randNum.ToString("D5");
            string dobPart = employee.DateOfBirth.ToString("ddMMMyyyy").ToUpper();

            employee.EmployeeNumber = $"{namePart}-{numberPart}-{dobPart}";

            _context.Employees.Add(employee);
            _context.SaveChanges();

            return employee;
        }

        //Update Employee
        public bool Update(string employeeNumber, Employee updatedEmployee)
        {
            var existing = GetByNumber(employeeNumber);
            if (existing == null) return false;

            existing.FirstName = updatedEmployee.FirstName;
            existing.LastName = updatedEmployee.LastName;
            existing.MiddleName = updatedEmployee.MiddleName;
            existing.DateOfBirth = updatedEmployee.DateOfBirth;
            existing.DailyRate = updatedEmployee.DailyRate;
            existing.WorkingDays = updatedEmployee.WorkingDays;

            _context.SaveChanges();
            return true;
        }

        //Delete Employee
        public bool Delete(string employeeNumber)
        {
            var existing = GetByNumber(employeeNumber);
            if (existing == null) return false;

            _context.Employees.Remove(existing);
            _context.SaveChanges();
            return true;
        }

        //Computation
        public decimal ComputeDailyRate() => _context.Employees.Sum(e => e.DailyRate);

        public decimal ComputeTakeHomePay(string employeeNumber, DateTime startDate, DateTime endDate)
        {
            var employee = _context.Employees.Find(employeeNumber);
            if (employee == null) return 0m;

            // Include all days in the period
            int totalDays = (endDate.Date - startDate.Date).Days + 1;

            return totalDays * employee.DailyRate;
        }
    }
}
