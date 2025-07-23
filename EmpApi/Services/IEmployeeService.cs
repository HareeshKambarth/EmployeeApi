using EmpApi.Models;
namespace EmpApi.Services
{
    public interface IEmployeeService
    {
        Task AddEmployee(Employee employee, Guid activityId );
        Task UpdateEmployee(Employee employee, Guid activityId);
        Task<Employee> GetEmployeebyId(int id, Guid activityId);
        Task UpdateEmployeeStatus(int id, int status, Guid activityId);
        Task DeleteEmployee(int id, Guid activityId);
        Task <List<Employee>> GetAllEmployees(Guid activityId);
    }   
}
