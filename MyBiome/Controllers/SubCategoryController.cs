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
                _toastNotification.AddSuccessToastMessage($"SubCategory {subCategory.Name} was insert sucessfuly");
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
              
                Console.WriteLine(ex);
                // Here's an example of how to show an error message to the user:
                ModelState.AddModelError("", "Error saving sub-category. Please try again later.");
            }
        }


        // GET: SubCategory/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subCategory = await _context.SubCategories
                .Include(s => s.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (subCategory == null)
            {
                return NotFound();
            }

            return View(subCategory);
        }

        // POST: SubCategory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var subCategory = await _context.SubCategories.FindAsync(id);
            if (subCategory == null)
            {
                return NotFound();
            }

            _context.SubCategories.Remove(subCategory);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }

        // GET: SubCategory/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subCategory = await _context.SubCategories.FindAsync(id);
            if (subCategory == null)
            {
                return NotFound();
            }

            // Se necessário, você pode popular uma lista de categorias para exibir no formulário de edição
            ViewBag.Categories = await _context.Categories.ToListAsync();

            return View(subCategory);
        }

        // POST: SubCategory/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SubCategory subCategory)
        {
            if (id != subCategory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(subCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SubCategoryExists(subCategory.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "Home");
            }

           
             ViewBag.Categories = await _context.Categories.ToListAsync();

            return View(subCategory);
        }

        private bool SubCategoryExists(int id)
        {
            return _context.SubCategories.Any(e => e.Id == id);
        }

    }
}
