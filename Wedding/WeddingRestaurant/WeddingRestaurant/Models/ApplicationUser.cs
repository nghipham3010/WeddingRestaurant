using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace WeddingRestaurant.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required(ErrorMessage = "Tên đầy đủ là bắt buộc.")]
        [StringLength(100, ErrorMessage = "Tên đầy đủ không được vượt quá 100 ký tự.")]
        [Display(Name = "Tên Đầy Đủ")]
        public string FullName { get; set; } = string.Empty;

        [StringLength(50)]
        public string? Role { get; set; }
    }
}
