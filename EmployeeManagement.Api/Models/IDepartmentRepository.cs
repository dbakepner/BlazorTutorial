using EmployeeManagement.Models;
using System.Collections.Generic;

public interface IDepartmentRepository
{
    IEnumerable<Department> GetDepartments();
    Department GetDepartment(int departmentId);
}