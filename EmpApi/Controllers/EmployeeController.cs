using EmpApi.Data;
using EmpApi.Logging;
using EmpApi.Models;
using EmpApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmpApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        
        //Logging
        private readonly ILoggingService _Logger;
        
        private readonly IEmployeeService _employeeService;
        public EmployeeController(IEmployeeService employeeService, ILoggingService logger)
        {
            _employeeService = employeeService;
            _Logger = logger;
        }
        string GetDeepestMessage(Exception ex)
        {
            while (ex.InnerException != null)
                ex = ex.InnerException;

            return ex.Message.Trim();
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Employee>> AddEmployee([FromBody]Employee employee)
        {
            var activityId = Guid.NewGuid();
            _Logger.LogInformation("Adding Employee started.", activityId);
            try
            {

                // User input sanitization
                employee.Name = InputSanitizer.Sanitize(employee.Name);
                employee.Address = InputSanitizer.Sanitize(employee.Address);
                employee.Email = InputSanitizer.Sanitize(employee.Email);
                if (ModelState.IsValid == false)
                {
                    _Logger.LogInformation("Model state is invalid.", activityId);
                    return BadRequest(ModelState);
                }
                if (employee.Name == null || employee.Address == null || employee.Email == null)
                {
                    _Logger.LogInformation("Fields cannot be null", activityId);
                    return BadRequest();
                }
                await _employeeService.AddEmployee(employee, activityId);
                _Logger.LogInformation($"Adding Employee completed. Employee Id: {employee.Id}", activityId);
                return Ok();
            }
            catch (Exception ex)
            {
                GetDeepestMessage(ex);
                _Logger.LogError("Exception" + ex.Message, ex,activityId);
                throw (new Exception("Exception" + ex.Message));
            }
        }
        [Authorize]
        [HttpPut]
        public async Task<ActionResult<Employee>> UpdateEmployee(Employee employee)
        {
            var activityId = Guid.NewGuid();
            _Logger.LogInformation("Update Employee started.", activityId);
            try
            {
                // User input sanitization
                employee.Name = InputSanitizer.Sanitize(employee.Name);
                employee.Address = InputSanitizer.Sanitize(employee.Address);
                employee.Email = InputSanitizer.Sanitize(employee.Email);
                if (ModelState.IsValid == false)
                {
                    _Logger.LogInformation("Model state is invalid.", activityId);
                    return BadRequest(ModelState);
                }
                await _employeeService.UpdateEmployee(employee, activityId);
                
                _Logger.LogInformation($"Update Employee completed. Employee Id: {employee.Id}", activityId);
                return Ok();
            }
            catch (Exception ex)
            {

                _Logger.LogError("Exception" + ex.Message,ex, activityId);
                throw (new Exception("Exception" + ex.Message));
            }



        }
        [Authorize]
        [HttpPut("{id}/status")]
        public async Task<ActionResult<Employee>> UpdateEmployeeStatus(int id, int status)
        {
            var activityId = Guid.NewGuid();
            _Logger.LogInformation("Update Employee Status started.", activityId);
            try
            {
               
                await _employeeService.UpdateEmployeeStatus(id, status, activityId);
                _Logger.LogInformation($"Update Employee Status completed. Employee Id: {id}", activityId);
                return Ok();
            }
            catch (Exception ex)
            {

                _Logger.LogError("Exception" + ex.Message,ex, activityId);
                throw (new Exception("Exception" + ex.Message));
            }

        }
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployees(int id)
        {
            var activityId = Guid.NewGuid();
            _Logger.LogInformation("Get Employee details started.", activityId);
            try
            {
                var emp = await _employeeService.GetEmployeebyId(id, activityId);
                _Logger.LogInformation("Get  Employee by id services Complted.", activityId);
                _Logger.LogInformation("Get Employee details completed.", activityId);
                return Ok(emp);
            }
            catch (Exception ex)
            {

                _Logger.LogError("Exception" + ex.Message, ex, activityId);
                throw (new Exception("Exception" + ex.Message));
            }


        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult<Employee>> DeleteEmployee(int id)
        {
            var activityId = Guid.NewGuid();
            _Logger.LogInformation("Soft delete Employee started.", activityId);
            try
            {
                
                await _employeeService.DeleteEmployee(id, activityId);
                _Logger.LogInformation($"Soft delete Employee completed. Employee Id: {id}", activityId);
                return Ok();
            }
            catch (Exception ex)
            {

                _Logger.LogError("Exception" + ex.Message, ex, activityId);
                throw (new Exception("Exception" + ex.Message));
            }

        }
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetAllEmployees()
        {
            var activityId = Guid.NewGuid();
            _Logger.LogInformation("Get non deleted Employee started.", activityId);
            try
            {
                
                var employees = await _employeeService.GetAllEmployees(activityId);
                _Logger.LogInformation("Get all Employee services completed.", activityId);
                _Logger.LogInformation("Get non deleted Employee complted.", activityId);
                return Ok(employees);
            }
            catch (Exception ex)
            {

                _Logger.LogError("Exception" + ex.Message, ex, activityId);
                throw (new Exception("Exception" + ex.Message));
            }
        }
    }

}
