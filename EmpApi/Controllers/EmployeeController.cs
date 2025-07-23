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

        //private readonly EmpDbContext _context;
        //public EmployeeController(EmpDbContext context, ILogger<EmployeeController> logger)
        //{
        //    _context = context;
        //    Logger = logger;
        //}
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

        //[Authorize]
        [HttpPost]
        public async Task<ActionResult<Employee>> AddEmployee(Employee employee)
        {
            var activityId = Guid.NewGuid();
            _Logger.LogInformation("Adding Employee started.", activityId);
            try
            {
                //_context.Employee.Add(employee);
                //await _context.SaveChangesAsync();
                await _employeeService.AddEmployee(employee);
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
        //[Authorize]
        //[HttpPut]
        //public async Task<ActionResult<Employee>> UpdateEmployee(Employee employee)
        //{
        //    Logger.LogInformation("Update Employee started.");
        //    try
        //    {
        //        //var emp = await _context.Employee.FindAsync(employee.Id);
        //        await _employeeService.UpdateEmployee(employee);
        //        //emp.Name = employee.Name;
        //        //emp.Address = employee.Address;
        //        //emp.Email = employee.Email;
        //        //emp.RegistrationDate = employee.RegistrationDate;
        //        //emp.ActiveStatus = employee.ActiveStatus;
        //        //emp.Delete = employee.Delete;
        //        //await _context.SaveChangesAsync();
        //        Logger.LogInformation($"Update Employee completed. Employee Id: {employee.Id}");
        //        return Ok();
        //    }
        //    catch (Exception ex)
        //    {

        //        Logger.LogError("Exception" + ex.Message);
        //        throw (new Exception("Exception" + ex.Message));
        //    }



        //}
        //[Authorize]
        //[HttpPut("{id}/status")]
        //public async Task<ActionResult<Employee>> UpdateEmployeeStatus(int id, int status)
        //{
        //    Logger.LogInformation("Update Employee Status started.");
        //    try
        //    {
        //        //var emp = await _context.Employee.FindAsync(id);
        //        //emp.ActiveStatus = status;
        //        //await _context.SaveChangesAsync();
        //        await _employeeService.UpdateEmployeeStatus(id,status);
        //        Logger.LogInformation($"Update Employee Status completed. Employee Id: {id}");
        //        return Ok();
        //    }
        //    catch (Exception ex)
        //    {

        //        Logger.LogError("Exception" + ex.Message);
        //        throw (new Exception("Exception" + ex.Message));
        //    }

        //}
        //[Authorize]
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Employee>> GetEmployees(int id)
        //{
        //    Logger.LogInformation("Get Employee details started.");
        //    try
        //    {
        //        var emp = await _employeeService.GetEmployeebyId(id);

        //        Logger.LogInformation("Get Employee details completed.");
        //        return Ok(emp);
        //    }
        //    catch (Exception ex)
        //    {

        //        Logger.LogError("Exception" + ex.Message);
        //        throw (new Exception("Exception" + ex.Message));
        //    }


        //}

        //[Authorize]
        //[HttpPut("{id}")]
        //public async Task<ActionResult<Employee>> DeleteEmployee(int id)
        //{
        //    Logger.LogInformation("Soft delete Employee started.");
        //    try
        //    {
        //        //var emp = await _context.Employee.FindAsync(id);
        //        //emp.Delete = 1;
        //        //await _context.SaveChangesAsync();
        //        await _employeeService.DeleteEmployee(id);
        //        Logger.LogInformation($"Soft delete Employee completed. Employee Id: {id}");
        //        return Ok();
        //    }
        //    catch (Exception ex)
        //    {

        //        Logger.LogError("Exception" + ex.Message);
        //        throw (new Exception("Exception" + ex.Message));
        //    }

        //}
        //[Authorize]
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Employee>>> GetAllEmployees()
        //{
        //    Logger.LogInformation("Get non deleted Employee started.");
        //    try
        //    {
        //        //var employees = await _context.Employee.Where(e => e.Delete == 0).ToListAsync();
        //        var employees = await _employeeService.GetAllEmployees();
        //        Logger.LogInformation("Get non deleted Employee complted.");
        //        return Ok(employees);
        //    }
        //    catch (Exception ex)
        //    {

        //        Logger.LogError("Exception" + ex.Message);
        //        throw (new Exception("Exception" + ex.Message));
        //    }
        //}
    }

}
