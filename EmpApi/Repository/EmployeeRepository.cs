using EmpApi.Data;
using EmpApi.Logging;
using EmpApi.Models;
using Microsoft.EntityFrameworkCore;
using EmpApi.Logging;


namespace EmpApi.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly EmpDbContext _context;
        private readonly ILoggingService _Logger;
        public EmployeeRepository(EmpDbContext context, ILoggingService Logger)
        {
            _context = context;
            _Logger = Logger;
        }
        string GetDeepestMessage(Exception ex)
        {
            while (ex.InnerException != null)
                ex = ex.InnerException;
            return ex.Message.Trim();
        }
        public async Task AddEmployee(Employee employee, Guid activityId)
        {
            _Logger.LogInformation("Adding Employee Repository started.", activityId);
            try
            {
                await _context.Database.ExecuteSqlInterpolatedAsync($@"
        EXEC AddEmployee @Id = {employee.Id},
        @Name = {employee.Name},
        @Address = {employee.Address},
        @Email = {employee.Email},
        @RegistrationDate = {employee.RegistrationDate},
        @ActiveStatus = {employee.ActiveStatus},
        @Delete = {employee.Delete}");
                _Logger.LogInformation("Adding Employee Repository Completed.", activityId);
            }
            catch (Exception ex)
            {
                GetDeepestMessage(ex);
                _Logger.LogError("Exception" + ex.Message, ex, activityId);
                throw (new Exception("Exception" + ex.Message));

            }

        }

        public async Task UpdateEmployee(Employee employee, Guid activityId)
        {
            _Logger.LogInformation("Updating Employee Repository started.", activityId);
            try
            {

                await _context.Database.ExecuteSqlInterpolatedAsync($@"
        EXEC UpdateEmployee
        @Id = {employee.Id},
        @Name = {employee.Name},
        @Address = {employee.Address},
        @Email = {employee.Email},
        @RegistrationDate = {employee.RegistrationDate},
        @ActiveStatus = {employee.ActiveStatus},
        @Delete = {employee.Delete}");
                _Logger.LogInformation("Updating Employee Repository Completed.", activityId);
            }
            catch (Exception ex)
            {
                GetDeepestMessage(ex);
                _Logger.LogError("Exception" + ex.Message, ex, activityId);
                throw (new Exception("Exception" + ex.Message));

            }
        }
        public async Task<Employee> GetEmployeeById(int id, Guid activityId)
        {
            _Logger.LogInformation("Get  Employee Repository started.", activityId);
            try
            {

                var result = await _context.Employee
            .FromSqlInterpolated($"EXEC GetEmployeeById @Id = {id}")
            .AsNoTracking()
            .ToListAsync();
                _Logger.LogInformation("Get  Employee Repository Completed.", activityId);
                return result.FirstOrDefault();
            }
            catch (Exception ex)
            {
                GetDeepestMessage(ex);
                _Logger.LogError("Exception" + ex.Message, ex, activityId);
                throw (new Exception("Exception" + ex.Message));

            }
        }
        public async Task UpdateEmployeeStatus(int id, int status, Guid activityId)
        {
            _Logger.LogInformation("Update Employee Repository started.", activityId);
            try
            {
                await _context.Database.ExecuteSqlInterpolatedAsync($@"EXEC UpdateEmployeeStatus @Id={id}, @ActiveStatus = {status}");
                _Logger.LogInformation("Update Employee Repository Completed.", activityId);
            }
            catch (Exception ex)
            {
                GetDeepestMessage(ex);
                _Logger.LogError("Exception" + ex.Message, ex, activityId);
                throw (new Exception("Exception" + ex.Message));

            }

        }
        public async Task DeleteEmployee(int id, Guid activityId)
        {
            _Logger.LogInformation("delete Employee Repository started.", activityId);
            try
            {
                await _context.Database.ExecuteSqlInterpolatedAsync($@"EXEC DeleteEmployee @id={id}");
                _Logger.LogInformation("delete Employee Repository Completed.", activityId);
            }
            catch (Exception ex)
            {
                GetDeepestMessage(ex);
                _Logger.LogError("Exception" + ex.Message, ex, activityId);
                throw (new Exception("Exception" + ex.Message));

            }

        }
        public async Task<List<Employee>> GetAllEmployees(Guid activityId)
        {
            _Logger.LogInformation("Get all Employee Repository started.", activityId);
            try
            {
                var result = await _context.Employee
                    .FromSqlInterpolated($"EXEC GetAllEmployees")
                    .ToListAsync();
                _Logger.LogInformation("Get all Employee Repository Completed.", activityId);
                return result.ToList();
            }
            catch (Exception ex)
            {
                GetDeepestMessage(ex);
                _Logger.LogError("Exception" + ex.Message, ex, activityId);
                throw (new Exception("Exception" + ex.Message));

            }
        }
    }
}
