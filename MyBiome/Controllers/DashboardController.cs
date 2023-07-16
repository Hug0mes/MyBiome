using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBiome.Infrastructure;
using MyBiome.Models;
using NToastNotify;
using System.Linq;
using System.Security.Claims;

namespace MyBiome.Controllers
{
    public class DashboardController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DashboardController(UserManager<AppUser> userManager, DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }



        [Authorize]
        public IActionResult Index()
        {
            var id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            ViewBag.TotalPurchases = _context.Orders.Where(t => t.UserId == id).Count();

           

            if(_context.Orders.Where(t => t.UserId == id).Count() > 0)
            {
                ViewBag.AveragePurchases = (int)_context.Orders.Where(t => t.UserId == id).Average(t => t.GrandTotal);
            }
            else
            {
                ViewBag.AveragePurchases = 0;
            }

            ViewBag.O2saved = _context.Orders.Where(t => t.UserId == id).Count()*1.2 ;

            ViewBag.TotalPlants = _context.Products.Count();


            return View();
        }

        public IActionResult Email()
        {
            return View();
        }


        public IActionResult Favorites()
        {

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Recuperar os produtos favoritos do banco de dados para o usuário atualmente autenticado
            List<Favorites> favoriteItems = _context.Favorites
                .Where(f => f.CostumerId == userId)
                .Include(f => f.Products)
                .ToList();

            // Extrair a lista de IDs dos produtos favoritos
            List<int> favoriteProductIds = favoriteItems.Select(f => f.ProductId).ToList();

            // Recuperar os detalhes dos produtos favoritos com base nos IDs
            List<Products> favoriteProducts = _context.Products
                .Where(p => favoriteProductIds.Contains(p.Id))
                .ToList();

            // Envia a lista de favoritos para a view
            return View(favoriteProducts);
        }
        // GET: Dashboard/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var favorite = await _context.Favorites
                .Where(t => t.CostumerId == userId)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (favorite == null)
            {
                return NotFound();
            }

            return View(favorite);
        }

        // POST: Dashboard/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var favorite = await _context.Favorites.FindAsync(id);
            if (favorite == null)
            {
                return NotFound();
            }

            _context.Favorites.Remove(favorite);
            await _context.SaveChangesAsync();
            return RedirectToAction("favorites", "Dashboard");
        }

        public IActionResult Orders()
        {
            return View();
        }

        public IActionResult AccountSettings()
        {
            return View();
        }

   //     public async Task<IActionResult> Logout()
   //     {
			//Session.Clear();
			//return RedirectToAction("Index", "Home");
   //     }

        public ActionResult PartialView()
        {
            return PartialView();
        }
    }
}
