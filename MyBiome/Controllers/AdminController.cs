using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MyBiome.Controllers
{
    public class AdminController : Controller
    {

        //[Authorize]
        public IActionResult Index()
        {
            return View();
        }
    }
}
