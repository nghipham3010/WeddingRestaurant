using System.Linq.Expressions;

namespace WeddingRestaurant.Repositories
{
        public interface IRepository<TEntity> where TEntity : class
        {
            // Lấy tất cả các Entity
            Task<IEnumerable<TEntity>> GetAllAsync();

            // Lấy một Entity theo ID
            Task<TEntity?> GetByIdAsync(int id);

            // Tìm các Entity dựa trên một biểu thức điều kiện
            Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);

            // Thêm một Entity mới
            Task AddAsync(TEntity entity);

            // Cập nhật một Entity hiện có
            void Update(TEntity entity);

            // Xóa một Entity
            void Remove(TEntity entity);

            // Thêm nhiều Entity cùng lúc
            Task AddRangeAsync(IEnumerable<TEntity> entities);

            // Xóa nhiều Entity cùng lúc
            void RemoveRange(IEnumerable<TEntity> entities);
        }
}
