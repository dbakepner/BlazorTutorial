using EmployeeManagement.Api.Models;
using EmployeeManagement.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace EmployeeManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController] // automatically checks whether modelstate is valid.
    // Derive from ControllerBase if using web api or a combination of
    // web api and web application.  Derive from Controller class for
    // web application.
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeRepository employeeRepository;

        public EmployeesController(IEmployeeRepository employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }

        [HttpGet]
        public async Task<ActionResult> GetEmployees()
        {
            try
            {
                // returns 200
                return Ok(await employeeRepository.GetEmployees());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        [HttpGet("{id:int}")] // require int parameter
        public async Task<ActionResult<Employee>> GetEmployee(int id)
        {
            try
            {
                var result = await employeeRepository.GetEmployee(id);
                if (result == null)
                {
                    return NotFound();
                }
                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                    "Error retrieving data from the database");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Employee>> CreateEmployee(Employee employee)
        {
            try
            {
                if (employee == null)
                {
                    return BadRequest();
                }

                var emp = await employeeRepository.GetEmployeeByEmail(employee.Email);

                if (emp != null)
                {
                    ModelState.AddModelError("email", "Employee email is already in use.");
                    return BadRequest();
                }

                var createdEmployee = await employeeRepository.AddEmployee(employee);
                // return 201(successfully created), newly created resource
                // add location header (uri of newly created employee object)
                return CreatedAtAction(nameof(GetEmployee), new { id = createdEmployee.EmployeeId }, createdEmployee);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                  "Error adding data to the database");
            }
        }

        [HttpPut("{id:int}")]
        //  [FromBody] maps to repository if there is no [ApiController] at the top.
        //public async Task<ActionResult<Employee>> UpdateEmployee(int id, [FromBody] Employee employee)
        public async Task<ActionResult<Employee>> UpdateEmployee(int id, Employee employee)
        {
            try
            {
                if (id != employee.EmployeeId)
                {
                    return BadRequest("Employee Id mismatch");
                }

                var employeeToUpdate = await employeeRepository.GetEmployee(id);
                if (employeeToUpdate == null)
                {
                    return NotFound($"Employee with Id {id} was not found.");
                }

                return await employeeRepository.UpdateEmployee(employee);

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                  "Error updating data in the database");
            }


        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Employee>> DeleteEmployee(int id, Employee employee)
        {
            try
            {
                if (id != employee.EmployeeId)
                {
                    return BadRequest("Employee Id mismatch");
                }

                var employeeToDelete = await employeeRepository.GetEmployee(id);
                if (employeeToDelete == null)
                {
                    return NotFound($"Employee with Id {id} was not found.");
                }

                return await employeeRepository.DeleteEmployee(id);

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                  "Error deleting data in the database");
            }

        }
    }
}