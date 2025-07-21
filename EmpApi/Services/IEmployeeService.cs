using EmpApi.Models;
namespace EmpApi.Services
{
    public interface IEmployeeService
    {
        Task AddEmployee(Employee employee);
        Task UpdateEmployee(Employee employee);
        Task<Employee> GetEmployeebyId(int id);
        Task UpdateEmployeeStatus(int id, int status);
        Task DeleteEmployee(int id);
        Task <List<Employee>> GetAllEmployees();
    }   
}
