using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace evojacu.Migrations
{
    /// <inheritdoc />
    public partial class ena9 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PosaoID",
                table: "Transakcije",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Transakcije_PosaoID",
                table: "Transakcije",
                column: "PosaoID");

            migrationBuilder.AddForeignKey(
                name: "FK_Transakcije_Poslovi_PosaoID",
                table: "Transakcije",
                column: "PosaoID",
                principalTable: "Poslovi",
                principalColumn: "ZadatakID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transakcije_Poslovi_PosaoID",
                table: "Transakcije");

            migrationBuilder.DropIndex(
                name: "IX_Transakcije_PosaoID",
                table: "Transakcije");

            migrationBuilder.DropColumn(
                name: "PosaoID",
                table: "Transakcije");
        }
    }
}
