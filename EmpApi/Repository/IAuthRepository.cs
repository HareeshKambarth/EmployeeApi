using EmpApi.Models;

namespace EmpApi.Repository
{
    public interface IAuthRepository
    {
        Task<User> GetUserByUsernameAsync(string username);
    }
}
