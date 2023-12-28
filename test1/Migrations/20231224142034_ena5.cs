using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace evojacu.Migrations
{
    /// <inheritdoc />
    public partial class ena5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Obaveze",
                columns: table => new
                {
                    ObavezaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PosloprimaocID = table.Column<int>(type: "int", nullable: false),
                    Opis = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DatumRokaIzvrsenja = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PrioritetID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Obaveze", x => x.ObavezaID);
                    table.ForeignKey(
                        name: "FK_Obaveze_Posloprimaoci_PosloprimaocID",
                        column: x => x.PosloprimaocID,
                        principalTable: "Posloprimaoci",
                        principalColumn: "PosloprimaocID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Obaveze_Prioriteti_PrioritetID",
                        column: x => x.PrioritetID,
                        principalTable: "Prioriteti",
                        principalColumn: "PrioritetID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Obaveze_PosloprimaocID",
                table: "Obaveze",
                column: "PosloprimaocID");

            migrationBuilder.CreateIndex(
                name: "IX_Obaveze_PrioritetID",
                table: "Obaveze",
                column: "PrioritetID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Obaveze");
        }
    }
}
