using Employee.Models;
using Employee.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text;

namespace Employee.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly EmployeeService _employeeService;

        public EmployeeController(EmployeeService employeeService)
        {
            _employeeService = employeeService;
        }
        public async Task<IActionResult> Index()
        {
            var employees = await _employeeService.GetEmployeesAsync();
            return View(employees);

        }

        public async Task<IActionResult> EmployeeDetails(int id)
        {
            try
            {
                var employee = await _employeeService.GetEmployeeByIdAsync(id);
                return PartialView("_EmployeeDetails", employee);
            }
            catch (HttpRequestException)
            {
                return View("Error");
            }
        }

        public IActionResult DeleteEmployee(int id)
        {
            try
            {
                _employeeService.DeleteEmployeeAsync(id);
                return Json(new { success = true });
            }
            catch (HttpRequestException)
            {
                return View("Error");
            }
        }
    }
}
