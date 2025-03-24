using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace evojacu.Migrations
{
    /// <inheritdoc />
    public partial class zadatak : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "KorisnikId",
                table: "Zadaci",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Zadaci_KorisnikId",
                table: "Zadaci",
                column: "KorisnikId");

            migrationBuilder.AddForeignKey(
                name: "FK_Zadaci_Korisnici_KorisnikId",
                table: "Zadaci",
                column: "KorisnikId",
                principalTable: "Korisnici",
                principalColumn: "KorisnikID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Zadaci_Korisnici_KorisnikId",
                table: "Zadaci");

            migrationBuilder.DropIndex(
                name: "IX_Zadaci_KorisnikId",
                table: "Zadaci");

            migrationBuilder.DropColumn(
                name: "KorisnikId",
                table: "Zadaci");
        }
    }
}
