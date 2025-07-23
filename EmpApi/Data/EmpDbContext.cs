using EmpApi.Models;
using Microsoft.EntityFrameworkCore;
using EmpApi.Models;
namespace EmpApi.Data
{
    public class EmpDbContext:DbContext
    {
        public EmpDbContext(DbContextOptions<EmpDbContext> options):base(options)
        {
        }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
