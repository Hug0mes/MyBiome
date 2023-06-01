using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using System.Web;
using Microsoft.EntityFrameworkCore;
using MyBiome.Infrastructure;
using MyBiome.Models;
using MyBiome.Models.ViewModels;
using NToastNotify;
using System.Security.Claims;


namespace MyBiome.Controllers
{   
    [Authorize]
    public class FavoritesController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IToastNotification _toastNotification;

        public FavoritesController(UserManager<AppUser> userManager, DataContext context, IHttpContextAccessor httpContextAccessor, IToastNotification nToastNotify)
        {
            _context = context;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _toastNotification = nToastNotify;
        }


        public async Task<IActionResult> Favorites()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var favorites = await _context.Favorites
                .Where(f => f.CostumerId == userId)
                .Include(f => f.Products)
                .ToListAsync();

            // Envia a lista de favoritos para a view
            return View(favorites);
        }

        public async Task<IActionResult> Index(string customerId)
        {
            var favorites = await _context.Favorites
                .Where(f => f.CostumerId == customerId)
                .Include(f => f.Products)
                .Include(f => f.AppUser)
                .ToListAsync();

            return View(favorites);
        }

        [HttpGet]
        [HttpPost]
        public async Task<IActionResult> AddToFavorite(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }

            string userId = _userManager.GetUserId(User);
            var favorite = new Favorites
            {
                CostumerId = userId,
                ProductId = id,
            };

            _context.Favorites.Add(favorite);
            await _context.SaveChangesAsync();

           _toastNotification.AddSuccessToastMessage($"The product has been added to favorites!");

            return Redirect(Request.Headers["Referer"].ToString());
        }

        [HttpPost]
        public async Task<IActionResult> Remove(int id)
        {
            var favorite = await _context.Favorites.FindAsync(id);

            if (favorite == null)
            {
                return NotFound();
            }

            _context.Favorites.Remove(favorite);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", new { customerId = favorite.CostumerId });
        }
    }

    //public bool IsProductFavorite(int productId)
    //{
    //    var user = _userManager.GetUserAsync(User).Result;
    //    if (user != null)
    //    {
    //        // Verificar se o produto está na lista de favoritos do usuário
    //        var favorite = _context.Favorites.FirstOrDefault(f => f.CostumerId == user.Id && f.ProductId == productId);
    //        return favorite != null;
    //    }

    //    return false;
    //}


}
