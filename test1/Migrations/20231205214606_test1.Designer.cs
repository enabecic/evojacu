﻿// <auto-generated />
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
    [Migration("20231205214606_evojacu")]
    partial class evojacu
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

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

            modelBuilder.Entity("evojacu.Models.Poslodavac", b =>
                {
                    b.HasOne("evojacu.Models.Korisnik", "Korisnik")
                        .WithMany()
                        .HasForeignKey("KorisnikId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Korisnik");
                });

            modelBuilder.Entity("evojacu.Models.Posloprimaoc", b =>
                {
                    b.HasOne("evojacu.Models.Korisnik", "Korisnik")
                        .WithMany()
                        .HasForeignKey("KorisnikId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Korisnik");
                });
#pragma warning restore 612, 618
        }
    }
}
