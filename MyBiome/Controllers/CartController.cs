using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBiome.Infrastructure;
using MyBiome.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using MyBiome.Models.ViewModels;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption.ConfigurationModel;

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
			List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart") ?? new List<CartItem>();

			CartViewModel cartVM = new()
			{
				CartItems = cart,
				GrandTotal = cart.Sum(x => x.Quantity * x.Price)
			};

			return View(cartVM);
		}


		public async Task<IActionResult> Add(int id)
        {
            Products product = await _context.Products.FindAsync(id);

			var cartViewModel = new CartViewModel();

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


        public async Task<IActionResult> Decrease(long id)
        {
            List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart");

            CartItem cartItem = cart.Where(c => c.ProductId == id).FirstOrDefault();

            if (cartItem.Quantity > 1)
            {
                --cartItem.Quantity;
            }
            else
            {
                cart.RemoveAll(p => p.ProductId == id);
            }

            if (cart.Count == 0)
            {
                HttpContext.Session.Remove("Cart");
            }
            else
            {
                HttpContext.Session.SetJson("Cart", cart);
            }

            TempData["Success"] = "The product has been removed!";

            return RedirectToAction("Index");
        }


        public async Task<IActionResult> Remove(long id)
        {
            List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart");

            cart.RemoveAll(p => p.ProductId == id);

            if (cart.Count == 0)
            {
                HttpContext.Session.Remove("Cart");
            }
            else
            {
                HttpContext.Session.SetJson("Cart", cart);
            }

            TempData["Success"] = "The product has been removed!";

            return RedirectToAction("Index");
        }

        public IActionResult Clear()
        {
            HttpContext.Session.Remove("Cart");

            return RedirectToAction("Index");
        }

        public IActionResult Checkout()
        {
            return View();
        }

        public IActionResult Thankyou()
        {
            return View();
        }

    }
}
