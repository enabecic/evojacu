using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace evojacu.Migrations
{
    /// <inheritdoc />
    public partial class korisnikTadatak : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Zadaci_Korisnici_korisnikId",
                table: "Zadaci");

            migrationBuilder.RenameColumn(
                name: "korisnikId",
                table: "Zadaci",
                newName: "KorisnikID");

            migrationBuilder.RenameIndex(
                name: "IX_Zadaci_korisnikId",
                table: "Zadaci",
                newName: "IX_Zadaci_KorisnikID");

            migrationBuilder.AddForeignKey(
                name: "FK_Zadaci_Korisnici_KorisnikID",
                table: "Zadaci",
                column: "KorisnikID",
                principalTable: "Korisnici",
                principalColumn: "KorisnikID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Zadaci_Korisnici_KorisnikID",
                table: "Zadaci");

            migrationBuilder.RenameColumn(
                name: "KorisnikID",
                table: "Zadaci",
                newName: "korisnikId");

            migrationBuilder.RenameIndex(
                name: "IX_Zadaci_KorisnikID",
                table: "Zadaci",
                newName: "IX_Zadaci_korisnikId");

            migrationBuilder.AddForeignKey(
                name: "FK_Zadaci_Korisnici_korisnikId",
                table: "Zadaci",
                column: "korisnikId",
                principalTable: "Korisnici",
                principalColumn: "KorisnikID");
        }
    }
}
