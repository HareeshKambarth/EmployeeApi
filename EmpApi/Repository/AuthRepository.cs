using EmpApi.Data;
using EmpApi.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.Data;

namespace EmpApi.Repository
{
    public class AuthRepository : IAuthRepository
    {
        private readonly EmpDbContext _context;

        public AuthRepository(EmpDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            var param = new SqlParameter("@username", username);            
            var user = await _context.Users
                .FromSqlRaw("EXEC GetUserByUsername @username", param)
                .ToListAsync();
            
            return user.FirstOrDefault();
        }

    }
}
