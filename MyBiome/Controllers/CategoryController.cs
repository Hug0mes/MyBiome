using MyBiome.Models;
using MyBiome.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using NToastNotify;
using MyBiome.Infrastructure.Components;

namespace YourNamespace.Controllers
{
    public class CategoryController : Controller
    {
        private readonly DataContext _context;
        private readonly IWebHostEnvironment _appEnvironment;
        private readonly IToastNotification _toastNotification;
        private readonly string _imageFolder;

        public CategoryController(DataContext context, IWebHostEnvironment appEnvironment, IToastNotification toastNotification)
        {
            _context = context;
            _appEnvironment = appEnvironment;
            _toastNotification = toastNotification;
            _imageFolder = Path.Combine(_appEnvironment.WebRootPath, "images\\categories");
        }

        //GET: Category
        public async Task<IActionResult> Index()
        {
            var categories = await _context.Categories.ToListAsync();
            return View(categories);
        }


        // GET: Category/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // GET: Category/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Category/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryViewModel categoryVM)
        {
            if (ModelState.IsValid)
            {
                await SaveCategorie(categoryVM);
                _toastNotification.AddSuccessToastMessage($"Category {categoryVM.Category.Name} was insert sucessfuly");
                return RedirectToAction(nameof(Index));
            }
            return View(categoryVM);
        }

    
        // GET: Category/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: Category/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Category category)
        {
            if (id != category.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(category);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        private bool CategoryExists(int id)
        {
            throw new NotImplementedException();
        }

        // GET: Category/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> SaveCategorie(CategoryViewModel CategoriesVM)
        {
            try
            {
                // File included?
                string uniqueFileName = UploadedFile(CategoriesVM);
                if ((uniqueFileName != null) || (CategoriesVM.RemoverImagem))
                {
                    // Previous image?
                    if (CategoriesVM.Category.Image != null)
                    {
                        // remove file
                        string filePath = Path.Combine(_imageFolder, CategoriesVM.Category.Image);
                        System.IO.File.Delete(filePath);

                    }
                    // Set filename image in Artigo
                    CategoriesVM.Category.Image = uniqueFileName;
                }

                // Save Categories
                if (CategoriesVM.Category.Id == 0)
                {
                    //_context.Entry(Categories.Categories).State = EntityState.Added;
                    _context.Add(CategoriesVM.Category);
                }
                else
                {
                    //_context.Entry(Categories.Categories).State = EntityState.Modified;
                    _context.Update(CategoriesVM.Category);
                }
                // Store everything in db
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                // TODO: Handle failure
                return false;
            }
            //catch (DbUpdateConcurrencyException)
            //{
            //}
        }

        private string UploadedFile(CategoryViewModel CategoriesVM)
        {
            string uniqueFileName = null;

            if (CategoriesVM.ImageFile != null)
            {
                System.IO.Directory.CreateDirectory(_imageFolder);
                uniqueFileName = Guid.NewGuid().ToString()
                        + "_"
                        + System.IO.Path.GetFileName(CategoriesVM.ImageFile.FileName);
                string filePath = Path.Combine(_imageFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    CategoriesVM.ImageFile.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }

    }
}
