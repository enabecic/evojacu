using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace evojacu.Migrations
{
    /// <inheritdoc />
    public partial class posao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Poslovi_Korisnici_KorisnikID",
                table: "Poslovi");

            migrationBuilder.DropColumn(
                name: "JePonuda",
                table: "Poslovi");

            migrationBuilder.RenameColumn(
                name: "KorisnikID",
                table: "Poslovi",
                newName: "PoslodavacID");

            migrationBuilder.RenameIndex(
                name: "IX_Poslovi_KorisnikID",
                table: "Poslovi",
                newName: "IX_Poslovi_PoslodavacID");

            migrationBuilder.AddColumn<DateTime>(
                name: "DatumObjave",
                table: "Poslovi",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_Poslovi_Poslodavci_PoslodavacID",
                table: "Poslovi",
                column: "PoslodavacID",
                principalTable: "Poslodavci",
                principalColumn: "PoslodavacID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Poslovi_Poslodavci_PoslodavacID",
                table: "Poslovi");

            migrationBuilder.DropColumn(
                name: "DatumObjave",
                table: "Poslovi");

            migrationBuilder.RenameColumn(
                name: "PoslodavacID",
                table: "Poslovi",
                newName: "KorisnikID");

            migrationBuilder.RenameIndex(
                name: "IX_Poslovi_PoslodavacID",
                table: "Poslovi",
                newName: "IX_Poslovi_KorisnikID");

            migrationBuilder.AddColumn<bool>(
                name: "JePonuda",
                table: "Poslovi",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_Poslovi_Korisnici_KorisnikID",
                table: "Poslovi",
                column: "KorisnikID",
                principalTable: "Korisnici",
                principalColumn: "KorisnikID");
        }
    }
}
