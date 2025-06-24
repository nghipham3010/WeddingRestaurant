using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WeddingRestaurant.Migrations
{
    /// <inheritdoc />
    public partial class ThanhToan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "MonAns",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "MonAns",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "MonAns",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "MonAns",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "MonAns",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "MonAns",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Sanhs",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Sanhs",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Sanhs",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.AlterColumn<string>(
                name: "ViTri",
                table: "BanAns",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "MaBan",
                table: "BanAns",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "LoaiBan",
                table: "BanAns",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ViTri",
                table: "BanAns",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "MaBan",
                table: "BanAns",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "LoaiBan",
                table: "BanAns",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "MonAns",
                columns: new[] { "Id", "GiaTien", "HinhAnhUrl", "IsActive", "MoTa", "TenMonAn" },
                values: new object[,]
                {
                    { 1, 150000m, "/images/seed/monan/goi-ngo-sen.jpg", true, "Món gỏi khai vị thanh mát với ngó sen giòn, tôm tươi và thịt ba chỉ luộc.", "Gỏi Ngó Sen Tôm Thịt" },
                    { 2, 180000m, "/images/seed/monan/sup-hai-san.jpg", true, "Súp nóng hổi với các loại hải sản tươi ngon như tôm, mực, bào ngư, nấm hương.", "Súp Hải Sản Bát Bảo" },
                    { 3, 250000m, "/images/seed/monan/tom-hap-nuoc-dua.jpg", true, "Tôm sú tươi sống hấp với nước dừa xiêm ngọt thanh, giữ trọn vị ngon tự nhiên.", "Tôm Hấp Nước Dừa Tươi" },
                    { 4, 200000m, "/images/seed/monan/ca-chien-xu.jpg", true, "Cá diêu hồng chiên giòn rụm, vàng ươm, chấm nước mắm gừng.", "Cá Diêu Hồng Chiên Xù" },
                    { 5, 350000m, "/images/seed/monan/lau-thai.jpg", true, "Nồi lẩu Thái đậm đà hương vị, chua cay hấp dẫn với hải sản và rau tươi.", "Lẩu Thái Chua Cay" },
                    { 6, 120000m, "/images/seed/monan/com-chien-hai-san.jpg", true, "Cơm chiên tơi xốp cùng hải sản tươi ngon, trứng, và rau củ.", "Cơm Chiên Hải Sản" }
                });

            migrationBuilder.InsertData(
                table: "Sanhs",
                columns: new[] { "Id", "GiaThue", "HinhAnhUrl", "MoTa", "SucChuaToiDa", "TenSanh" },
                values: new object[,]
                {
                    { 1, 50000000m, "/images/seed/sanh/grand-ballroom.jpg", "Sảnh chính lớn nhất, sang trọng, phù hợp cho các tiệc cưới quy mô lớn.", 500, "Sảnh Grand Ballroom" },
                    { 2, 20000000m, "/images/seed/sanh/sanh-ruby.jpg", "Sảnh ấm cúng với thiết kế hiện đại, phù hợp tiệc vừa và nhỏ.", 200, "Sảnh Ruby" },
                    { 3, 10000000m, "/images/seed/sanh/sanh-emerald.jpg", "Sảnh nhỏ, riêng tư, thích hợp cho tiệc thân mật hoặc lễ đính hôn.", 100, "Sảnh Emerald" }
                });
        }
    }
}
