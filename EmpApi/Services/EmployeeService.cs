using EmpApi.Logging;
using EmpApi.Models;
using EmpApi.Repository;
using EmpApi.Logging;


namespace EmpApi.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ILoggingService _Logger;
        public EmployeeService(IEmployeeRepository employeeRepository, ILoggingService Logger)
        {
            _employeeRepository = employeeRepository;
            _Logger = Logger;
        }
        string GetDeepestMessage(Exception ex)
        {
            while (ex.InnerException != null)
                ex = ex.InnerException;

            return ex.Message.Trim();
        }
        public async Task AddEmployee(Employee employee,Guid activityId)
        {
            _Logger.LogInformation("Adding Employee services started.", activityId);
            try
            {
                await _employeeRepository.AddEmployee(employee, activityId);
                _Logger.LogInformation($"Employee services completed for employeeId:{employee.Id}.", activityId);
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
            _Logger.LogInformation("Updating Employee services started.", activityId);
            try
            {
                await _employeeRepository.UpdateEmployee(employee, activityId);
                _Logger.LogInformation("Updating Employee services Completed.", activityId);
            }
            catch (Exception ex)
            {

                _Logger.LogError("Exception" + ex.Message, ex, activityId);
                throw (new Exception("Exception" + ex.Message));
            }
        }
        public async Task<Employee> GetEmployeebyId(int id, Guid activityId)
        {

            _Logger.LogInformation("Get employee by id  services started.", activityId);
            try
            {
                return await _employeeRepository.GetEmployeeById(id, activityId);
            }
            catch (Exception ex)
            {

                _Logger.LogError("Exception" + ex.Message, ex, activityId);
                throw (new Exception("Exception" + ex.Message));
            }
            
            
        }
        public async Task UpdateEmployeeStatus(int id, int status, Guid activityId)
        {
            _Logger.LogInformation("Update Employee status services started.", activityId);
            try
            {
                await _employeeRepository.UpdateEmployeeStatus(id, status, activityId);
                _Logger.LogInformation("Update Employee status services completed.", activityId);
            }
            catch (Exception ex)
            {

                _Logger.LogError("Exception" + ex.Message, ex, activityId);
                throw (new Exception("Exception" + ex.Message));
            }
        }
        public async Task DeleteEmployee(int id, Guid activityId)
        {
            _Logger.LogInformation("Delete Employee services started.", activityId);
            try
            {
                await _employeeRepository.DeleteEmployee(id, activityId);
                _Logger.LogInformation("Delete Employee services completed.", activityId);
            }
            catch (Exception ex)
            {

                _Logger.LogError("Exception" + ex.Message, ex, activityId);
                throw (new Exception("Exception" + ex.Message));
            }
        }
        public async Task<List<Employee>> GetAllEmployees(Guid activityId)
        {
            _Logger.LogInformation("Get all Employee services started.", activityId);
            try
            {
                return await _employeeRepository.GetAllEmployees(activityId);
            }
            catch (Exception ex)
            {

                _Logger.LogError("Exception" + ex.Message, ex, activityId);
                throw (new Exception("Exception" + ex.Message));
            }

        }
    }
}
