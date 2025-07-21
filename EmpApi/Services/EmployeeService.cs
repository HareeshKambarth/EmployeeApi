using EmpApi.Models;
using EmpApi.Repository;

namespace EmpApi.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        public async Task AddEmployee(Employee employee)
        {
            await _employeeRepository.AddEmployee(employee);
        }
        public async Task UpdateEmployee(Employee employee)
        {
            await _employeeRepository.UpdateEmployee(employee);
        }
        public async Task<Employee> GetEmployeebyId(int id)
        {

            return await _employeeRepository.GetEmployeeById(id);
        }
        public async Task UpdateEmployeeStatus(int id, int status)
        {
            await _employeeRepository.UpdateEmployeeStatus(id, status);
        }
        public async Task DeleteEmployee(int id)
        {
            await _employeeRepository.DeleteEmployee(id);
        }
        public async Task<List<Employee>> GetAllEmployees()
        {
            return await _employeeRepository.GetAllEmployees();
        }
    }
}
