using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace evojacu.Migrations
{
    /// <inheritdoc />
    public partial class OdabraniPosao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OdabraniPoslovi",
                columns: table => new
                {
                    OdabraniPosaoID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PosloprimaocID = table.Column<int>(type: "int", nullable: false),
                    PosaoID = table.Column<int>(type: "int", nullable: false),
                    DatumOdabira = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OdabraniPoslovi", x => x.OdabraniPosaoID);
                    table.ForeignKey(
                        name: "FK_OdabraniPoslovi_Posloprimaoci_PosloprimaocID",
                        column: x => x.PosloprimaocID,
                        principalTable: "Posloprimaoci",
                        principalColumn: "PosloprimaocID");
                    table.ForeignKey(
                        name: "FK_OdabraniPoslovi_Poslovi_PosaoID",
                        column: x => x.PosaoID,
                        principalTable: "Poslovi",
                        principalColumn: "ZadatakID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_OdabraniPoslovi_PosaoID",
                table: "OdabraniPoslovi",
                column: "PosaoID");

            migrationBuilder.CreateIndex(
                name: "IX_OdabraniPoslovi_PosloprimaocID",
                table: "OdabraniPoslovi",
                column: "PosloprimaocID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OdabraniPoslovi");
        }
    }
}
