using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyBiome.Helpers;
using MyBiome.Infrastructure;
using MyBiome.Models;
using NToastNotify;
using MyBiome.Helpers;
using Microsoft.AspNetCore.Identity;

namespace MyBiome.Controllers
{
    public class ProductsController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly DataContext _context;
        private readonly IWebHostEnvironment _appEnvironment;
        private readonly IToastNotification _toastNotification;
        private readonly string _imageFolder;


        public ProductsController(DataContext context, IWebHostEnvironment appEnvironment, IToastNotification toastNotification, UserManager<AppUser> userManager)
        {
            _context = context;
            _appEnvironment = appEnvironment;
            _toastNotification = toastNotification;
            _imageFolder = Path.Combine(_appEnvironment.WebRootPath, "images\\Products");
            _userManager = userManager;
        }


        // GET: Products
        public IActionResult Index()
        {
            List<Products> products = _context.Products
                .Include(p => p.SubCategory)
                .Include(p => p.SubCategory.Category).ToList();
            return View(products);
        }

        // GET: Products1
        public IActionResult ListProducts()
        {



            List<Products> products = _context.Products.Include(p => p.SubCategory).Where(p => p.Status == "Active").ToList();
            List<Category> categories = _context.Categories.ToList();

        

            ProductsViewModel viewModel = new ProductsViewModel()
            {
                ProductsList = products,
                Categories = categories
                //,
                //favorites = favorites
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
                .Include(p => p.SubCategory)
                .Include(p => p.SubCategory.Category)
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
            //IEnumerable<SelectListItem> scatList = DBHelper.FillSubCategories(_context);
            //ViewBag.scatList = scatList;
            ViewBag.scatList = _context.SubCategories
               .Select(x => new SelectListItem
               {
                   Value = x.Id.ToString(),
                   Text = x.Name
               })
               .ToList();
            return View();
        }

        // POST: Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductsViewModel ProductsVM)
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

        [HttpGet]
        public IActionResult Edit(int id)
        {
            // Obtenha o produto pelo ID do banco de dados
            var product = _context.Products.Find(id);

            if (product == null)
            {
                return NotFound();
            }

            // Crie o objeto ProductsViewModel e defina suas propriedades com os dados do produto
            var viewModel = new ProductsViewModel
            {
                Products = product
            };

            // Preencha a lista de categorias para o dropdown
            ViewBag.scatList = _context.SubCategories
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Name
                })
                .ToList();

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditAsync(ProductsViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                // Obtenha o produto original do banco de dados
                var product = _context.Products.Find(viewModel.Products.Id);

                if (product == null)
                {
                    return NotFound();
                }

                // Atualize as propriedades do produto com os dados do view model
                product.Name = viewModel.Products.Name;
                product.Description = viewModel.Products.Description;
                product.Price = viewModel.Products.Price;
                product.Status = viewModel.Products.Status;
                product.Height = viewModel.Products.Height;
                product.Whidh = viewModel.Products.Whidh;
                product.Stock = viewModel.Products.Stock;

                // Atualize outras propriedades, se necessário
                await SaveProduct(viewModel);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            // Preencha a lista de categorias para o dropdown
            ViewBag.scatList = _context.SubCategories
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Name
                })
                .ToList();

            return View(viewModel);
        }




        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }

        public IActionResult Moredetails()
        {

            return View();
        }
        
        public IActionResult delete(int id)
        {
            var product =  _context.Products.Find(id);
            if (product != null)
            {
                product.Status = "Inactive";
                _context.SaveChanges();
            }

            return RedirectToAction("Index"); // Redireciona para a página principal ou ação desejada após a inativação do produto
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



        public ActionResult ListProductsFilter(int sortoption)
        {
            // Obtenha a lista de produtos do seu modelo
            List<Products> productsList = _context.Products.Where(p => p.Status == "Active").ToList();
            // Aplique a lógica de filtragem com base no valor de SortOption
            if (sortoption == 1)
            {
                // Ordenar por melhores vendas
                productsList = productsList.OrderByDescending(p => p.Stock).ToList();
            }
            else if (sortoption == 2)
            {
                // Ordenar por preço crescente
                productsList = productsList.OrderBy(p => p.Price).ToList();
            }
            else if (sortoption == 3)
            {
                // Ordenar por preço decrescente
                productsList = productsList.OrderByDescending(p => p.Price).ToList();
            }
            else if (sortoption == 4)
            {
                // Ordenar por classificação alta para baixa
                productsList = productsList.ToList();
            }
            else
            {
                // Ordenar por itens mais recentes (opção padrão)
                productsList = productsList.ToList();
            }

            // Outras lógicas relevantes...
            List<Category> categories = _context.Categories.ToList();


            ProductsViewModel viewModel = new ProductsViewModel()
            {
                ProductsList = productsList,
                Categories = categories
                //,
                //favorites = favorites
            };
            return View("ListProducts",viewModel);
        }


    }
}
