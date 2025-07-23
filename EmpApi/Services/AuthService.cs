using EmpApi.Repository;
using EmpApi.Services;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
public class AuthService : IAuthService
{
    private readonly IAuthRepository _authRepository;
    private readonly IConfiguration _configuration;

    public AuthService(IAuthRepository authRepository, IConfiguration configuration)
    {
        _authRepository = authRepository;
        _configuration = configuration;
    }

    public async Task<string> Authenticate(string username, string password)
    {
        var user = await _authRepository.GetUserByUsernameAsync(username);

        //Console.WriteLine(user.Username);
        //Console.WriteLine(user.Password);
        //Console.WriteLine(password);
        ////if(user.Password.Trim().Equals(password.Trim())) Console.WriteLine("Password match");
        //else Console.WriteLine("Password mismatch");
        if (user.Username == null || user.Password.Trim() != password.Trim())
            return null;

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);
        //Console.WriteLine("User not null");
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] {
                new Claim(ClaimTypes.Name, user.Username)                
            }),
            Expires = DateTime.UtcNow.AddHours(1),
            Issuer = _configuration["Jwt:Issuer"],
            Audience = _configuration["Jwt:Audience"],
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        //Console.WriteLine(token);
        return tokenHandler.WriteToken(token);
    }
}
