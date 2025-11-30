using System;

namespace EmployeeAPI.ViewModels
{
    public class EmployeeReadViewModel
    {
        public string EmployeeNumber { get; set; } = "";
        public string FullName { get; set; } = "";
        public DateTime DateOfBirth { get; set; }
        public decimal DailyRate { get; set; }
        public string WorkingDays { get; set; } = "";

    }
}
