
using WeddingRestaurant.Models;

namespace WeddingRestaurant.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        // Các Repository cụ thể cho từng Model trong dự án tiệc cưới
        IRepository<MonAn> MonAns { get; }      // Repository cho các Món Ăn
        IRepository<Sanh> Sanhs { get; }        // Repository cho các Sảnh
        IRepository<DatBan> DatBans { get; }    // Repository cho các Đặt Bàn

        // Phương thức để lưu tất cả các thay đổi vào cơ sở dữ liệu
        Task CompleteAsync();
    }
}
