using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WeddingRestaurant.Models;


namespace WeddingRestaurant.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class BanAnController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BanAnController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /BanAn/Index
        // Xem danh sách bàn với bộ lọc loại bàn và trạng thái
        public async Task<IActionResult> Index(string? loaiBan, string? trangThai)
        {
            var query = _context.BanAns.AsQueryable();

            if (!string.IsNullOrEmpty(loaiBan))
                query = query.Where(b => b.LoaiBan.Contains(loaiBan));

            if (!string.IsNullOrEmpty(trangThai))
                query = query.Where(b => b.TrangThai == trangThai);

            var danhSachBan = await query.OrderBy(b => b.LoaiBan).ToListAsync();

            ViewData["LoaiBan"] = loaiBan;
            ViewData["TrangThai"] = trangThai;

            return View(danhSachBan);
        }

        // GET: /BanAn/Create
        public IActionResult Create()
        {
            var model = new BanAn
            {
                TrangThai = "Còn trống" // Đặt mặc định là "Còn trống"
            };

            return View(model);
        }


        // POST: /BanAn/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BanAn banAn)
        {
            if (ModelState.IsValid)
            {
                _context.BanAns.Add(banAn); // hoặc _unitOfWork.BanAns.AddAsync(banAn)
                await _context.SaveChangesAsync(); // hoặc _unitOfWork.CompleteAsync()

                TempData["SuccessMessage"] = "Bàn đã được thêm thành công!";
                return RedirectToAction("Index");
            }

            // Nếu không hợp lệ, trả lại view với dữ liệu đã nhập
            return View(banAn);
        }


        // GET: /BanAn/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var ban = await _context.BanAns.FindAsync(id);
            if (ban == null)
                return NotFound();

            return View(ban);
        }

        // POST: /BanAn/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, BanAn banAn)
        {
            if (id != banAn.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(banAn);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Cập nhật bàn thành công!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.BanAns.Any(b => b.Id == id))
                        return NotFound();

                    throw;
                }

                return RedirectToAction(nameof(Index));
            }

            return View(banAn);
        }

        // GET: /BanAn/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var ban = await _context.BanAns.FirstOrDefaultAsync(b => b.Id == id);
            if (ban == null)
                return NotFound();

            return View(ban);
        }

        // POST: /BanAn/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ban = await _context.BanAns.FindAsync(id);
            if (ban != null)
            {
                _context.BanAns.Remove(ban);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Xóa bàn thành công!";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
