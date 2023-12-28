using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace evojacu.Migrations
{
    /// <inheritdoc />
    public partial class test22 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FazePoslova",
                columns: table => new
                {
                    FazaPoslaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Opis = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FazePoslova", x => x.FazaPoslaID);
                });

            migrationBuilder.CreateTable(
                name: "Gradovi",
                columns: table => new
                {
                    GradID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gradovi", x => x.GradID);
                });

            migrationBuilder.CreateTable(
                name: "StanjaPlacanja",
                columns: table => new
                {
                    StanjePlacanjaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OpisStanja = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StanjaPlacanja", x => x.StanjePlacanjaID);
                });

            migrationBuilder.CreateTable(
                name: "VremenaIzvrsavanja",
                columns: table => new
                {
                    VrijemeIzvrsavanjaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VremenaIzvrsavanja", x => x.VrijemeIzvrsavanjaID);
                });

            migrationBuilder.CreateTable(
                name: "VrstePlacanja",
                columns: table => new
                {
                    VrstaPlacanjaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TipPlacanja = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BrojKartice = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DatumIsteka = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VrstePlacanja", x => x.VrstaPlacanjaID);
                });

            migrationBuilder.CreateTable(
                name: "Transakcije",
                columns: table => new
                {
                    TransakcijaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Iznos = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    VrijemeTransakcije = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NacinPlacanjaId = table.Column<int>(type: "int", nullable: false),
                    StanjePlacanjaId = table.Column<int>(type: "int", nullable: false),
                    Opis = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transakcije", x => x.TransakcijaID);
                    table.ForeignKey(
                        name: "FK_Transakcije_StanjaPlacanja_StanjePlacanjaId",
                        column: x => x.StanjePlacanjaId,
                        principalTable: "StanjaPlacanja",
                        principalColumn: "StanjePlacanjaID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transakcije_VrstePlacanja_NacinPlacanjaId",
                        column: x => x.NacinPlacanjaId,
                        principalTable: "VrstePlacanja",
                        principalColumn: "VrstaPlacanjaID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Poslovi",
                columns: table => new
                {
                    ZadatakID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VrijemeIzvrsavanjaId = table.Column<int>(type: "int", nullable: false),
                    GradId = table.Column<int>(type: "int", nullable: false),
                    FazaPoslaId = table.Column<int>(type: "int", nullable: false),
                    TransakcijaId = table.Column<int>(type: "int", nullable: false),
                    OpisPosla = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JePonuda = table.Column<bool>(type: "bit", nullable: false),
                    Adresa = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Poslovi", x => x.ZadatakID);
                    table.ForeignKey(
                        name: "FK_Poslovi_FazePoslova_FazaPoslaId",
                        column: x => x.FazaPoslaId,
                        principalTable: "FazePoslova",
                        principalColumn: "FazaPoslaID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Poslovi_Gradovi_GradId",
                        column: x => x.GradId,
                        principalTable: "Gradovi",
                        principalColumn: "GradID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Poslovi_Transakcije_TransakcijaId",
                        column: x => x.TransakcijaId,
                        principalTable: "Transakcije",
                        principalColumn: "TransakcijaID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Poslovi_VremenaIzvrsavanja_VrijemeIzvrsavanjaId",
                        column: x => x.VrijemeIzvrsavanjaId,
                        principalTable: "VremenaIzvrsavanja",
                        principalColumn: "VrijemeIzvrsavanjaID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Poslovi_FazaPoslaId",
                table: "Poslovi",
                column: "FazaPoslaId");

            migrationBuilder.CreateIndex(
                name: "IX_Poslovi_GradId",
                table: "Poslovi",
                column: "GradId");

            migrationBuilder.CreateIndex(
                name: "IX_Poslovi_TransakcijaId",
                table: "Poslovi",
                column: "TransakcijaId");

            migrationBuilder.CreateIndex(
                name: "IX_Poslovi_VrijemeIzvrsavanjaId",
                table: "Poslovi",
                column: "VrijemeIzvrsavanjaId");

            migrationBuilder.CreateIndex(
                name: "IX_Transakcije_NacinPlacanjaId",
                table: "Transakcije",
                column: "NacinPlacanjaId");

            migrationBuilder.CreateIndex(
                name: "IX_Transakcije_StanjePlacanjaId",
                table: "Transakcije",
                column: "StanjePlacanjaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Poslovi");

            migrationBuilder.DropTable(
                name: "FazePoslova");

            migrationBuilder.DropTable(
                name: "Gradovi");

            migrationBuilder.DropTable(
                name: "Transakcije");

            migrationBuilder.DropTable(
                name: "VremenaIzvrsavanja");

            migrationBuilder.DropTable(
                name: "StanjaPlacanja");

            migrationBuilder.DropTable(
                name: "VrstePlacanja");
        }
    }
}
