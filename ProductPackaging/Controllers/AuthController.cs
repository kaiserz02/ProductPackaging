using Microsoft.AspNetCore.Mvc;

namespace ProductPackaging.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
