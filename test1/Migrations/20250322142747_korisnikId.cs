using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace evojacu.Migrations
{
    /// <inheritdoc />
    public partial class korisnikId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "korisnikId",
                table: "Poslovi",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Poslovi_korisnikId",
                table: "Poslovi",
                column: "korisnikId");

            migrationBuilder.AddForeignKey(
                name: "FK_Poslovi_Korisnici_korisnikId",
                table: "Poslovi",
                column: "korisnikId",
                principalTable: "Korisnici",
                principalColumn: "KorisnikID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Poslovi_Korisnici_korisnikId",
                table: "Poslovi");

            migrationBuilder.DropIndex(
                name: "IX_Poslovi_korisnikId",
                table: "Poslovi");

            migrationBuilder.DropColumn(
                name: "korisnikId",
                table: "Poslovi");
        }
    }
}
