using Microsoft.AspNetCore.Mvc;

namespace MyBiome.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
