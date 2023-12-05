using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataProject.Migrations
{
    /// <inheritdoc />
    public partial class newField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "About",
                table: "Autos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Autos",
                keyColumn: "Id",
                keyValue: 1,
                column: "About",
                value: "Автомобіль в хорошому стані, якість перевірена часом");

            migrationBuilder.UpdateData(
                table: "Autos",
                keyColumn: "Id",
                keyValue: 2,
                column: "About",
                value: "Швидкісний, японський автомобіль");

            migrationBuilder.UpdateData(
                table: "Autos",
                keyColumn: "Id",
                keyValue: 3,
                column: "About",
                value: "Велика тачка");

            migrationBuilder.UpdateData(
                table: "Autos",
                keyColumn: "Id",
                keyValue: 4,
                column: "About",
                value: "Український автопром :)");

            migrationBuilder.UpdateData(
                table: "Autos",
                keyColumn: "Id",
                keyValue: 5,
                column: "About",
                value: "Ще одна велика тачка");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "About",
                table: "Autos");
        }
    }
}
