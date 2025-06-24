using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WeddingRestaurant.Models;
using WeddingRestaurant.Repositories;

namespace WeddingRestaurant.Controllers
{
    [Authorize(Roles = "Customer")]
    public class CustomerBookingController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;

        // Constructor để inject IUnitOfWork và UserManager
        public CustomerBookingController(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        // GET: CustomerBooking/Create
        // Hiển thị form cho khách hàng để tạo một yêu cầu đặt bàn mới.
        public async Task<IActionResult> Create() // Đổi DatBanMoi -> Create
        {
            var user = await _userManager.GetUserAsync(User); // Lấy thông tin người dùng hiện tại
            var model = new DatBan // Khởi tạo một đối tượng DatBan mới
            {
                TenKhachHang = user?.FullName ?? user?.UserName ?? "", // Điền sẵn tên khách hàng từ user profile
                SoDienThoai = user?.PhoneNumber ?? "", // Điền sẵn số điện thoại từ user profile
                ThoiGianNhanBan = DateTime.Now.AddDays(1).Date.AddHours(19).AddMinutes(0), // Đặt thời gian mặc định (ví dụ: ngày mai, 7 tối)
                SoLuongKhach = 2, // Số lượng khách mặc định
                ThoiGianDuKienHoanTatPhut = 60 // Thời gian hoàn tất dự kiến mặc định
            };
            return View(model);
        }

        // POST: CustomerBooking/Create
        // Xử lý việc gửi yêu cầu đặt bàn từ form.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] DatBan datBan)
        {
            // Bỏ qua validate ApplicationUserId vì sẽ gán ở server
            ModelState.Remove(nameof(DatBan.ApplicationUserId));

            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (userId == null)
                {
                    return Unauthorized("You need to be logged in to make a booking.");
                }

                // Gán ApplicationUserId ở đây
                datBan.ApplicationUserId = userId;
                datBan.ThoiGianTao = DateTime.Now;
                datBan.TrangThai = TrangThaiDatBan.ChoXacNhan;
                datBan.TienCoc = 0;
                datBan.PhuongThucDatCoc = null;

                await _unitOfWork.DatBans.AddAsync(datBan);
                await _unitOfWork.CompleteAsync();
                TempData["SuccessMessage"] = "Your booking request has been sent. We will contact you soon for confirmation!";
                return RedirectToAction("MyBookings");
            }
            return View(datBan);
        }

        // GET: CustomerBooking/MyBookings
        // Hiển thị danh sách các đặt bàn của người dùng đang đăng nhập.
        public async Task<IActionResult> MyBookings() // Giữ nguyên tên Action
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Lấy ID của người dùng đang đăng nhập
            if (userId == null)
            {
                return Unauthorized("You need to be logged in to view your bookings.");
            }

            // Tìm tất cả đặt bàn thuộc về người dùng hiện tại, sắp xếp theo thời gian tạo mới nhất
            var myBookings = (await _unitOfWork.DatBans
                                                .FindAsync(db => db.ApplicationUserId == userId))
                                                .OrderByDescending(db => db.ThoiGianTao)
                                                .ToList();

            return View(myBookings);
        }

        // GET: CustomerBooking/Details/5
        // Hiển thị chi tiết của một đặt bàn cụ thể thuộc về người dùng đang đăng nhập.
        public async Task<IActionResult> Details(int? id) // Giữ nguyên tên Action
        {
            if (id == null)
            {
                return NotFound(); // Trả về 404 nếu ID không được cung cấp
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Lấy ID của người dùng đang đăng nhập
            // Tìm đặt bàn theo ID và đảm bảo nó thuộc về người dùng hiện tại
            var datBan = (await _unitOfWork.DatBans
                                            .FindAsync(db => db.Id == id && db.ApplicationUserId == userId))
                                            .FirstOrDefault();

            if (datBan == null)
            {
                return NotFound(); // Trả về 404 nếu không tìm thấy đặt bàn hoặc không thuộc về người dùng
            }

            return View(datBan);
        }

        // GET: CustomerBooking/CancelBooking/5
        // Hiển thị form xác nhận hủy đặt bàn.
        public async Task<IActionResult> CancelBooking(int? id) // Giữ nguyên tên Action
        {
            if (id == null)
            {
                return NotFound();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var datBan = await _unitOfWork.DatBans.GetByIdAsync(id.Value);

            // Kiểm tra xem đặt bàn có tồn tại, có thuộc về người dùng và có thể hủy được không
            if (datBan == null || datBan.ApplicationUserId != userId ||
                datBan.TrangThai == TrangThaiDatBan.DaHuy || // Không thể hủy nếu đã hủy
                datBan.TrangThai == TrangThaiDatBan.HoanThanh) // Hoặc đã hoàn thành
            {
                TempData["ErrorMessage"] = "Cannot cancel this booking as its status is already 'Cancelled' or 'Completed'.";
                return RedirectToAction("MyBookings");
            }

            // Thêm logic: Không thể hủy nếu thời gian nhận bàn quá gần (ví dụ: trong vòng 24 giờ tới)
            if (datBan.ThoiGianNhanBan.Subtract(DateTime.Now).TotalHours < 24 && datBan.TrangThai != TrangThaiDatBan.ChoXacNhan)
            {
                TempData["ErrorMessage"] = "You can only cancel a booking at least 24 hours before the booking time, or if it's still 'Pending Confirmation'.";
                return RedirectToAction("MyBookings");
            }

            return View(datBan);
        }

        // POST: CustomerBooking/ConfirmCancellation/5
        // Xử lý việc hủy đặt bàn sau khi người dùng xác nhận.
        [HttpPost, ActionName("CancelBooking")] // Map ActionName này với GET "CancelBooking" để cùng URL
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmCancellation(int id, string reason) // Đổi ConfirmCancelBooking -> ConfirmCancellation
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var datBan = await _unitOfWork.DatBans.GetByIdAsync(id);

            // Kiểm tra lại các điều kiện hủy tương tự như GET action
            if (datBan == null || datBan.ApplicationUserId != userId ||
                datBan.TrangThai == TrangThaiDatBan.DaHuy ||
                datBan.TrangThai == TrangThaiDatBan.HoanThanh)
            {
                TempData["ErrorMessage"] = "Cannot cancel this booking as its status is already 'Cancelled' or 'Completed'.";
                return RedirectToAction("MyBookings");
            }

            if (datBan.ThoiGianNhanBan.Subtract(DateTime.Now).TotalHours < 24 && datBan.TrangThai != TrangThaiDatBan.ChoXacNhan)
            {
                TempData["ErrorMessage"] = "You can only cancel a booking at least 24 hours before the booking time, or if it's still 'Pending Confirmation'.";
                return RedirectToAction("MyBookings");
            }

            // Cập nhật trạng thái và lý do hủy
            datBan.TrangThai = TrangThaiDatBan.DaHuy;
            datBan.LyDoHuy = reason;

            _unitOfWork.DatBans.Update(datBan); // Đánh dấu đối tượng để cập nhật
            await _unitOfWork.CompleteAsync(); // Lưu thay đổi vào cơ sở dữ liệu
            TempData["SuccessMessage"] = "Your booking has been cancelled successfully.";
            return RedirectToAction("MyBookings");
        }
    }
}
