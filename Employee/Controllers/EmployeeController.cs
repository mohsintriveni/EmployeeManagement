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

                var cityName = await _employeeService.GetEmployeeCity(employee.City);
                ViewBag.CityName = cityName;
                var countryName = await _employeeService.GetEmployeeCountry(employee.Country);
                ViewBag.CountryName = countryName;
                var stateName = await _employeeService.GetEmployeeState(employee.State);
                ViewBag.StateName = stateName;

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
