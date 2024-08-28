using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace evojacu.Migrations
{
    /// <inheritdoc />
    public partial class recenzija : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recenzije_Poslodavci_PoslodavacID",
                table: "Recenzije");

            migrationBuilder.DropIndex(
                name: "IX_Recenzije_PoslodavacID",
                table: "Recenzije");

            migrationBuilder.DropColumn(
                name: "Ocjena",
                table: "Recenzije");

            migrationBuilder.DropColumn(
                name: "PoslodavacID",
                table: "Recenzije");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Ocjena",
                table: "Recenzije",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "PoslodavacID",
                table: "Recenzije",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Recenzije_PoslodavacID",
                table: "Recenzije",
                column: "PoslodavacID");

            migrationBuilder.AddForeignKey(
                name: "FK_Recenzije_Poslodavci_PoslodavacID",
                table: "Recenzije",
                column: "PoslodavacID",
                principalTable: "Poslodavci",
                principalColumn: "PoslodavacID");
        }
    }
}
