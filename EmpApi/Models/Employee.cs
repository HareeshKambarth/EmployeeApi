using Microsoft.AspNetCore.Http.HttpResults;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;
using System.Xml.Linq;

namespace EmpApi.Models
{
    public class Employee
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(20, ErrorMessage = "Name cannot be longer than 20 characters.")]
        public string Name { get; set; }
        public string Address { get; set; }
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        [Required]
        [Column("Registration_Date")]
        public DateTime RegistrationDate { get; set; }
        [Column("Active_status")]
        public int ActiveStatus { get; set; }
        public int Delete { get; set; }
    }
}

