using Microsoft.AspNetCore.Mvc;

namespace MyBiome.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
