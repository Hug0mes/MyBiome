using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyBiome.Models;
using MyBiome.Models.ViewModels;
using NToastNotify;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using MyBiome.Infrastructure;

namespace MyBiome.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {

        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
		private readonly IToastNotification _toastNotification;
        private readonly DataContext _context;

        public OrderController(UserManager<AppUser> userManager,
		 SignInManager<AppUser> signInManager, IToastNotification toastNotification, DataContext context)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_toastNotification = toastNotification;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var order = await _context.Orders
                .Where(f => f.UserId == id)
                .ToListAsync();

            return View(order);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .FirstOrDefaultAsync(m => m.Id == id);

          
            if (order == null)
            {
                return NotFound();
            }


            var orderItems = _context.OrderItems.Where(t => t.OrderId == order.Id).ToList();
            var productIds = orderItems.Select(item => item.ProductId).ToList();
            var quantities = orderItems.Select(item => item.Quantity).ToList();
            var products = await _context.Products.Where(p => productIds.Contains(p.Id)).ToListAsync();

            MyOrdersViewModel MyOrdersVM = new MyOrdersViewModel
            {
                Orders = order,
                Products = products,
                Quantities = quantities
            };




            return View(MyOrdersVM);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AllOrders()
        {
            
            var order = await _context.Orders
                .ToListAsync();

            return View(order);
        }


        public async Task<IActionResult> edit(int? id)
        {

            var order = _context.Orders.Find(id);
            return View(order);

        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> update(int? id, string status)
        {
             var order = _context.Orders.Find(id);
             if (order == null)
                {
                    return NotFound();
                }


 
            order.OrderStatus = status;
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
            _toastNotification.AddSuccessToastMessage($"Order updated successfully");
           


            return RedirectToAction(nameof(AllOrders));

        }
        }
    }
