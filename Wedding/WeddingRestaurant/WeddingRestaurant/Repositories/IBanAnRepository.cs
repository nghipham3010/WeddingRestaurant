using WeddingRestaurant.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace WeddingRestaurant.Repositories
{
    public interface IBanAnRepository : IRepository<BanAn>
    {
        Task<IEnumerable<BanAn>> GetByStatusAsync(string status);
        Task<IEnumerable<BanAn>> GetByTypeAsync(string loaiBan);
    }
}