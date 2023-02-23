using Microsoft.AspNetCore.Mvc;

namespace MyBiome.Controllers
{
	public class ProductsController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
