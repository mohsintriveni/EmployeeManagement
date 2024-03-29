using Employee.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Employee.Services
{
    public class EmployeeService
    {
        private readonly HttpClient _httpClient;

        public EmployeeService(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<List<EmployeeModel>> GetEmployeesAsync()
        {
            var response = await _httpClient.GetAsync("api/Employees/get-all-employees");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<EmployeeModel>>();
        }

        public async Task<EmployeeModel> GetEmployeeByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"api/Employees/get-employee-by-id/{id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<EmployeeModel>();
        }

        public async Task DeleteEmployeeAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/Employees/delete-employee/{id}"); 
            response.EnsureSuccessStatusCode();
        }

        public async Task<EmployeeModel> AddEmployeeAsync(EmployeeModel employee)
        {
            var response = await _httpClient.PostAsJsonAsync($"api/Employees/add-employee/",employee);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<EmployeeModel>();
        }

        public async Task<List<CountryModel>> GetCountries()
        {
            var response = await _httpClient.GetAsync($"api/Country/get-countries");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<CountryModel>>();
        }
    }
}
