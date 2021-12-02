using JwtAuth.Models;
using JwtAuth.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JwtAuth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private ILoginService _loginService;

        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login([FromBody] UserLoginModel userLogin)
        {
            var user = _loginService.Authenticate(userLogin);
            if (user != null)
            {
                string token = _loginService.GenerateJwt(user);
                return Ok(token);
            }

            return NotFound();
        }
    }
}