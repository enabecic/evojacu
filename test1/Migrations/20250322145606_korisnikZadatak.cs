using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace evojacu.Migrations
{
    /// <inheritdoc />
    public partial class korisnikZadatak : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Zadaci_Korisnici_KorisnikId",
                table: "Zadaci");

            migrationBuilder.RenameColumn(
                name: "KorisnikId",
                table: "Zadaci",
                newName: "korisnikId");

            migrationBuilder.RenameIndex(
                name: "IX_Zadaci_KorisnikId",
                table: "Zadaci",
                newName: "IX_Zadaci_korisnikId");

            migrationBuilder.AddForeignKey(
                name: "FK_Zadaci_Korisnici_korisnikId",
                table: "Zadaci",
                column: "korisnikId",
                principalTable: "Korisnici",
                principalColumn: "KorisnikID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Zadaci_Korisnici_korisnikId",
                table: "Zadaci");

            migrationBuilder.RenameColumn(
                name: "korisnikId",
                table: "Zadaci",
                newName: "KorisnikId");

            migrationBuilder.RenameIndex(
                name: "IX_Zadaci_korisnikId",
                table: "Zadaci",
                newName: "IX_Zadaci_KorisnikId");

            migrationBuilder.AddForeignKey(
                name: "FK_Zadaci_Korisnici_KorisnikId",
                table: "Zadaci",
                column: "KorisnikId",
                principalTable: "Korisnici",
                principalColumn: "KorisnikID");
        }
    }
}
