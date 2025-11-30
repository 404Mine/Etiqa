using System.Collections.Generic;
using System.Linq;
using EmployeeAPI.Models;

namespace EmployeeAPI.Services
{
    public class EmployeeService
    {
        private readonly List<Employee> _employees = new();
        private int _nextEmployeeNum = 1;

        //Get Employees
        public IEnumerable<Employee> GetAll()
        {
            return _employees;
        }

        //Get Employee with EmpNum
        public Employee? GetByNumber(int employeeNumber)
        {
            return _employees.FirstOrDefault(e => e.EmployeeNumber == employeeNumber);
        }

        //Create Employee
        public Employee Create(Employee employee)
        {
            string namePart = (employee.LastName.Length >= 3 ? employee.LastName.Substring(0, 3) : employee.LastName).ToUpper();

            Random rnd = new Random();
            int randNum = rnd.Next(0, 100000);
            string numberPart = randNum.ToString("D5");

            string dobPart = employee.DateOfBirth.ToString("ddMMMyyyy").ToUpper();

            employee.EmployeeNumber = $"{namePart}-{numberPart}-{dobPart}";

            _employees.Add(employee);
            return employee;
        }

        //Update Employee
        public bool Update(int employeeNumber, Employee updatedEmployee)
        {
            var existing = GetByNumber(employeeNumber);
            if (existing == null) return false;

            existing.FirstName = updatedEmployee.FirstName;
            existing.LastName = updatedEmployee.LastName;
            existing.MiddleName = updatedEmployee.MiddleName;
            existing.DateOfBirth = updatedEmployee.DateOfBirth;
            existing.DailyRate = updatedEmployee.DailyRate;
            existing.WorkingDays = updatedEmployee.WorkingDays;

            return true;
        }

        //Delete Employee
        public bool Delete(int employeeNumber)
        {
            var existing = GetByNumber(_nextEmployeeNum);
            if (existing == null) return false;

            _employees.Remove(existing);
            return true;
        }

        //Computation
        public decimal ComputeDailyRate()
        {
            return _employees.Sum(e => e.DailyRate);
        }
    }
}
