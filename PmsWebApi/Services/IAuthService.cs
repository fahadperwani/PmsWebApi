using Microsoft.AspNetCore.Identity;
using PmsWebApi.Models;
using System.IdentityModel.Tokens.Jwt;

namespace PmsWebApi.Services
{
    public interface IAuthService
    {
        Task<IdentityResult> RegisterUser(Registration User);

        Task<string> LoginUser(Login User);
    }
}