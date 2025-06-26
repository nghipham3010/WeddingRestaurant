namespace WeddingRestaurant.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime NgayDat { get; set; } = DateTime.Now;
        public decimal TongTien { get; set; }

        public ICollection<OrderDetail> ChiTietDonHangs { get; set; }
    }

}
