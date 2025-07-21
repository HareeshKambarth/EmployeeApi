using Microsoft.EntityFrameworkCore;
using EmpApi.Models;
namespace EmpApi.Repository
{
    public interface IEmployeeRepository
    {

        Task AddEmployee(Employee employee);
        Task UpdateEmployee(Employee employee);
        Task<Employee> GetEmployeeById(int id);  
        Task UpdateEmployeeStatus(int id, int status);
        Task DeleteEmployee(int id);
        Task<List<Employee>> GetAllEmployees();

    }
   
}
