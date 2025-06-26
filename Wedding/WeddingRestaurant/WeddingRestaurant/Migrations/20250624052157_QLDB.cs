using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeddingRestaurant.Migrations
{
    /// <inheritdoc />
    public partial class QLDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BanAns",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaBan = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    LoaiBan = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ViTri = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TrangThai = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BanAns", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BanAns");
        }
    }
}
