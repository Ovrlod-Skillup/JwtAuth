using Microsoft.AspNetCore.Mvc;

namespace JwtAuth.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}