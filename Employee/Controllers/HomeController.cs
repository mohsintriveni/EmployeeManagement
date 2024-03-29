using Employee.Models;
using Employee.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;

namespace Employee.Controllers
{
    public class HomeController : Controller
    {
        private readonly EmployeeService _employeeService;

        public HomeController(EmployeeService employeeService)
        {
            _employeeService = employeeService;
        }
        //private readonly ILogger<HomeController> _logger;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}

        public async Task<IActionResult> Index()
        {
            var countries = await _employeeService.GetCountries();
            ViewBag.Countries = new SelectList(countries, "Id", "Name");
            var employees = await _employeeService.GetEmployeesAsync();
            return View(employees);
        }

        public async Task<IActionResult> Add()
        {
            var countries = await _employeeService.GetCountries();
            ViewBag.Countries = new SelectList(countries, "Id", "Name");
            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
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

        public ActionResult EmployeeDetails(int id)
        {
            try
            {
                var employee = _employeeService.GetEmployeeByIdAsync(id);
                return Ok(employee);
            }
            catch (HttpRequestException)
            {
                return View("Error");
            }
        }
    }
}