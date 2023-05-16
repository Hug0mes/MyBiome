using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyBiome.Helpers;
using MyBiome.Infrastructure;
using MyBiome.Models;
using NToastNotify;
using MyBiome.Helpers;
namespace MyBiome.Controllers
{
    public class ProductsController : Controller
    {
        private readonly DataContext _context;
        private readonly IWebHostEnvironment _appEnvironment;
        private readonly IToastNotification _toastNotification;
        private readonly string _imageFolder;

 
        public ProductsController(DataContext context, IWebHostEnvironment appEnvironment, IToastNotification toastNotification)
        {
            _context = context;
            _appEnvironment = appEnvironment;
            _toastNotification = toastNotification;
            _imageFolder = Path.Combine(_appEnvironment.WebRootPath, "images\\Products");
        }


        // GET: Products
        public IActionResult Index()
        {
            List<Products> products = _context.Products.Include(p => p.SubCategory).ToList();
            return View(products);
        }

        // GET: Products1
        public IActionResult ListProducts()
        {
			
			List<Products> products = _context.Products.Include(p => p.SubCategory).ToList();
            List<Category> categories = _context.Categories.ToList();

            ProductsViewModel viewModel = new ProductsViewModel()
            {
                ProductsList = products,
                Categories = categories
            };
            return View(viewModel);
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        private List<Category> GetCategories()
        {
            return _context.Categories.ToList();
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            IEnumerable<SelectListItem> scatList = DBHelper.FillSubCategories(_context);
            ViewBag.scatList = scatList;
            return View();
        }

        // POST: Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( ProductsViewModel ProductsVM)
        {
           
            if (ModelState.IsValid)
            {

                await SaveProduct(ProductsVM);
                _toastNotification.AddSuccessToastMessage($"Products {ProductsVM.Products.Name} Insert successfully");
                _context.SaveChanges();


                return RedirectToAction(nameof(Index));
            }

        
            return View(ProductsVM);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

           var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Descri,Price,Status,Height,Whidh,Stock,Image1,Image2,Image3")] Products products)
        {
            if (id != products.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(products);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(products.Id))
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
            return View(products);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }

        public IActionResult Moredetails()
        {
       
            return View();
        }

        private async Task<bool> SaveProduct(ProductsViewModel ProductsVM)
        {
            try
            {
                // File included?
                string uniqueFileName1 = UploadedFile1(ProductsVM);
                if ((uniqueFileName1 != null) || (ProductsVM.RemoverImagem))
                {
                    // Previous image?
                    if (ProductsVM.Products.Image1 != null)
                    {
                        // remove file
                        string filePath = Path.Combine(_imageFolder, ProductsVM.Products.Image1);
                        System.IO.File.Delete(filePath);

                    }
                    // Set filename image in Artigo
                    ProductsVM.Products.Image1 = uniqueFileName1;
                  
                }

                string uniqueFileName2 = UploadedFile2(ProductsVM);
                if ((uniqueFileName2 != null) || (ProductsVM.RemoverImagem))
                {
                    // Previous image?
                    if (ProductsVM.Products.Image2 != null)
                    {
                        // remove file
                        string filePath = Path.Combine(_imageFolder, ProductsVM.Products.Image2);
                        System.IO.File.Delete(filePath);

                    }
                    // Set filename image in Artigo
                    ProductsVM.Products.Image2 = uniqueFileName2;

                }

                string uniqueFileName3 = UploadedFile3(ProductsVM);
                if ((uniqueFileName3 != null) || (ProductsVM.RemoverImagem))
                {
                    // Previous image?
                    if (ProductsVM.Products.Image3 != null)
                    {
                        // remove file
                        string filePath = Path.Combine(_imageFolder, ProductsVM.Products.Image3);
                        System.IO.File.Delete(filePath);

                    }
                    // Set filename image in Artigo
                    ProductsVM.Products.Image3 = uniqueFileName3;

                }
                // Save Products
                if (ProductsVM.Products.Id == 0)
                {
                    //_context.Entry(Products.Products).State = EntityState.Added;
                    _context.Add(ProductsVM.Products);
                }
                else
                {
                    //_context.Entry(Products.Products).State = EntityState.Modified;
                    _context.Update(ProductsVM.Products);
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


        private string UploadedFile1(ProductsViewModel ProductsVM)
        {
            string uniqueFileName1 = null;

            if (ProductsVM.ImageFile1 != null)
            {
                System.IO.Directory.CreateDirectory(_imageFolder);
                uniqueFileName1 = Guid.NewGuid().ToString()
                        + "_"
                        + System.IO.Path.GetFileName(ProductsVM.ImageFile1.FileName);
                string filePath = Path.Combine(_imageFolder, uniqueFileName1);
                using var fileStream = new FileStream(filePath, FileMode.Create);
                ProductsVM.ImageFile1.CopyTo(fileStream);
            }
            return uniqueFileName1;
        }

        private string UploadedFile2(ProductsViewModel ProductsVM)
        {
            string uniqueFileName2 = null;

            if (ProductsVM.ImageFile2 != null)
            {
                System.IO.Directory.CreateDirectory(_imageFolder);
                uniqueFileName2 = Guid.NewGuid().ToString()
                        + "_"
                        + System.IO.Path.GetFileName(ProductsVM.ImageFile2.FileName);
                string filePath = Path.Combine(_imageFolder, uniqueFileName2);
                using var fileStream = new FileStream(filePath, FileMode.Create);
                ProductsVM.ImageFile2.CopyTo(fileStream);
            }
            return uniqueFileName2;
        }

        private string UploadedFile3(ProductsViewModel ProductsVM)
        {
            string uniqueFileName3 = null;

            if (ProductsVM.ImageFile3 != null)
            {
                System.IO.Directory.CreateDirectory(_imageFolder);
                uniqueFileName3 = Guid.NewGuid().ToString()
                        + "_"
                        + System.IO.Path.GetFileName(ProductsVM.ImageFile3.FileName);
                string filePath = Path.Combine(_imageFolder, uniqueFileName3);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    ProductsVM.ImageFile3.CopyTo(fileStream);
                }
            }
            return uniqueFileName3;
        }

        [HttpGet]
        public JsonResult GetSubCategories(int categoryId)
        {
            var subcategories = _context.SubCategories.Where(s => s.CategoryId == categoryId).ToList();
            return Json(subcategories);
        }

    }
}
