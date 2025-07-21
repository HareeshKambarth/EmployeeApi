using EmpApi.Data;
using EmpApi.Models;
using Microsoft.EntityFrameworkCore;

namespace EmpApi.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly EmpDbContext _context;
        public EmployeeRepository(EmpDbContext context)
        {
            _context = context;
        }
        public async Task AddEmployee(Employee employee)
        {
            _context.Employee.Add(employee);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateEmployee(Employee employee)
        {
            var existingEmployee = await _context.Employee.FindAsync(employee.Id);
            existingEmployee.Name = employee.Name;
            existingEmployee.Address = employee.Address;
            existingEmployee.Email = employee.Email;
            existingEmployee.RegistrationDate = employee.RegistrationDate;
            existingEmployee.ActiveStatus = employee.ActiveStatus;
            existingEmployee.Delete = employee.Delete;
            await _context.SaveChangesAsync();
        }
        public async Task<Employee> GetEmployeeById(int id)
        {
            return await _context.Employee.FindAsync(id);
        }
        public async Task UpdateEmployeeStatus(int id, int status)
        {
            var employee = await _context.Employee.FindAsync(id);
            employee.ActiveStatus = status;
            await _context.SaveChangesAsync();
        }
        public async Task DeleteEmployee(int id)
        {
            var employee = await _context.Employee.FindAsync(id);
            employee.Delete = 1;
            await _context.SaveChangesAsync();

        }
        public async Task<List<Employee>> GetAllEmployees()
        {
            return await _context.Employee.Where(e => e.Delete == 0).ToListAsync();
        }
    }
}
