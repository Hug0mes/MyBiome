using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MyBiome.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {

        //[Authorize]
        public IActionResult Index()
        {
            return View();
        }
    }
}
