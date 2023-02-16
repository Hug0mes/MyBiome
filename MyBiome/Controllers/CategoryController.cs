using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBiome.Infrastructure;
using MyBiome.Models;

namespace MyBiome.Controllers
{
    public class CategoryController : Controller
    {
            private readonly DataContext _context;

            public CategoryController(DataContext context)
            {
                _context = context;
            }

            // GET: categorys
            public async Task<IActionResult> Index()
            {
                return View(await _context.Categories.ToListAsync());
            }

            // GET: categorys/Details/5
            public async Task<IActionResult> Details(int? id)
            {
                if (id == null || _context.Categories == null)
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

            // GET: categorys/Create
            public IActionResult Create()
            {
                return View();
            }

            // POST: categorys/Create
            // To protect from overposting attacks, enable the specific properties you want to bind to.
            // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Create([Bind("Id,Name")] Category categories)
            {
                if (ModelState.IsValid)
                {
                    _context.Add(categories);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                return View(categories);
            }

            // GET: categorys/Edit/5
            public async Task<IActionResult> Edit(int? id)
            {
                if (id == null || _context.Categories == null)
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

            // POST: categorys/Edit/5
            // To protect from overposting attacks, enable the specific properties you want to bind to.
            // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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
                        if (!Category.CategoryExists(category.Id))
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

            // GET: categorys/Delete/5
            public async Task<IActionResult> Delete(int? id)
            {
                if (id == null || _context.Categories == null)
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

            // POST: categorys/Delete/5
            [HttpPost, ActionName("Delete")]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> DeleteConfirmed(int id)
            {
                if (_context.Categories == null)
                {
                    return Problem("Entity set 'DataContext.categorys'  is null.");
                }
                var category = await _context.Categories.FindAsync(id);
                if (category != null)
                {
                    _context.Categories.Remove(category);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            private bool CategoryExists(int id)
            {
                return _context.Categories.Any(e => e.Id == id);
            }
        }
}
