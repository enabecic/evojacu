using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace evojacu.Migrations
{
    /// <inheritdoc />
    public partial class ena6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmailObavijesti",
                columns: table => new
                {
                    EmailID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naslov = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sadrzaj = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DatumSlanja = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PoslodavacID = table.Column<int>(type: "int", nullable: false),
                    PosloprimaocID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailObavijesti", x => x.EmailID);
                    table.ForeignKey(
                        name: "FK_EmailObavijesti_Poslodavci_PoslodavacID",
                        column: x => x.PoslodavacID,
                        principalTable: "Poslodavci",
                        principalColumn: "PoslodavacID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_EmailObavijesti_Posloprimaoci_PosloprimaocID",
                        column: x => x.PosloprimaocID,
                        principalTable: "Posloprimaoci",
                        principalColumn: "PosloprimaocID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmailObavijesti_PoslodavacID",
                table: "EmailObavijesti",
                column: "PoslodavacID");

            migrationBuilder.CreateIndex(
                name: "IX_EmailObavijesti_PosloprimaocID",
                table: "EmailObavijesti",
                column: "PosloprimaocID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmailObavijesti");
        }
    }
}
