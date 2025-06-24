using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WeddingRestaurant.Models;

namespace WeddingRestaurant.Controllers
{
    [AllowAnonymous]
    public class MenuController : Controller
    {
        private readonly ApplicationDbContext _context;
        public MenuController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Menu
        // Hiển thị tất cả các món ăn đang hoạt động.
        // Có thể tùy chọn nhận một chuỗi tìm kiếm để lọc.
        [AllowAnonymous] // Đảm bảo hành động này có thể truy cập mà không cần đăng nhập
        public async Task<IActionResult> Index(string searchString)
        {
            IQueryable<MonAn> monAns = _context.MonAns.Where(m => m.IsActive); // Chỉ hiển thị các món ăn đang hoạt động

            if (!string.IsNullOrEmpty(searchString))
            {
                // Tìm kiếm đơn giản không phân biệt chữ hoa chữ thường theo tên món ăn hoặc mô tả
                monAns = monAns.Where(m => m.TenMonAn.Contains(searchString) ||
                                           (m.MoTa != null && m.MoTa.Contains(searchString)));
            }

            // Sắp xếp theo tên để hiển thị nhất quán
            var activeMonAns = await monAns.OrderBy(m => m.TenMonAn).ToListAsync();

            // Truyền chuỗi tìm kiếm trở lại view để điền vào ô tìm kiếm
            ViewData["CurrentFilter"] = searchString;

            return View(activeMonAns);
        }

        // GET: Menu/Details/5
        // Hiển thị chi tiết cho một món ăn cụ thể.
        [AllowAnonymous] // Đảm bảo hành động này có thể truy cập mà không cần đăng nhập
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound(); // Trả về 404 nếu không cung cấp ID
            }

            var monAn = await _context.MonAns
                .FirstOrDefaultAsync(m => m.Id == id && m.IsActive); // Chỉ hiển thị nếu đang hoạt động

            if (monAn == null)
            {
                return NotFound(); // Trả về 404 nếu không tìm thấy món ăn hoặc không hoạt động
            }

            return View(monAn);
        }

        [HttpGet]
        public async Task<IActionResult> Index(string? searchString, int pageNumber = 1)
        {
            int pageSize = 6;

            var query = _context.MonAns.AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(m => m.TenMonAn.Contains(searchString));
                ViewData["CurrentFilter"] = searchString;
            }

            var paginatedList = await PaginatedList<MonAn>.CreateAsync(query.OrderBy(m => m.TenMonAn), pageNumber, pageSize);
            return View(paginatedList);
        }




    }
}
