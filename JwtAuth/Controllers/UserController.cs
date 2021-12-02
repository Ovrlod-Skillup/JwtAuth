using JwtAuth.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JwtAuth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet("admin")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetAdminUser()
        {
            var user = userService.GetCurrentUser(HttpContext);
            return Ok($"Hi {user.Username}, you are an {user.Role}.");
        }

        [HttpGet("seller")]
        [Authorize(Roles = "Seller")]
        public IActionResult GetSellerUser()
        {
            var user = userService.GetCurrentUser(HttpContext);
            return Ok($"Hi {user.Username}, you are an {user.Role}.");
        }

    }
}