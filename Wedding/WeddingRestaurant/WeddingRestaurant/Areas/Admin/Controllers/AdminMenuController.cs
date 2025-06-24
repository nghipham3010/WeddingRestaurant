using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WeddingRestaurant.Models;

namespace WeddingRestaurant.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminMenuController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminMenuController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/AdminMenu
        public async Task<IActionResult> Index()
        {
            var allMonAn = await _context.MonAns.OrderBy(m => m.TenMonAn).ToListAsync();
            return View(allMonAn);
        }

        // GET: Admin/AdminMenu/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MonAn monAn)
        {
            if (ModelState.IsValid)
            {
                _context.Add(monAn);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(monAn);
        }

        // GET: Admin/AdminMenu/Edit
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var monAn = await _context.MonAns.FindAsync(id);
            if (monAn == null) return NotFound();

            return View(monAn);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, MonAn monAn)
        {
            if (id != monAn.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(monAn);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.MonAns.Any(e => e.Id == monAn.Id))
                        return NotFound();
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(monAn);
        }

        // GET: Admin/AdminMenu/Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var monAn = await _context.MonAns.FirstOrDefaultAsync(m => m.Id == id);
            if (monAn == null) return NotFound();

            return View(monAn);
        }

        // POST: Admin/AdminMenu/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var monAn = await _context.MonAns.FindAsync(id);
            if (monAn != null)
            {
                _context.MonAns.Remove(monAn);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
