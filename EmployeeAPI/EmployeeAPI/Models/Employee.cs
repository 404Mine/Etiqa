using System.ComponentModel.DataAnnotations;

namespace EmployeeAPI.Models
{
    public class Employee
    {
        [Key]
        public string EmployeeNumber { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public decimal DailyRate { get; set; }
        public string WorkingDays { get; set; }
        
    }
}
