using Employee.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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

        public async Task<string> GetEmployeeCity(int id)
        {
            var response = await _httpClient.GetAsync($"api/City/get-city-by-id/{id}");
            if (response.IsSuccessStatusCode)
            {
                var contentType = response.Content.Headers.ContentType?.MediaType;

                if (contentType != null && contentType.Equals("application/json", StringComparison.OrdinalIgnoreCase))
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var city = JsonConvert.DeserializeObject<CityModel>(content);

                    return city.CityName;
                }
                else
                {
                    throw new Exception($"Unexpected content type: {contentType}");
                }
            }
            else
            {
                throw new Exception($"Failed to get city. Status code: {response.StatusCode}");
            }
        }

        public async Task<string> GetEmployeeCountry(int id)
        {
            var response = await _httpClient.GetAsync($"api/Country/get-country-by-id/{id}");
            if (response.IsSuccessStatusCode)
            {
                var contentType = response.Content.Headers.ContentType?.MediaType;

                if (contentType != null && contentType.Equals("application/json", StringComparison.OrdinalIgnoreCase))
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var country = JsonConvert.DeserializeObject<CountryModel>(content);

                    return country.CountryName;
                }
                else
                {
                    throw new Exception($"Unexpected content type: {contentType}");
                }
            }
            else
            {
                throw new Exception($"Failed to get country. Status code: {response.StatusCode}");
            }
        }

        public async Task<string> GetEmployeeState(int id)
        {
            var response = await _httpClient.GetAsync($"api/States/get-state-by-id/{id}");
            if (response.IsSuccessStatusCode)
            {
                var contentType = response.Content.Headers.ContentType?.MediaType;

                if (contentType != null && contentType.Equals("application/json", StringComparison.OrdinalIgnoreCase))
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var state = JsonConvert.DeserializeObject<StateModel>(content);

                    return state.StateName;
                }
                else
                {
                    throw new Exception($"Unexpected content type: {contentType}");
                }
            }
            else
            {
                throw new Exception($"Failed to get state. Status code: {response.StatusCode}");
            }
        }

    }
}
