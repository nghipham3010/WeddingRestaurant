using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WeddingRestaurant.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Khai báo các DbSet cho các Model của bạn
        public DbSet<MonAn> MonAns { get; set; }
        public DbSet<Sanh> Sanhs { get; set; }
        public DbSet<DatBan> DatBans { get; set; }
        public DbSet<BanAn> BanAns { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed dữ liệu cho MonAn
          

        }
    }
}
