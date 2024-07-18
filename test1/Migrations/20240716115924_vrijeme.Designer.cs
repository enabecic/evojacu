﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using evojacu.Models;

#nullable disable

namespace evojacu.Migrations
{
    [DbContext(typeof(evojacuDBContext))]
    [Migration("20240716115924_vrijeme")]
    partial class vrijeme
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("evojacu.Models.AdminPanel", b =>
                {
                    b.Property<int>("AdminID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AdminID"));

                    b.Property<string>("KorisnickoIme")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Lozinka")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AdminID");

                    b.ToTable("AdminPaneli");
                });

            modelBuilder.Entity("evojacu.Models.EmailObavijest", b =>
                {
                    b.Property<int>("EmailID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EmailID"));

                    b.Property<DateTime>("DatumSlanja")
                        .HasColumnType("datetime2");

                    b.Property<string>("Naslov")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PoslodavacID")
                        .HasColumnType("int");

                    b.Property<int>("PosloprimaocID")
                        .HasColumnType("int");

                    b.Property<string>("Sadrzaj")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("EmailID");

                    b.HasIndex("PoslodavacID");

                    b.HasIndex("PosloprimaocID");

                    b.ToTable("EmailObavijesti");
                });

            modelBuilder.Entity("evojacu.Models.FazaPosla", b =>
                {
                    b.Property<int>("FazaPoslaID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FazaPoslaID"));

                    b.Property<string>("Naziv")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Opis")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("FazaPoslaID");

                    b.ToTable("FazePoslova");
                });

            modelBuilder.Entity("evojacu.Models.Gost", b =>
                {
                    b.Property<int>("GostID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("GostID"));

                    b.Property<int>("BrojPosjeta")
                        .HasColumnType("int");

                    b.HasKey("GostID");

                    b.ToTable("Gosti");
                });

            modelBuilder.Entity("evojacu.Models.Grad", b =>
                {
                    b.Property<int>("GradID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("GradID"));

                    b.Property<string>("Naziv")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("GradID");

                    b.ToTable("Gradovi");
                });

            modelBuilder.Entity("evojacu.Models.Kategorija", b =>
                {
                    b.Property<int>("KategorijaID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("KategorijaID"));

                    b.Property<string>("Naziv")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("KategorijaID");

                    b.ToTable("Kategorije");
                });

            modelBuilder.Entity("evojacu.Models.Korisnik", b =>
                {
                    b.Property<int>("KorisnikID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("KorisnikID"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Lozinka")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("KorisnikID");

                    b.ToTable("Korisnici");
                });

            modelBuilder.Entity("evojacu.Models.Obaveza", b =>
                {
                    b.Property<int>("ObavezaID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ObavezaID"));

                    b.Property<DateTime>("DatumRokaIzvrsenja")
                        .HasColumnType("datetime2");

                    b.Property<string>("Opis")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PosloprimaocID")
                        .HasColumnType("int");

                    b.Property<int>("PrioritetID")
                        .HasColumnType("int");

                    b.HasKey("ObavezaID");

                    b.HasIndex("PosloprimaocID");

                    b.HasIndex("PrioritetID");

                    b.ToTable("Obaveze");
                });

            modelBuilder.Entity("evojacu.Models.Posao", b =>
                {
                    b.Property<int>("ZadatakID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ZadatakID"));

                    b.Property<string>("Adresa")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("FazaPoslaId")
                        .HasColumnType("int");

                    b.Property<int>("GradId")
                        .HasColumnType("int");

                    b.Property<bool>("JePonuda")
                        .HasColumnType("bit");

                    b.Property<int>("KorisnikID")
                        .HasColumnType("int");

                    b.Property<string>("OpisPosla")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("VrijemeIzvrsavanjaId")
                        .HasColumnType("int");

                    b.Property<int>("ZadatakStraniID")
                        .HasColumnType("int");

                    b.HasKey("ZadatakID");

                    b.HasIndex("FazaPoslaId");

                    b.HasIndex("GradId");

                    b.HasIndex("KorisnikID");

                    b.HasIndex("VrijemeIzvrsavanjaId");

                    b.HasIndex("ZadatakStraniID");

                    b.ToTable("Poslovi");
                });

            modelBuilder.Entity("evojacu.Models.Poslodavac", b =>
                {
                    b.Property<int>("PoslodavacID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PoslodavacID"));

                    b.Property<int>("KorisnikId")
                        .HasColumnType("int");

                    b.Property<string>("NazivKompanije")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PoslodavacID");

                    b.HasIndex("KorisnikId");

                    b.ToTable("Poslodavci");
                });

            modelBuilder.Entity("evojacu.Models.Posloprimaoc", b =>
                {
                    b.Property<int>("PosloprimaocID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PosloprimaocID"));

                    b.Property<int>("KorisnikId")
                        .HasColumnType("int");

                    b.Property<string>("Strucnost")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PosloprimaocID");

                    b.HasIndex("KorisnikId");

                    b.ToTable("Posloprimaoci");
                });

            modelBuilder.Entity("evojacu.Models.Prioritet", b =>
                {
                    b.Property<int>("PrioritetID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PrioritetID"));

                    b.Property<string>("Naziv")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Opis")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PrioritetID");

                    b.ToTable("Prioriteti");
                });

            modelBuilder.Entity("evojacu.Models.RangLista", b =>
                {
                    b.Property<int>("RangListaID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RangListaID"));

                    b.Property<int>("BrojZadataka")
                        .HasColumnType("int");

                    b.Property<int>("KorisnikID")
                        .HasColumnType("int");

                    b.Property<int>("Pozicija")
                        .HasColumnType("int");

                    b.Property<double>("ProsjecnaOcjena")
                        .HasColumnType("float");

                    b.HasKey("RangListaID");

                    b.HasIndex("KorisnikID");

                    b.ToTable("RangListe");
                });

            modelBuilder.Entity("evojacu.Models.Recenzija", b =>
                {
                    b.Property<int>("RecenzijaID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RecenzijaID"));

                    b.Property<string>("Komentar")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Ocjena")
                        .HasColumnType("float");

                    b.Property<int>("PosaoID")
                        .HasColumnType("int");

                    b.Property<int>("PoslodavacID")
                        .HasColumnType("int");

                    b.Property<int>("PosloprimaocID")
                        .HasColumnType("int");

                    b.HasKey("RecenzijaID");

                    b.HasIndex("PosaoID");

                    b.HasIndex("PoslodavacID");

                    b.HasIndex("PosloprimaocID");

                    b.ToTable("Recenzije");
                });

            modelBuilder.Entity("evojacu.Models.StanjePlacanja", b =>
                {
                    b.Property<int>("StanjePlacanjaID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StanjePlacanjaID"));

                    b.Property<string>("OpisStanja")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("StanjePlacanjaID");

                    b.ToTable("StanjaPlacanja");
                });

            modelBuilder.Entity("evojacu.Models.Transakcija", b =>
                {
                    b.Property<int>("TransakcijaID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TransakcijaID"));

                    b.Property<decimal>("Iznos")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("NacinPlacanjaId")
                        .HasColumnType("int");

                    b.Property<string>("Opis")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PosaoID")
                        .HasColumnType("int");

                    b.Property<int>("PoslodavacID")
                        .HasColumnType("int");

                    b.Property<int>("StanjePlacanjaId")
                        .HasColumnType("int");

                    b.Property<DateTime>("VrijemeTransakcije")
                        .HasColumnType("datetime2");

                    b.HasKey("TransakcijaID");

                    b.HasIndex("NacinPlacanjaId");

                    b.HasIndex("PosaoID");

                    b.HasIndex("PoslodavacID");

                    b.HasIndex("StanjePlacanjaId");

                    b.ToTable("Transakcije");
                });

            modelBuilder.Entity("evojacu.Models.VrijemeIzvrsavanja", b =>
                {
                    b.Property<int>("VrijemeIzvrsavanjaID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("VrijemeIzvrsavanjaID"));

                    b.Property<DateTime>("KrajVremena")
                        .HasColumnType("datetime2");

                    b.HasKey("VrijemeIzvrsavanjaID");

                    b.ToTable("VremenaIzvrsavanja");
                });

            modelBuilder.Entity("evojacu.Models.VrstaPlacanja", b =>
                {
                    b.Property<int>("VrstaPlacanjaID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("VrstaPlacanjaID"));

                    b.Property<string>("BrojKartice")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DatumIsteka")
                        .HasColumnType("datetime2");

                    b.Property<string>("TipPlacanja")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("VrstaPlacanjaID");

                    b.ToTable("VrstePlacanja");
                });

            modelBuilder.Entity("evojacu.Models.Zadatak", b =>
                {
                    b.Property<int>("ZadatakId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ZadatakId"));

                    b.Property<int>("KategorijaID")
                        .HasColumnType("int");

                    b.Property<string>("Naziv")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Opis")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ZadatakId");

                    b.HasIndex("KategorijaID");

                    b.ToTable("Zadaci");
                });

            modelBuilder.Entity("evojacu.Models.EmailObavijest", b =>
                {
                    b.HasOne("evojacu.Models.Poslodavac", "Poslodavac")
                        .WithMany()
                        .HasForeignKey("PoslodavacID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("evojacu.Models.Posloprimaoc", "Posloprimaoc")
                        .WithMany()
                        .HasForeignKey("PosloprimaocID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Poslodavac");

                    b.Navigation("Posloprimaoc");
                });

            modelBuilder.Entity("evojacu.Models.Obaveza", b =>
                {
                    b.HasOne("evojacu.Models.Posloprimaoc", "Posloprimaoc")
                        .WithMany()
                        .HasForeignKey("PosloprimaocID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("evojacu.Models.Prioritet", "Prioritet")
                        .WithMany()
                        .HasForeignKey("PrioritetID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Posloprimaoc");

                    b.Navigation("Prioritet");
                });

            modelBuilder.Entity("evojacu.Models.Posao", b =>
                {
                    b.HasOne("evojacu.Models.FazaPosla", "FazaPosla")
                        .WithMany()
                        .HasForeignKey("FazaPoslaId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("evojacu.Models.Grad", "Grad")
                        .WithMany()
                        .HasForeignKey("GradId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("evojacu.Models.Korisnik", "Korisnik")
                        .WithMany()
                        .HasForeignKey("KorisnikID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("evojacu.Models.VrijemeIzvrsavanja", "VrijemeIzvrsavanja")
                        .WithMany()
                        .HasForeignKey("VrijemeIzvrsavanjaId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("evojacu.Models.Zadatak", "Zadatak")
                        .WithMany()
                        .HasForeignKey("ZadatakStraniID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("FazaPosla");

                    b.Navigation("Grad");

                    b.Navigation("Korisnik");

                    b.Navigation("VrijemeIzvrsavanja");

                    b.Navigation("Zadatak");
                });

            modelBuilder.Entity("evojacu.Models.Poslodavac", b =>
                {
                    b.HasOne("evojacu.Models.Korisnik", "Korisnik")
                        .WithMany()
                        .HasForeignKey("KorisnikId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Korisnik");
                });

            modelBuilder.Entity("evojacu.Models.Posloprimaoc", b =>
                {
                    b.HasOne("evojacu.Models.Korisnik", "Korisnik")
                        .WithMany()
                        .HasForeignKey("KorisnikId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Korisnik");
                });

            modelBuilder.Entity("evojacu.Models.RangLista", b =>
                {
                    b.HasOne("evojacu.Models.Korisnik", "Korisnik")
                        .WithMany()
                        .HasForeignKey("KorisnikID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Korisnik");
                });

            modelBuilder.Entity("evojacu.Models.Recenzija", b =>
                {
                    b.HasOne("evojacu.Models.Posao", "Posao")
                        .WithMany()
                        .HasForeignKey("PosaoID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("evojacu.Models.Poslodavac", "Poslodavac")
                        .WithMany()
                        .HasForeignKey("PoslodavacID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("evojacu.Models.Posloprimaoc", "Posloprimaoc")
                        .WithMany()
                        .HasForeignKey("PosloprimaocID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Posao");

                    b.Navigation("Poslodavac");

                    b.Navigation("Posloprimaoc");
                });

            modelBuilder.Entity("evojacu.Models.Transakcija", b =>
                {
                    b.HasOne("evojacu.Models.VrstaPlacanja", "VrstaPlacanja")
                        .WithMany()
                        .HasForeignKey("NacinPlacanjaId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("evojacu.Models.Posao", "Posao")
                        .WithMany()
                        .HasForeignKey("PosaoID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("evojacu.Models.Poslodavac", "Poslodavac")
                        .WithMany()
                        .HasForeignKey("PoslodavacID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("evojacu.Models.StanjePlacanja", "StanjePlacanja")
                        .WithMany()
                        .HasForeignKey("StanjePlacanjaId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Posao");

                    b.Navigation("Poslodavac");

                    b.Navigation("StanjePlacanja");

                    b.Navigation("VrstaPlacanja");
                });

            modelBuilder.Entity("evojacu.Models.Zadatak", b =>
                {
                    b.HasOne("evojacu.Models.Kategorija", "Kategorija")
                        .WithMany()
                        .HasForeignKey("KategorijaID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Kategorija");
                });
#pragma warning restore 612, 618
        }
    }
}
