using WeddingRestaurant.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace WeddingRestaurant.Repositories
{
    public class BanAnRepository : Repository<BanAn>, IBanAnRepository
    {
        private readonly ApplicationDbContext _context;

        public BanAnRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<BanAn>> GetByStatusAsync(string status)
        {
            return await _context.BanAns.Where(b => b.TrangThai == status).ToListAsync();
        }

        public async Task<IEnumerable<BanAn>> GetByTypeAsync(string loaiBan)
        {
            return await _context.BanAns.Where(b => b.LoaiBan == loaiBan).ToListAsync();
        }
    }
}