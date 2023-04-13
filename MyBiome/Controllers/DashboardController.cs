using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MyBiome.Controllers
{
    public class DashboardController : Controller
    {

        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        public ActionResult PartialView()
        {
            return PartialView();
        }
    }
}
