using System.ComponentModel.DataAnnotations;

namespace WeddingRestaurant.Models
{
    public class BanAn
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Mã bàn là bắt buộc.")]
        [Display(Name = "Mã Bàn")]
        public string MaBan { get; set; }

        [Required(ErrorMessage = "Loại bàn là bắt buộc.")]
        [Display(Name = "Loại Bàn")]
        public string LoaiBan { get; set; }

        [Required(ErrorMessage = "Vị trí là bắt buộc.")]
        [Display(Name = "Vị Trí")]
        public string ViTri { get; set; }

        [Required(ErrorMessage = "Trạng thái là bắt buộc.")]
        [Display(Name = "Trạng Thái")]
        public string TrangThai { get; set; }
    }

}
