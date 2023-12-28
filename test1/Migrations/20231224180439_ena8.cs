using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace evojacu.Migrations
{
    /// <inheritdoc />
    public partial class ena8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Poslovi_Transakcije_TransakcijaId",
                table: "Poslovi");

            migrationBuilder.DropIndex(
                name: "IX_Poslovi_TransakcijaId",
                table: "Poslovi");

            migrationBuilder.DropColumn(
                name: "TransakcijaId",
                table: "Poslovi");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TransakcijaId",
                table: "Poslovi",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Poslovi_TransakcijaId",
                table: "Poslovi",
                column: "TransakcijaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Poslovi_Transakcije_TransakcijaId",
                table: "Poslovi",
                column: "TransakcijaId",
                principalTable: "Transakcije",
                principalColumn: "TransakcijaID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
