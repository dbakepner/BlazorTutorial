using AutoMapper;
using EmployeeManagement.Models;
using EmployeeManagement.Web.Models;
using EmployeeManagement.Web.Services;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Web.Pages
{
    public class EditEmployeeBase : ComponentBase
    {
        [Inject]
        public IEmployeeService EmployeeService { get; set; }

        [Inject]
        public IMapper Mapper { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        private Employee Employee { get; set; } = new Employee();

        public EditEmployeeModel EditEmployeeModel { get; set; } = new EditEmployeeModel();

        [Inject]
        public IDepartmentService DepartmentService { get; set; }

        public string DepartmentId { get; set; }
        // created because dropdown Id can't be an int32.  https://www.youtube.com/watch?v=K-9_B-YSIxc

        public List<Department> Departments { get; set; } = new List<Department>();

        [Parameter]
        public string Id { get; set; }

        protected async override Task OnInitializedAsync()
        {
            Employee = await EmployeeService.GetEmployeeById(int.Parse(Id));
            Departments = (await DepartmentService.GetDepartments()).ToList();
            DepartmentId = Employee.DepartmentId.ToString(); // he removed this w/o explaining
            // https://www.youtube.com/watch?v=mUqP-2LilAQ 4:04
            // 

            Mapper.Map(Employee, EditEmployeeModel);
            
        }

        protected async Task HandleValidSubmit() {
            Mapper.Map(EditEmployeeModel, Employee);
            var result = await EmployeeService.UpdateEmployee(Employee);

            if (result != null)
            {
                    NavigationManager.NavigateTo("/");
            }

        }
    }
}
