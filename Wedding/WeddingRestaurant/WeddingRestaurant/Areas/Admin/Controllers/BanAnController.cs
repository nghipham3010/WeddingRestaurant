using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WeddingRestaurant.Models;
using WeddingRestaurant.Repositories;


namespace WeddingRestaurant.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class BanAnController : Controller
    {
        private readonly ApplicationDbContext _context; // Hoặc dùng IBanAnRepository
        private readonly IBanAnRepository _banAnRepository; // Thêm dòng này

        public BanAnController(ApplicationDbContext context, IBanAnRepository banAnRepository) // Chỉnh sửa constructor
        {
            _context = context;
            _banAnRepository = banAnRepository;
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
            // Bước 1: Kiểm tra Model State.
            // Nếu dữ liệu từ form không hợp lệ theo các Validation Attributes (như [Required]),
            // thì ModelState.IsValid sẽ là false.
            if (!ModelState.IsValid)
            {
                // Debugging: Có thể ghi log các lỗi ở đây để xem chúng là gì
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                foreach (var error in errors)
                {
                    Console.WriteLine($"Validation Error: {error}"); // Ghi log lỗi vào Console (trong chế độ debug)
                }
                // Trả về View với dữ liệu đã nhập để hiển thị các thông báo lỗi validation.
                return View(banAn);
            }

            // Nếu ModelState.IsValid là true, tiếp tục xử lý
            try
            {
                // Bước 2: Thêm đối tượng BanAn vào cơ sở dữ liệu thông qua Repository.
                // Chỉ sử dụng một trong hai cách dưới đây, ưu tiên dùng Repository.
                await _banAnRepository.AddAsync(banAn); // Sử dụng Repository

                // Bước 3: Lưu các thay đổi vào cơ sở dữ liệu.
                // Vì _banAnRepository được khởi tạo với _context, việc SaveChangesAsync() trên _context
                // sẽ lưu các thay đổi mà _banAnRepository đã thêm.
                await _context.SaveChangesAsync();

                // Bước 4: Thông báo thành công và chuyển hướng.
                TempData["SuccessMessage"] = "Bàn đã được thêm thành công!";
                return RedirectToAction("Index");
            }
            catch (DbUpdateException ex)
            {
                // Xử lý lỗi khi lưu vào DB, ví dụ: trùng MaBan nếu có ràng buộc UNIQUE
                // Log lỗi chi tiết: ex.Message, ex.InnerException
                Console.WriteLine($"Database Error: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                }
                ModelState.AddModelError("", "Đã xảy ra lỗi khi lưu dữ liệu. Có thể mã bàn đã tồn tại hoặc dữ liệu không hợp lệ.");
                return View(banAn); // Trả về view với lỗi
            }
            catch (Exception ex)
            {
                // Xử lý các lỗi chung khác
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                ModelState.AddModelError("", "Đã xảy ra lỗi không mong muốn. Vui lòng thử lại.");
                return View(banAn);
            }
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
