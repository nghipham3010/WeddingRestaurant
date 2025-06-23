using System.ComponentModel.DataAnnotations;

namespace WeddingRestaurant.Models
{
    public class Sanh
    {
        [Key] // Đánh dấu Id là khóa chính
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên sảnh là bắt buộc.")]
        [StringLength(100, ErrorMessage = "Tên sảnh không được vượt quá 100 ký tự.")]
        [Display(Name = "Tên Sảnh")]
        public string TenSanh { get; set; } = string.Empty; // Khởi tạo để tránh null reference

        [Required(ErrorMessage = "Sức chứa tối đa là bắt buộc.")]
        [Range(10, 1000, ErrorMessage = "Sức chứa phải từ 10 đến 1000 khách.")]
        [Display(Name = "Sức Chứa Tối Đa")]
        public int SucChuaToiDa { get; set; } // Đảm bảo tên thuộc tính này khớp với seeding

        [StringLength(500, ErrorMessage = "Mô tả không được vượt quá 500 ký tự.")]
        [Display(Name = "Mô Tả")]
        public string? MoTa { get; set; } // Nullable

        [Display(Name = "Đường Dẫn Hình Ảnh")]
        public string? HinhAnhUrl { get; set; }

        // Thêm thuộc tính GiaThue nếu chưa có
        [Required(ErrorMessage = "Giá thuê là bắt buộc.")]
        [Range(0, double.MaxValue, ErrorMessage = "Giá thuê phải là số dương.")]
        [Display(Name = "Giá Thuê")]
        public decimal GiaThue { get; set; } // Sử dụng decimal cho tiền tệ
    }
}
