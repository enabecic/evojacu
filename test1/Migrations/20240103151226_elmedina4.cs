using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace evojacu.Migrations
{
    /// <inheritdoc />
    public partial class elmedina4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Recenzije",
                columns: table => new
                {
                    RecenzijaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PosaoID = table.Column<int>(type: "int", nullable: false),
                    PoslodavacID = table.Column<int>(type: "int", nullable: false),
                    PosloprimaocID = table.Column<int>(type: "int", nullable: false),
                    Komentar = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ocjena = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recenzije", x => x.RecenzijaID);
                    table.ForeignKey(
                        name: "FK_Recenzije_Poslodavci_PoslodavacID",
                        column: x => x.PoslodavacID,
                        principalTable: "Poslodavci",
                        principalColumn: "PoslodavacID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Recenzije_Posloprimaoci_PosloprimaocID",
                        column: x => x.PosloprimaocID,
                        principalTable: "Posloprimaoci",
                        principalColumn: "PosloprimaocID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Recenzije_Poslovi_PosaoID",
                        column: x => x.PosaoID,
                        principalTable: "Poslovi",
                        principalColumn: "ZadatakID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Recenzije_PosaoID",
                table: "Recenzije",
                column: "PosaoID");

            migrationBuilder.CreateIndex(
                name: "IX_Recenzije_PoslodavacID",
                table: "Recenzije",
                column: "PoslodavacID");

            migrationBuilder.CreateIndex(
                name: "IX_Recenzije_PosloprimaocID",
                table: "Recenzije",
                column: "PosloprimaocID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Recenzije");
        }
    }
}
