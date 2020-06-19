using EmployeeManagement.Models;
using EmployeeManagement.Web.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Web.Pages
{
    public class EmployeeDetailsBase :ComponentBase
    {
        /* this is the property a view will bind to, to display the employee data */
        public Employee Employee { get; set; } = new Employee();

        protected string Coordinates { get; set; }

        protected string ButtonText { get; set; } = "Hide footer";
        protected string CssClass { get; set; } = null;

        [Inject]
        public IEmployeeService EmployeeService { get; set; }

        [Parameter]
        public string Id { get; set; }

        protected async override Task OnInitializedAsync()
        {
            // If Id value is not supplied in the URL, use the value 1
            Id = Id ?? "1";
            Employee = await EmployeeService.GetEmployeeById(int.Parse(Id));
        }

        protected void Button_Click()
        {
            if(ButtonText == "Hide footer")
            {
                ButtonText = "Show footer";
                CssClass = "HideFooter";
            }
            else
            {
                CssClass = null;
                ButtonText = "Hide footer";
            }
        }
        protected void Mouse_Move(MouseEventArgs e)
        {
            Coordinates = $"X = {e.ClientX} Y = {e.ClientY}";
        }
    }
}
