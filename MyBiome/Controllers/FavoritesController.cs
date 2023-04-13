using Microsoft.AspNetCore.Mvc;

namespace MyBiome.Controllers
{
    public class FavoritesController : Controller
    {
        public IActionResult Favorites()
        {
            return View();
        }
    }
}
