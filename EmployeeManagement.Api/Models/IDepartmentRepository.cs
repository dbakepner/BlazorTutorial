using EmployeeManagement.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IDepartmentRepository
{
    // changed to tasks because we want to make database changes.
    // https://www.youtube.com/watch?v=T1rTWvZL8ts 1:57
    Task<IEnumerable<Department>> GetDepartments();
    Task<Department> GetDepartment(int departmentId);
}