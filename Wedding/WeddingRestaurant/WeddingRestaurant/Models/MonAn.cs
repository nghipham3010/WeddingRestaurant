using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeddingRestaurant.Models
{
    public class MonAn
    {
        [Key] // Đánh dấu Id là khóa chính
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên món ăn là bắt buộc.")]
        [StringLength(100, ErrorMessage = "Tên món ăn không được vượt quá 100 ký tự.")]
        [Display(Name = "Tên Món Ăn")]
        public string TenMonAn { get; set; } = string.Empty; // Khởi tạo để tránh null reference

        [Required(ErrorMessage = "Giá tiền là bắt buộc.")]
        [Range(0, 1000000000, ErrorMessage = "Giá tiền phải là số dương và hợp lệ.")] // Đặt giới hạn hợp lý
        [Display(Name = "Giá Tiền")]
        public decimal GiaTien { get; set; }

        [StringLength(500, ErrorMessage = "Mô tả không được vượt quá 500 ký tự.")]
        [Display(Name = "Mô Tả")]
        public string? MoTa { get; set; } // Nullable

        [Display(Name = "Đường Dẫn Hình Ảnh")]
        [NotMapped]
        public IFormFile? UploadedImage { get; set; }

        public string? HinhAnhUrl { get; set; } // Đây vẫn dùng để lưu tên file


        [Display(Name = "Trạng Thái")]
        public bool IsActive { get; set; } = true; // Mặc định là đang kinh doanh
    }
}
