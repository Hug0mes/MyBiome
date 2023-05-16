using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBiome.Infrastructure;
using MyBiome.Models;
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
            return View();
        }

        public IActionResult Email()
        {
            return View();
        }


        public async Task<IActionResult> Favorites()
        {
           
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var favorites = await _context.Favorites
                .Where(f => f.CostumerId == userId)
                .Include(f => f.Products) // inclui os dados dos produtos
                .ToListAsync();

            return View(favorites);
        }

        public IActionResult Orders()
        {
            return View();
        }

        public IActionResult AccountSettings()
        {
            return View();
        }

      
        public ActionResult PartialView()
        {
            return PartialView();
        }
    }
}
