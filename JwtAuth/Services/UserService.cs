using JwtAuth.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Security.Claims;

namespace JwtAuth.Services
{
    public interface IUserService : IDisposable
    {
        UserModel GetCurrentUser(HttpContext httpContext);
    }
    public class UserService : IUserService
    {
        private bool disposed = false;
        private readonly IConfiguration _config;
        public UserService(IConfiguration configuration)
        {
            _config = configuration;
        }

        public UserModel GetCurrentUser(HttpContext httpContext)
        {
            var identity = httpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                var userClaims = identity.Claims;
                return new UserModel()
                {
                    Username = userClaims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.NameIdentifier))?.Value,
                    EmailAddress = userClaims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Email))?.Value,
                    GivenName = userClaims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.GivenName))?.Value,
                    Surname = userClaims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Surname))?.Value,
                    Role = userClaims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Role))?.Value,
                };
            }
            return null;
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
