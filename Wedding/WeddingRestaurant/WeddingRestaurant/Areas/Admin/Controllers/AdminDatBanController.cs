using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WeddingRestaurant.Models;

namespace WeddingRestaurant.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminDatBanController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminDatBanController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Danh sách các đặt bàn chờ xác nhận hoặc đã xác nhận
        public async Task<IActionResult> Index()
        {
            var datBans = await _context.DatBans
                .Where(d => d.TrangThai != TrangThaiDatBan.DaHuy && d.TrangThai != TrangThaiDatBan.HoanThanh)
                .OrderByDescending(d => d.ThoiGianTao)
                .ToListAsync();
            return View(datBans);
        }

        // GET: AdminDatBan/AssignTable/5
        public async Task<IActionResult> AssignTable(int? id)
        {
            if (id == null) return NotFound();

            var datBan = await _context.DatBans.FindAsync(id);
            if (datBan == null) return NotFound();

            // Lấy danh sách bàn còn trống
            var availableTables = await _context.BanAns
                .Where(b => b.TrangThai == "Còn trống")
                .ToListAsync();

            ViewBag.AvailableTables = availableTables;
            return View(datBan);
        }

        // POST: AdminDatBan/AssignTable/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AssignTable(int id, int selectedTableId)
        {
            var datBan = await _context.DatBans.FindAsync(id);
            var banAn = await _context.BanAns.FindAsync(selectedTableId);

            if (datBan == null || banAn == null)
            {
                TempData["ErrorMessage"] = "Không tìm thấy đặt bàn hoặc bàn ăn.";
                return RedirectToAction(nameof(Index));
            }

            // Cập nhật thông tin đặt bàn
            datBan.BanDaXep = banAn.MaBan;
            datBan.TrangThai = TrangThaiDatBan.DaXepBan;

            // Cập nhật trạng thái bàn
            banAn.TrangThai = "Đã đặt";

            _context.Update(datBan);
            _context.Update(banAn);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = $"Đã xếp bàn {banAn.MaBan} cho khách!";
            return RedirectToAction(nameof(Index));
        }
    }
}