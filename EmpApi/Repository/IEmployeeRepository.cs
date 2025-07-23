using Microsoft.EntityFrameworkCore;
using EmpApi.Models;
namespace EmpApi.Repository
{
    public interface IEmployeeRepository
    {

        Task AddEmployee(Employee employee, Guid activityId);
        Task UpdateEmployee(Employee employee, Guid activityId);
        Task<Employee> GetEmployeeById(int id, Guid activityId);  
        Task UpdateEmployeeStatus(int id, int status, Guid activityId);
        Task DeleteEmployee(int id, Guid activityId);
        Task<List<Employee>> GetAllEmployees(Guid activityId);

    }
   
}
