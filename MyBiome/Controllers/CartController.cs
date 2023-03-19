using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBiome.Infrastructure;
using MyBiome.Models;

namespace MyBiome.Controllers
{
    public class CartController : Controller
    {

        private readonly DataContext _context;

        public CartController(DataContext context)
        {
            _context = context;
        }


        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Add(long id)
        {
            Products product = await _context.Products.FindAsync(id);

            List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart") ?? new List<CartItem>();

            CartItem cartItem = cart.Where(c => c.ProductId == id).FirstOrDefault();

            if (cartItem == null)
            {
                cart.Add(new CartItem(product));
            }
            else
            {
                cartItem.Quantity += 1;
            }

            HttpContext.Session.SetJson("Cart", cart);

            TempData["Success"] = "The product has been added!";

            return Redirect(Request.Headers["Referer"].ToString());
        }
    }
}
