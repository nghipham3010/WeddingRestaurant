using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WeddingRestaurant.Models
{
    public class DatBan
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        public ApplicationUser? ApplicationUser { get; set; }

        [Required(ErrorMessage = "Tên khách hàng là bắt buộc.")]
        [StringLength(100, ErrorMessage = "Tên khách hàng không được vượt quá 100 ký tự.")]
        [Display(Name = "Tên Khách Hàng")]
        public string TenKhachHang { get; set; } = string.Empty;

        [Required(ErrorMessage = "Số điện thoại là bắt buộc.")]
        [Phone(ErrorMessage = "Số điện thoại không hợp lệ.")]
        [StringLength(20, ErrorMessage = "Số điện thoại không được vượt quá 20 ký tự.")]
        [Display(Name = "Số Điện Thoại")]
        public string SoDienThoai { get; set; } = string.Empty;

        [Required(ErrorMessage = "Thời gian nhận bàn là bắt buộc.")]
        [DataType(DataType.DateTime)]
        [Display(Name = "Thời Gian Nhận Bàn")]
        public DateTime ThoiGianNhanBan { get; set; }

        [Required(ErrorMessage = "Số lượng khách là bắt buộc.")]
        [Range(1, 1000, ErrorMessage = "Số lượng khách phải từ 1 đến 1000.")]
        [Display(Name = "Số Lượng Khách")]
        public int SoLuongKhach { get; set; }

        [Display(Name = "Thời Gian Dự Kiến Hoàn Tất (phút)")]
        [Range(10, 360, ErrorMessage = "Thời gian dự kiến phải từ 10 đến 360 phút.")]
        public int ThoiGianDuKienHoanTatPhut { get; set; } = 60;

        [StringLength(500, ErrorMessage = "Ghi chú không được vượt quá 500 ký tự.")]
        [Display(Name = "Ghi Chú")]
        public string? GhiChu { get; set; }

        [Display(Name = "Tiền Cọc")]
        [Range(0, 10000000000, ErrorMessage = "Số tiền cọc không hợp lệ.")]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal TienCoc { get; set; } = 0;

        [Display(Name = "Phương Thức Đặt Cọc")]
        [StringLength(50, ErrorMessage = "Phương thức đặt cọc không được vượt quá 50 ký tự.")]
        public string? PhuongThucDatCoc { get; set; }

        [Display(Name = "Trạng Thái")]
        public string TrangThai { get; set; } = TrangThaiDatBan.ChoXacNhan; // Sử dụng class TrangThaiDatBan

        [Display(Name = "Lý Do Hủy")]
        [StringLength(500, ErrorMessage = "Lý do hủy không được vượt quá 500 ký tự.")]
        public string? LyDoHuy { get; set; }

        [Display(Name = "Thời Gian Tạo")]
        public DateTime ThoiGianTao { get; set; } = DateTime.Now;

        [Display(Name = "Bàn Đã Xếp")]
        [StringLength(200, ErrorMessage = "Thông tin bàn đã xếp không được vượt quá 200 ký tự.")]
        public string? BanDaXep { get; set; }
    }

    public static class TrangThaiDatBan // Giữ nguyên tên class TrangThaiDatBan
    {
        public const string ChoXacNhan = "Chờ Xác Nhận";
        public const string DaXacNhan = "Đã Xác Nhận";
        public const string DaXepBan = "Đã Xếp Bàn";
        public const string HoanThanh = "Hoàn Thành";
        public const string DaHuy = "Đã Hủy";
    }
}

