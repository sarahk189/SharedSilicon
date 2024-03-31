using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
