using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBiome.Infrastructure;
using MyBiome.Models;
   
namespace MyBiome.Controllers
{
    public class SuppliersController : Controller
    {
        private readonly DataContext _context;

        public SuppliersController(DataContext context)
        {
            _context = context;
        }

        // GET: Suppliers
        public IActionResult Index()
        {
            var suppliers = _context.Suppliers.ToList();
            return View(suppliers);
        }

        // GET: Suppliers/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var supplier = _context.Suppliers
                .FirstOrDefault(m => m.Id == id);

            if (supplier == null)
            {
                return NotFound();
            }

            return View(supplier);
        }

        // GET: Suppliers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Suppliers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,CompanyName,ContactName,ContactEmail,phone,StreetAddress,City,State,PostalCode")] Suppliers suppliers)
        {
            if (ModelState.IsValid)
            {
                _context.Add(suppliers);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(suppliers);
        }

        // GET: Suppliers/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var supplier = _context.Suppliers.Find(id);

            if (supplier == null)
            {
                return NotFound();
            }
            return View(supplier);
        }

        // POST: Suppliers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,CompanyName,ContactName,ContactEmail,phone,StreetAddress,City,State,PostalCode")] Suppliers suppliers)
        {
            if (id != suppliers.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(suppliers);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SupplierExists((int)suppliers.Id))
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
            return View(suppliers);
        }

        // GET: Suppliers/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var supplier = _context.Suppliers
                .FirstOrDefault(m => m.Id == id);

            if (supplier == null)
            {
                return NotFound();
            }

            return View(supplier);
        }

        // POST: Suppliers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var supplier = _context.Suppliers.Find(id);
            _context.Suppliers.Remove(supplier);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool SupplierExists(int id)
        {
            return _context.Suppliers.Any(e => e.Id == id);
        }


     }
}
