using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBiome.Infrastructure;
using MyBiome.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using MyBiome.Models.ViewModels;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption.ConfigurationModel;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using NToastNotify;

namespace MyBiome.Controllers
{
    public class CartController : Controller
    {

        private readonly DataContext _context;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
		private readonly IToastNotification _toastNotification;
		public CartController(DataContext context, SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, IToastNotification toastNotification)
        {
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;
			_toastNotification = toastNotification;

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
		
			_toastNotification.AddSuccessToastMessage($"The product has been added!");
		

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

        [Authorize]
        public IActionResult Checkout()
        {
            List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart") ?? new List<CartItem>();

            CheckoutViewModel CheckVM = new()
            {
                CartItems = cart,

                GrandTotal = cart.Sum(x => x.Quantity * x.Price)
            };


            return View(CheckVM);
        }

        public IActionResult Thankyou()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateOrder(Orders orders)
        {
            if (ModelState.IsValid)
            {
                List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart");

                orders.UserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                orders.CartItems = cart;
                orders.OrderStatus = "pending";
                orders.OrderDate = DateTime.Now;
                orders.GrandTotal = cart.Sum(x => x.Quantity * x.Price);


				_context.Orders.Add(orders);
                await _context.SaveChangesAsync();
          
                int orderId = orders.Id;
                foreach (var item in cart)
                {
                    OrderItems orderItem = new OrderItems
                    {
                        OrderId = orderId,
                        ProductId = item.ProductId,
                        Quantity = item.Quantity,
                        Price = item.Price
                    };

                    _context.OrderItems.Add(orderItem);
                }

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Thankyou));

            }

            // Se o modelo não for válido, retorne a view com os erros de validação
            return RedirectToAction(nameof(Index));
        }
    }
}
