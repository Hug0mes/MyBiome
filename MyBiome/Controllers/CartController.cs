using Microsoft.AspNetCore.Mvc;

namespace MyBiome.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
