using WeddingRestaurant.Models;

namespace WeddingRestaurant.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        // Các trường để lưu trữ các instance của Repository
        private IRepository<MonAn>? _monAnRepository;
        private IRepository<Sanh>? _sanhRepository;
        private IRepository<DatBan>? _datBanRepository;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        // Các thuộc tính (properties) để truy cập các Repository cụ thể
        public IRepository<MonAn> MonAns
        {
            // Lazy initialization: chỉ tạo instance khi nó được truy cập lần đầu
            get { return _monAnRepository ??= new Repository<MonAn>(_context); }
        }

        public IRepository<Sanh> Sanhs
        {
            get { return _sanhRepository ??= new Repository<Sanh>(_context); }
        }

        public IRepository<DatBan> DatBans
        {
            get { return _datBanRepository ??= new Repository<DatBan>(_context); }
        }

        // Phương thức để lưu các thay đổi vào cơ sở dữ liệu
        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }

        // Phương thức Dispose để giải phóng tài nguyên (DbContext)
        public void Dispose()
        {
            _context.Dispose(); // Giải phóng DbContext
            GC.SuppressFinalize(this); // Ngăn finalizer chạy nếu đã Dispose
        }
    }
}
