namespace WeddingRestaurant.Models
{
    public class CartItem
    {
        public int MonAnId { get; set; }
        public string TenMonAn { get; set; }
        public decimal GiaTien { get; set; }
        public int SoLuong { get; set; }

        public decimal ThanhTien => GiaTien * SoLuong;
    }

}
