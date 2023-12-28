using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace evojacu.Migrations
{
    /// <inheritdoc />
    public partial class ena10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PoslodavacID",
                table: "Transakcije",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Transakcije_PoslodavacID",
                table: "Transakcije",
                column: "PoslodavacID");

            migrationBuilder.AddForeignKey(
                name: "FK_Transakcije_Poslodavci_PoslodavacID",
                table: "Transakcije",
                column: "PoslodavacID",
                principalTable: "Poslodavci",
                principalColumn: "PoslodavacID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transakcije_Poslodavci_PoslodavacID",
                table: "Transakcije");

            migrationBuilder.DropIndex(
                name: "IX_Transakcije_PoslodavacID",
                table: "Transakcije");

            migrationBuilder.DropColumn(
                name: "PoslodavacID",
                table: "Transakcije");
        }
    }
}
