using Microsoft.AspNetCore.Mvc;
using EmployeeAPI.Services;
using EmployeeAPI.Models;
using EmployeeAPI.ViewModels;

namespace EmployeeAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeService _service;

        public EmployeeController(EmployeeService service)
        {
            _service = service;
        }

        //Get: api/employee
        [HttpGet]
        public ActionResult<IEnumerable<EmployeeReadViewModel>> GetAll()
        {
            var employees = _service.GetAll()
                .Select(e => new EmployeeReadViewModel
                {
                    EmployeeNumber = e.EmployeeNumber,
                    FullName = $"{e.LastName}, {e.FirstName} {e.MiddleName}",
                    DateOfBirth = e.DateOfBirth,
                    DailyRate = e.DailyRate,
                    WorkingDays = e.WorkingDays
                });

            return Ok(employees);
        }

        //GET: api/employee/employeenumber
        [HttpGet("{employeeNumber}")]
        public ActionResult<EmployeeReadViewModel> Get(string employeeNumber)
        {
            var employee = _service.GetByNumber(employeeNumber);
            if (employee == null) return NotFound();

            var vm = new EmployeeReadViewModel
            {
                EmployeeNumber = employee.EmployeeNumber,
                FullName = $"{employee.LastName}, {employee.FirstName} {employee.MiddleName}".Trim(),
                DateOfBirth = employee.DateOfBirth,
                DailyRate = employee.DailyRate,
                WorkingDays = employee.WorkingDays
            };

            return Ok(vm);
        }

        //POST: api/employee
        [HttpPost]
        public ActionResult<EmployeeReadViewModel> Create(EmployeeCreateViewModel vm)
        {
            var employee = new Employee
            {
                LastName = vm.LastName,
                FirstName = vm.FirstName,
                MiddleName = vm.MiddleName,
                DateOfBirth = vm.DateOfBirth,
                DailyRate = vm.DailyRate,
                WorkingDays = vm.WorkingDays
            };

            var created = _service.Create(employee);

            var readVm = new EmployeeReadViewModel
            {
                EmployeeNumber = created.EmployeeNumber,
                FullName = $"{created.LastName}, {created.FirstName} {created.MiddleName}",
                DateOfBirth = created.DateOfBirth,
                DailyRate = created.DailyRate,
                WorkingDays = created.WorkingDays
            };

            return CreatedAtAction(nameof(Get), new { employeeNumber = created.EmployeeNumber }, readVm);
        }

        //PUT: api/employee/employeenumber
        [HttpPut("{employeeNumber}")]
        public IActionResult Update(string employeeNumber, EmployeeUpdateViewModel vm)
        {
            var updatedEmployee = new Employee
            {
                LastName = vm.LastName,
                FirstName = vm.FirstName,
                MiddleName = vm.MiddleName,
                DateOfBirth = vm.DateOfBirth,
                DailyRate = vm.DailyRate,
                WorkingDays = vm.WorkingDays
            };

            var result = _service.Update(employeeNumber, updatedEmployee);
            if (!result) return NotFound();

            return NoContent();
        }

        //DELETE: api/employee/employeenumber
        [HttpDelete("{employeeNumber}")]
        public IActionResult Delete(string employeeNumber)
        {
            var result = _service.Delete(employeeNumber);
            if (!result) return NotFound();

            return NoContent();
        }

        //GET: api/employee/compute/total-daily-rate
        [HttpGet("compute/total-daily-rate")]
        public IActionResult ComputeDailyRate()
        {
            var total = _service.ComputeDailyRate();
            return Ok(new { totalDailyRate = total });
        }

        [HttpGet("{employeeNumber}/takehome")]
        public IActionResult GetTakeHomePay (string employeeNumber, DateTime start, DateTime end)
        {
            var pay = _service.ComputeTakeHomePay(employeeNumber, start, end);
            return Ok(new { EmployeeNumber = employeeNumber, TakeHomePay = pay });
        }
    }
}
