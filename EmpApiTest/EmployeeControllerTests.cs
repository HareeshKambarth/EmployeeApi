using EmpApi.Controllers;
using EmpApi.Logging;
using EmpApi.Models;
using EmpApi.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.ComponentModel.DataAnnotations;
namespace EmpApiTest
{
    public class EmployeeControllerTests
    {
        private readonly Mock<IEmployeeService> _employeeServiceMock;
        private readonly Mock<ILoggingService> _loggerMock;
        private readonly EmployeeController _controller;
        
        public  EmployeeControllerTests()
        {
            _employeeServiceMock = new Mock<IEmployeeService>();
            _loggerMock = new Mock<ILoggingService>();
            _controller = new EmployeeController(_employeeServiceMock.Object, _loggerMock.Object);
        }
        //[Fact]
        //public async Task AddEmployeeTest()
        //{
        //    var newEmployee = new Employee
        //    {
        //        Id = 1,
        //        Name = "Nam",
        //        Address = "123 Main",
        //        Email = "john@eample.com",
        //        RegistrationDate = Convert.ToDateTime("2025-07-25T10:54:00.550Z"),
        //        ActiveStatus = 1,
        //        Delete = 0
        //    };


        //    var validationContext = new ValidationContext(newEmployee);
        //    var validationResults = new List<ValidationResult>();
        //    Validator.TryValidateObject(newEmployee, validationContext, validationResults, true);

        //    foreach (var validationResult in validationResults)
        //    {
        //        _controller.ModelState.AddModelError(validationResult.MemberNames.First(), validationResult.ErrorMessage);
        //    }

        //    var actionResult = await _controller.AddEmployee(newEmployee);
        //    var result = actionResult.Result;

        //    // Assert
        //    //var badRequest = Assert.IsType<BadRequestObjectResult>(result);
        //    var okResult = Assert.IsType<OkResult>(result);

        [Theory]
        [InlineData("John Doe", "123 Main", "john@example.com", "2025-07-25T10:54:00.550Z", 1, 0, true)] // valid
        [InlineData("", "123 Main", "john@example.com", "2025-07-25T10:54:00.550Z", 1, 0, false)] // Empty name
        [InlineData("John Doe", "123 Main", "", "2025-07-25T10:54:00.550Z", 1, 0, false)] // Empty email
        [InlineData("Jane Doe", "456 Main", "invalid-email", "2025-07-25T10:54:00.550Z", 1, 0, false)] // invalid email
        public async Task AddEmployeeTest(string name,string address, string email, string registrationDate,int activeStatus,int delete,bool isValid)
        {
            // Arrange
            var newEmployee = new Employee
            {
                Id = 1,
                Name = name,
                Address = address,
                Email = email,
                RegistrationDate = DateTime.Parse(registrationDate),
                ActiveStatus = activeStatus,
                Delete = delete
            };

            // Run validation manually
            var validationContext = new ValidationContext(newEmployee);
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(newEmployee, validationContext, validationResults, true);

            foreach (var validationResult in validationResults)
            {
                _controller.ModelState.AddModelError(validationResult.MemberNames.First(), validationResult.ErrorMessage);
            }

            // Act
            var actionResult = await _controller.AddEmployee(newEmployee);
            var result = actionResult.Result;

            // Assert
            if (isValid)
            {
                Assert.IsType<OkResult>(result);
            }
            else
            {
                Assert.IsType<BadRequestObjectResult>(result);
            }

        }
        [Theory]
        [InlineData("John Doe", "123 Main", "john@example.com", "2025-07-25T10:54:00.550Z", 1, 0, true)] // valid
        [InlineData("", "123 Main", "john@example.com", "2025-07-25T10:54:00.550Z", 1, 0, false)] // Empty name
        [InlineData("John Doe", "123 Main", "", "2025-07-25T10:54:00.550Z", 1, 0, false)] // Empty email
        [InlineData("Jane Doe", "456 Main", "invalid-email", "2025-07-25T10:54:00.550Z", 1, 0, false)] // invalid email
        public async Task UpdateEmployeeTest(string name, string address, string email, string registrationDate, int activeStatus, int delete, bool isValid)
        {
            var newEmployee = new Employee
            {
                Id = 1,
                Name = name,
                Address = address,
                Email = email,
                RegistrationDate = DateTime.Parse(registrationDate),
                ActiveStatus = activeStatus,
                Delete = delete
            };
            // Run validation manually
            var validationContext = new ValidationContext(newEmployee);
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(newEmployee, validationContext, validationResults, true);

            foreach (var validationResult in validationResults)
            {
                _controller.ModelState.AddModelError(validationResult.MemberNames.First(), validationResult.ErrorMessage);
            }
            // Act
            var actionResult = await _controller.AddEmployee(newEmployee);
            var result = actionResult.Result;

            // Assert
            if (isValid)
            {
                Assert.IsType<OkResult>(result);
            }
            else
            {
                Assert.IsType<BadRequestObjectResult>(result);
            }

        }
        [Fact]
        public async Task GetEmployeesTest()
        {
            // Arrange
            var employeeId = 1;
            var mockEmployee = new Employee
            {
                Id = employeeId,
                Name = "Test Employee",
                Address = "Test Address",
                Email = "test@example.com",
                RegistrationDate = DateTime.Now,
                ActiveStatus = 1,
                Delete = 0
            };

            _employeeServiceMock
                .Setup(service => service.GetEmployeebyId(It.IsAny<int>(), It.IsAny<Guid>()))
                .ReturnsAsync(mockEmployee);

            // Act
            var result = await _controller.GetEmployees(employeeId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedEmployee = Assert.IsType<Employee>(okResult.Value);
            Assert.Equal(employeeId, returnedEmployee.Id);
        }


    }

}    

