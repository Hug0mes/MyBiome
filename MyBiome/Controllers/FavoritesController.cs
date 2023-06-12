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
using Castle.Core.Resource;


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

            // Recuperar os produtos favoritos do banco de dados para o usuário atualmente autenticado
            List<Favorites> favoriteItems = _context.Favorites
                .Where(f => f.CostumerId == userId)
                .Include(f => f.Products)
                .ToList();

            // Extrair a lista de IDs dos produtos favoritos
            List<int> favoriteProductIds = favoriteItems.Select(f => f.Products.Id).ToList();

            // Recuperar os detalhes dos produtos favoritos com base nos IDs
            List<Products> favoriteProducts = _context.Products
                .Where(p => favoriteProductIds.Contains(p.Id))
                .ToList();

            // Envia a lista de favoritos para a view
            return View(favoriteProducts);
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

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var favorite = await _context.Favorites
                .FirstOrDefaultAsync(f => f.CostumerId == userId && f.ProductId == id);

            if (favorite == null)
            {
                return NotFound();
            }

            _context.Favorites.Remove(favorite);
            await _context.SaveChangesAsync();

            return RedirectToAction("favorites", "Dashboard");
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
