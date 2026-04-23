using Microsoft.AspNetCore.Mvc;

namespace ProductPackaging.Controllers
{
    public class ProductsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
