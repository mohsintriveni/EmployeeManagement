using Employee.Models;
using Employee.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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

        public async Task<IActionResult> AddEmployeeAsync(EmployeeModel employee)
        {
            try
            {
                await _employeeService.AddEmployeeAsync(employee);
                return RedirectToAction("Index");

            }
            catch (HttpRequestException)
            {
                return View("Error");
            }
        }
    }
}
