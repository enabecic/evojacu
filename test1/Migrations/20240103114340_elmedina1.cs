using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace evojacu.Migrations
{
    /// <inheritdoc />
    public partial class elmedina1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "KorisnikID",
                table: "Poslovi",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Poslovi_KorisnikID",
                table: "Poslovi",
                column: "KorisnikID");

            migrationBuilder.AddForeignKey(
                name: "FK_Poslovi_Korisnici_KorisnikID",
                table: "Poslovi",
                column: "KorisnikID",
                principalTable: "Korisnici",
                principalColumn: "KorisnikID",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Poslovi_Korisnici_KorisnikID",
                table: "Poslovi");

            migrationBuilder.DropIndex(
                name: "IX_Poslovi_KorisnikID",
                table: "Poslovi");

            migrationBuilder.DropColumn(
                name: "KorisnikID",
                table: "Poslovi");
        }
    }
}
