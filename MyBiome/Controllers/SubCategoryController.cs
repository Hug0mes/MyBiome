using MyBiome.Models;
using MyBiome.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using NToastNotify;
using MyBiome.Infrastructure.Components;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyBiome.Helpers;

namespace MyBiome.Controllers
{
    public class SubCategoryController : Controller
    {
        private readonly DataContext _context;
        private readonly IToastNotification _toastNotification;

        public SubCategoryController(DataContext context, IToastNotification toastNotification)
        {
            _toastNotification = toastNotification;
            _context = context;
        }

        // GET: SubCategories
        public async Task<IActionResult> IndexAsync()
        {
            var SubCategories = await _context.SubCategories.Include(sc=>sc.Category).ToListAsync();
            return View(SubCategories);
        }

        // GET: SubCat/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Subcat = await _context.SubCategories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (Subcat == null)
            {
                return NotFound();
            }

            return View(Subcat);
        }

        // GET: Subcat/Create
        public IActionResult Create()
        {
            IEnumerable<SelectListItem> catList = DBHelper.FillCategories(_context);
            ViewBag.catList = catList;
            return View();
        }

        // POST: SubCategory/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SubCategory subCategory)
        {
            if (ModelState.IsValid)
            {
                await SaveSubCat(subCategory);
                _toastNotification.AddSuccessToastMessage($"Sub-Category {subCategory.Name} was insert sucessfuly");
                return RedirectToAction(nameof(Index));
            }
            return View(subCategory);
        }

        private async Task SaveSubCat(SubCategory subCategory)
        {
            try
            {
                _context.SubCategories.Add(subCategory);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                // Handle exceptions here
                // You can log the exception and/or show an error message to the user
                // For example, if the subCategory.CategoryId value does not exist in the Category table, 
                // it will throw a DbUpdateException
                // You can catch the exception and handle it as you see fit
                // Here's an example of how to log the exception:
                Console.WriteLine(ex);
                // Here's an example of how to show an error message to the user:
                ModelState.AddModelError("", "Error saving sub-category. Please try again later.");
            }
        }


    }
}
