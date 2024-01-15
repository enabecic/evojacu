
using evojacu.Models;
using Microsoft.EntityFrameworkCore;

namespace evojacu.Models
{
    
    public class evojacuDBContext:DbContext
    {
        public evojacuDBContext(DbContextOptions<evojacuDBContext> options) : base(options)
        {

        }

        public DbSet<Korisnik> Korisnici { get; set; }
        public DbSet<Poslodavac> Poslodavci { get; set; }
        public DbSet<Posloprimaoc> Posloprimaoci { get; set; }
        public DbSet<FazaPosla> FazePoslova { get; set; }
        public DbSet<Grad> Gradovi { get; set; }
        public DbSet<VrstaPlacanja> VrstePlacanja { get; set; }
        public DbSet<Transakcija> Transakcije { get; set; }
        public DbSet<Posao> Poslovi { get; set; }
        public DbSet<StanjePlacanja> StanjaPlacanja { get; set; }
        public DbSet<VrijemeIzvrsavanja> VremenaIzvrsavanja { get; set; }
        public DbSet<Kategorija> Kategorije { get; set; }
        public DbSet<Prioritet> Prioriteti { get; set; }
        public DbSet<AdminPanel> AdminPaneli { get; set; }
        public DbSet<Obaveza> Obaveze { get; set; }
        public DbSet<EmailObavijest> EmailObavijesti { get; set; }
        public DbSet<RangLista> RangListe { get; set; }
        public DbSet<Gost> Gosti { get; set; }
        public DbSet<Recenzija> Recenzije { get; set; }
        public DbSet<Zadatak> Zadaci { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.NoAction;
            }
           
        }



    }
}
