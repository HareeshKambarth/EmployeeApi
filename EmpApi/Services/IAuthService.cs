﻿namespace EmpApi.Services
{
    public interface IAuthService
    {
        Task<string> Authenticate(string username, string password);
    }

}
