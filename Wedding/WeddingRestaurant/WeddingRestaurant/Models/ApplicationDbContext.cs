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
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed dữ liệu cho MonAn
            modelBuilder.Entity<MonAn>().HasData(
                new MonAn { Id = 1, TenMonAn = "Gỏi Ngó Sen Tôm Thịt", MoTa = "Món gỏi khai vị thanh mát với ngó sen giòn, tôm tươi và thịt ba chỉ luộc.", GiaTien = 150000, HinhAnhUrl = "/images/seed/monan/goi-ngo-sen.jpg", IsActive = true },
                new MonAn { Id = 2, TenMonAn = "Súp Hải Sản Bát Bảo", MoTa = "Súp nóng hổi với các loại hải sản tươi ngon như tôm, mực, bào ngư, nấm hương.", GiaTien = 180000, HinhAnhUrl = "/images/seed/monan/sup-hai-san.jpg", IsActive = true },
                new MonAn { Id = 3, TenMonAn = "Tôm Hấp Nước Dừa Tươi", MoTa = "Tôm sú tươi sống hấp với nước dừa xiêm ngọt thanh, giữ trọn vị ngon tự nhiên.", GiaTien = 250000, HinhAnhUrl = "/images/seed/monan/tom-hap-nuoc-dua.jpg", IsActive = true },
                new MonAn { Id = 4, TenMonAn = "Cá Diêu Hồng Chiên Xù", MoTa = "Cá diêu hồng chiên giòn rụm, vàng ươm, chấm nước mắm gừng.", GiaTien = 200000, HinhAnhUrl = "/images/seed/monan/ca-chien-xu.jpg", IsActive = true },
                new MonAn { Id = 5, TenMonAn = "Lẩu Thái Chua Cay", MoTa = "Nồi lẩu Thái đậm đà hương vị, chua cay hấp dẫn với hải sản và rau tươi.", GiaTien = 350000, HinhAnhUrl = "/images/seed/monan/lau-thai.jpg", IsActive = true },
                new MonAn { Id = 6, TenMonAn = "Cơm Chiên Hải Sản", MoTa = "Cơm chiên tơi xốp cùng hải sản tươi ngon, trứng, và rau củ.", GiaTien = 120000, HinhAnhUrl = "/images/seed/monan/com-chien-hai-san.jpg", IsActive = true }
            );

            // Seed dữ liệu cho Sanh
            modelBuilder.Entity<Sanh>().HasData(
                new Sanh { Id = 1, TenSanh = "Sảnh Grand Ballroom", SucChuaToiDa = 500, MoTa = "Sảnh chính lớn nhất, sang trọng, phù hợp cho các tiệc cưới quy mô lớn.", GiaThue = 50000000, HinhAnhUrl = "/images/seed/sanh/grand-ballroom.jpg" },
                new Sanh { Id = 2, TenSanh = "Sảnh Ruby", SucChuaToiDa = 200, MoTa = "Sảnh ấm cúng với thiết kế hiện đại, phù hợp tiệc vừa và nhỏ.", GiaThue = 20000000, HinhAnhUrl = "/images/seed/sanh/sanh-ruby.jpg" },
                new Sanh { Id = 3, TenSanh = "Sảnh Emerald", SucChuaToiDa = 100, MoTa = "Sảnh nhỏ, riêng tư, thích hợp cho tiệc thân mật hoặc lễ đính hôn.", GiaThue = 10000000, HinhAnhUrl = "/images/seed/sanh/sanh-emerald.jpg" }
            );

        }
    }
}
