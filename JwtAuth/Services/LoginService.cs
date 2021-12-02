using JwtAuth.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace JwtAuth.Services
{
    public interface ILoginService : IDisposable
    {
        UserModel Authenticate(UserLoginModel userLogin);
        string GenerateJwt(UserModel user);
    }
    public class LoginService : ILoginService
    {
        private bool disposed = false;
        private readonly IConfiguration _config;
        public LoginService(IConfiguration configuration)
        {
            _config = configuration;
        }

        public UserModel Authenticate(UserLoginModel userLogin)
        {
            var authenticatedUser = UserConstants.Users.FirstOrDefault(x =>
            x.Username.ToLower().Equals(userLogin.Username.ToLower()) &&
            x.Password.Equals(userLogin.Password));

            return authenticatedUser;
        }
        public string GenerateJwt(UserModel user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credientials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Username),
                new Claim(ClaimTypes.Email, user.EmailAddress),
                new Claim(ClaimTypes.GivenName, user.GivenName),
                new Claim(ClaimTypes.Surname, user.Surname),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"], _config["Jwt:Audience"], claims, expires: DateTime.Now.AddMinutes(15), signingCredentials: credientials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                disposed = true;
            }
        }
    }
}