using EmployeeManagement.Models;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace EmployeeManagement.Web.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly HttpClient httpClient;

        public EmployeeService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            /*
             * GetJsonAsync is part of preview NuGet pkg
             * Microsoft.AspNetCore.Blazor.HttpClient
             * and namespace Microsoft.AspNetCore.Components
             * Endpoint "api/employees" will be appended to the base Uri,
             * which is in Startup: 
             * services.AddHttpClient<IEmployeeService, EmployeeService>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:44337/");
            });
             */
            return await httpClient.GetJsonAsync<Employee[]>("api/employees");
        }
    }
}