using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace evojacu.Migrations
{
    /// <inheritdoc />
    public partial class test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AdminPaneli",
                columns: table => new
                {
                    AdminID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KorisnickoIme = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lozinka = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminPaneli", x => x.AdminID);
                });

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
                name: "Gosti",
                columns: table => new
                {
                    GostID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BrojPosjeta = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gosti", x => x.GostID);
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
                name: "Kategorije",
                columns: table => new
                {
                    KategorijaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kategorije", x => x.KategorijaID);
                });

            migrationBuilder.CreateTable(
                name: "Korisnici",
                columns: table => new
                {
                    KorisnikID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lozinka = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Korisnici", x => x.KorisnikID);
                });

            migrationBuilder.CreateTable(
                name: "Prioriteti",
                columns: table => new
                {
                    PrioritetID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Opis = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prioriteti", x => x.PrioritetID);
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
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PocetakVremena = table.Column<DateTime>(type: "datetime2", nullable: false),
                    KrajVremena = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                    BrojKartice = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DatumIsteka = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VrstePlacanja", x => x.VrstaPlacanjaID);
                });

            migrationBuilder.CreateTable(
                name: "Zadaci",
                columns: table => new
                {
                    ZadatakId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KategorijaID = table.Column<int>(type: "int", nullable: false),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Opis = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zadaci", x => x.ZadatakId);
                    table.ForeignKey(
                        name: "FK_Zadaci_Kategorije_KategorijaID",
                        column: x => x.KategorijaID,
                        principalTable: "Kategorije",
                        principalColumn: "KategorijaID");
                });

            migrationBuilder.CreateTable(
                name: "Poslodavci",
                columns: table => new
                {
                    PoslodavacID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KorisnikId = table.Column<int>(type: "int", nullable: false),
                    NazivKompanije = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Poslodavci", x => x.PoslodavacID);
                    table.ForeignKey(
                        name: "FK_Poslodavci_Korisnici_KorisnikId",
                        column: x => x.KorisnikId,
                        principalTable: "Korisnici",
                        principalColumn: "KorisnikID");
                });

            migrationBuilder.CreateTable(
                name: "Posloprimaoci",
                columns: table => new
                {
                    PosloprimaocID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KorisnikId = table.Column<int>(type: "int", nullable: false),
                    Strucnost = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posloprimaoci", x => x.PosloprimaocID);
                    table.ForeignKey(
                        name: "FK_Posloprimaoci_Korisnici_KorisnikId",
                        column: x => x.KorisnikId,
                        principalTable: "Korisnici",
                        principalColumn: "KorisnikID");
                });

            migrationBuilder.CreateTable(
                name: "RangListe",
                columns: table => new
                {
                    RangListaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KorisnikID = table.Column<int>(type: "int", nullable: false),
                    Pozicija = table.Column<int>(type: "int", nullable: false),
                    BrojZadataka = table.Column<int>(type: "int", nullable: false),
                    ProsjecnaOcjena = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RangListe", x => x.RangListaID);
                    table.ForeignKey(
                        name: "FK_RangListe_Korisnici_KorisnikID",
                        column: x => x.KorisnikID,
                        principalTable: "Korisnici",
                        principalColumn: "KorisnikID");
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
                    OpisPosla = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JePonuda = table.Column<bool>(type: "bit", nullable: false),
                    Adresa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KorisnikID = table.Column<int>(type: "int", nullable: false),
                    ZadatakStraniID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Poslovi", x => x.ZadatakID);
                    table.ForeignKey(
                        name: "FK_Poslovi_FazePoslova_FazaPoslaId",
                        column: x => x.FazaPoslaId,
                        principalTable: "FazePoslova",
                        principalColumn: "FazaPoslaID");
                    table.ForeignKey(
                        name: "FK_Poslovi_Gradovi_GradId",
                        column: x => x.GradId,
                        principalTable: "Gradovi",
                        principalColumn: "GradID");
                    table.ForeignKey(
                        name: "FK_Poslovi_Korisnici_KorisnikID",
                        column: x => x.KorisnikID,
                        principalTable: "Korisnici",
                        principalColumn: "KorisnikID");
                    table.ForeignKey(
                        name: "FK_Poslovi_VremenaIzvrsavanja_VrijemeIzvrsavanjaId",
                        column: x => x.VrijemeIzvrsavanjaId,
                        principalTable: "VremenaIzvrsavanja",
                        principalColumn: "VrijemeIzvrsavanjaID");
                    table.ForeignKey(
                        name: "FK_Poslovi_Zadaci_ZadatakStraniID",
                        column: x => x.ZadatakStraniID,
                        principalTable: "Zadaci",
                        principalColumn: "ZadatakId");
                });

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
                        principalColumn: "PoslodavacID");
                    table.ForeignKey(
                        name: "FK_EmailObavijesti_Posloprimaoci_PosloprimaocID",
                        column: x => x.PosloprimaocID,
                        principalTable: "Posloprimaoci",
                        principalColumn: "PosloprimaocID");
                });

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
                        principalColumn: "PosloprimaocID");
                    table.ForeignKey(
                        name: "FK_Obaveze_Prioriteti_PrioritetID",
                        column: x => x.PrioritetID,
                        principalTable: "Prioriteti",
                        principalColumn: "PrioritetID");
                });

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
                        principalColumn: "PoslodavacID");
                    table.ForeignKey(
                        name: "FK_Recenzije_Posloprimaoci_PosloprimaocID",
                        column: x => x.PosloprimaocID,
                        principalTable: "Posloprimaoci",
                        principalColumn: "PosloprimaocID");
                    table.ForeignKey(
                        name: "FK_Recenzije_Poslovi_PosaoID",
                        column: x => x.PosaoID,
                        principalTable: "Poslovi",
                        principalColumn: "ZadatakID");
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
                    Opis = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PosaoID = table.Column<int>(type: "int", nullable: false),
                    PoslodavacID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transakcije", x => x.TransakcijaID);
                    table.ForeignKey(
                        name: "FK_Transakcije_Poslodavci_PoslodavacID",
                        column: x => x.PoslodavacID,
                        principalTable: "Poslodavci",
                        principalColumn: "PoslodavacID");
                    table.ForeignKey(
                        name: "FK_Transakcije_Poslovi_PosaoID",
                        column: x => x.PosaoID,
                        principalTable: "Poslovi",
                        principalColumn: "ZadatakID");
                    table.ForeignKey(
                        name: "FK_Transakcije_StanjaPlacanja_StanjePlacanjaId",
                        column: x => x.StanjePlacanjaId,
                        principalTable: "StanjaPlacanja",
                        principalColumn: "StanjePlacanjaID");
                    table.ForeignKey(
                        name: "FK_Transakcije_VrstePlacanja_NacinPlacanjaId",
                        column: x => x.NacinPlacanjaId,
                        principalTable: "VrstePlacanja",
                        principalColumn: "VrstaPlacanjaID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmailObavijesti_PoslodavacID",
                table: "EmailObavijesti",
                column: "PoslodavacID");

            migrationBuilder.CreateIndex(
                name: "IX_EmailObavijesti_PosloprimaocID",
                table: "EmailObavijesti",
                column: "PosloprimaocID");

            migrationBuilder.CreateIndex(
                name: "IX_Obaveze_PosloprimaocID",
                table: "Obaveze",
                column: "PosloprimaocID");

            migrationBuilder.CreateIndex(
                name: "IX_Obaveze_PrioritetID",
                table: "Obaveze",
                column: "PrioritetID");

            migrationBuilder.CreateIndex(
                name: "IX_Poslodavci_KorisnikId",
                table: "Poslodavci",
                column: "KorisnikId");

            migrationBuilder.CreateIndex(
                name: "IX_Posloprimaoci_KorisnikId",
                table: "Posloprimaoci",
                column: "KorisnikId");

            migrationBuilder.CreateIndex(
                name: "IX_Poslovi_FazaPoslaId",
                table: "Poslovi",
                column: "FazaPoslaId");

            migrationBuilder.CreateIndex(
                name: "IX_Poslovi_GradId",
                table: "Poslovi",
                column: "GradId");

            migrationBuilder.CreateIndex(
                name: "IX_Poslovi_KorisnikID",
                table: "Poslovi",
                column: "KorisnikID");

            migrationBuilder.CreateIndex(
                name: "IX_Poslovi_VrijemeIzvrsavanjaId",
                table: "Poslovi",
                column: "VrijemeIzvrsavanjaId");

            migrationBuilder.CreateIndex(
                name: "IX_Poslovi_ZadatakStraniID",
                table: "Poslovi",
                column: "ZadatakStraniID");

            migrationBuilder.CreateIndex(
                name: "IX_RangListe_KorisnikID",
                table: "RangListe",
                column: "KorisnikID");

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

            migrationBuilder.CreateIndex(
                name: "IX_Transakcije_NacinPlacanjaId",
                table: "Transakcije",
                column: "NacinPlacanjaId");

            migrationBuilder.CreateIndex(
                name: "IX_Transakcije_PosaoID",
                table: "Transakcije",
                column: "PosaoID");

            migrationBuilder.CreateIndex(
                name: "IX_Transakcije_PoslodavacID",
                table: "Transakcije",
                column: "PoslodavacID");

            migrationBuilder.CreateIndex(
                name: "IX_Transakcije_StanjePlacanjaId",
                table: "Transakcije",
                column: "StanjePlacanjaId");

            migrationBuilder.CreateIndex(
                name: "IX_Zadaci_KategorijaID",
                table: "Zadaci",
                column: "KategorijaID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdminPaneli");

            migrationBuilder.DropTable(
                name: "EmailObavijesti");

            migrationBuilder.DropTable(
                name: "Gosti");

            migrationBuilder.DropTable(
                name: "Obaveze");

            migrationBuilder.DropTable(
                name: "RangListe");

            migrationBuilder.DropTable(
                name: "Recenzije");

            migrationBuilder.DropTable(
                name: "Transakcije");

            migrationBuilder.DropTable(
                name: "Prioriteti");

            migrationBuilder.DropTable(
                name: "Posloprimaoci");

            migrationBuilder.DropTable(
                name: "Poslodavci");

            migrationBuilder.DropTable(
                name: "Poslovi");

            migrationBuilder.DropTable(
                name: "StanjaPlacanja");

            migrationBuilder.DropTable(
                name: "VrstePlacanja");

            migrationBuilder.DropTable(
                name: "FazePoslova");

            migrationBuilder.DropTable(
                name: "Gradovi");

            migrationBuilder.DropTable(
                name: "Korisnici");

            migrationBuilder.DropTable(
                name: "VremenaIzvrsavanja");

            migrationBuilder.DropTable(
                name: "Zadaci");

            migrationBuilder.DropTable(
                name: "Kategorije");
        }
    }
}
