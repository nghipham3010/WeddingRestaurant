namespace WeddingRestaurant.Models
{
    public class OrderDetail
    {
        public int Id { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; }

        public int MonAnId { get; set; }
        public MonAn MonAn { get; set; }

        public int SoLuong { get; set; }
        public decimal GiaTien { get; set; }
        public decimal ThanhTien => SoLuong * GiaTien;
    }

}
