using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataProject.Migrations
{
    /// <inheritdoc />
    public partial class migration_13 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BuyAutoModel");

            migrationBuilder.AddColumn<bool>(
                name: "IsBought",
                table: "Autos",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Autos",
                keyColumn: "Id",
                keyValue: 1,
                column: "IsBought",
                value: false);

            migrationBuilder.UpdateData(
                table: "Autos",
                keyColumn: "Id",
                keyValue: 2,
                column: "IsBought",
                value: false);

            migrationBuilder.UpdateData(
                table: "Autos",
                keyColumn: "Id",
                keyValue: 3,
                column: "IsBought",
                value: false);

            migrationBuilder.UpdateData(
                table: "Autos",
                keyColumn: "Id",
                keyValue: 4,
                column: "IsBought",
                value: false);

            migrationBuilder.UpdateData(
                table: "Autos",
                keyColumn: "Id",
                keyValue: 5,
                column: "IsBought",
                value: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsBought",
                table: "Autos");

            migrationBuilder.CreateTable(
                name: "BuyAutoModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ColorColorName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ColorId = table.Column<int>(type: "int", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Mark = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Year = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuyAutoModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BuyAutoModel_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_BuyAutoModel_UserId",
                table: "BuyAutoModel",
                column: "UserId");
        }
    }
}
