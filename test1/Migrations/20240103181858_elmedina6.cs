using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace evojacu.Migrations
{
    /// <inheritdoc />
    public partial class elmedina6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ZadatakStraniID",
                table: "Poslovi",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Poslovi_ZadatakStraniID",
                table: "Poslovi",
                column: "ZadatakStraniID");

            migrationBuilder.AddForeignKey(
                name: "FK_Poslovi_Zadaci_ZadatakStraniID",
                table: "Poslovi",
                column: "ZadatakStraniID",
                principalTable: "Zadaci",
                principalColumn: "ZadatakId",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Poslovi_Zadaci_ZadatakStraniID",
                table: "Poslovi");

            migrationBuilder.DropIndex(
                name: "IX_Poslovi_ZadatakStraniID",
                table: "Poslovi");

            migrationBuilder.DropColumn(
                name: "ZadatakStraniID",
                table: "Poslovi");
        }
    }
}
