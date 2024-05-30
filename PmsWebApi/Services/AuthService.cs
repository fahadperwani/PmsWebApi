using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using PmsWebApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PmsWebApi.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _config;
        private readonly UserManager<User> _userManager;

        public AuthService(UserManager<User> userManager, IConfiguration config)
        {
            _userManager = userManager;
            _config = config;
        }

        public async Task<string> LoginUser(Login User)
        {
            var user = await _userManager.FindByEmailAsync(User.Email);

            if (user == null) return null;

            var result = await _userManager.CheckPasswordAsync(user, User.Password);

            if (!result) return null;

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);


            var claims = new[]
            {
                new Claim("Email", User.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim("Name", user.Name)
            };

            var token = new JwtSecurityToken(claims: claims, expires: DateTime.Now.AddMinutes(60), issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"], signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<IdentityResult> RegisterUser(Registration User)
        {
            var user = new User { UserName = User.Name, Email = User.Email, isDoctor = User.isDoctor, Name = User.Name };

            if (user.isDoctor) user.Specialization = User.Specialization;

            var result = await _userManager.CreateAsync(user, User.Password);

            return result;
        }

        public async Task<string> GetUserId(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var SecretKey = _config["Jwt:Key"];
            var key = Encoding.ASCII.GetBytes(SecretKey);

            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;
            var user =  jwtToken.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;
            return user;
        }
    }
}
