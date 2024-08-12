using evojacu.Models;
using Microsoft.AspNetCore.Mvc;

namespace evojacu.Modul_TestniPodaci
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class TestniPodaciController : ControllerBase
    {
        private readonly evojacuDBContext _dbContext;   

        public TestniPodaciController(evojacuDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public ActionResult Count()
        {
            Dictionary<string, int> data = new Dictionary<string, int>();
            data.Add("FazaPosla", _dbContext.FazePoslova.Count());
            data.Add("Grad", _dbContext.Gradovi.Count());
            data.Add("Kategorija", _dbContext.Kategorije.Count());
            data.Add("Zadatak", _dbContext.Zadaci.Count());
            data.Add("VrijemeIzvrsavanja", _dbContext.VremenaIzvrsavanja.Count());
            data.Add("Korisnik", _dbContext.Korisnici.Count());
            data.Add("Poslodavac", _dbContext.Poslodavci.Count());
            data.Add("Posloprimaoc", _dbContext.Posloprimaoci.Count());
            data.Add("Posao", _dbContext.Poslovi.Count());

            return Ok(data);
        }


        [HttpPost]
        public ActionResult Generisi()
        {
            var fazePoslova = new List<FazaPosla>();
            var gradovi = new List<Grad>();
            var kategorije = new List<Kategorija>();
            var zadaci = new List<Zadatak>();
            var vremenaIzvrsavanja = new List<VrijemeIzvrsavanja>();
            var korisnici = new List<Korisnik>();
            var poslodavci = new List<Poslodavac>();
            var posloprimaoci=new List<Posloprimaoc>();
            var poslovi = new List<Posao>();

            fazePoslova.Add(new FazaPosla { Opis = "Posao je aktivan", Naziv = "Aktivan" });
            fazePoslova.Add(new FazaPosla { Opis = "Posao nije aktivan", Naziv = "Neaktivan"});

            gradovi.Add(new Grad { Naziv = "Sarajevo" });
            gradovi.Add(new Grad { Naziv = "Banja Luka" });
            gradovi.Add(new Grad { Naziv = "Tuzla" });
            gradovi.Add(new Grad { Naziv = "Zenica" });
            gradovi.Add(new Grad { Naziv = "Mostar" });
            gradovi.Add(new Grad { Naziv = "Bijeljina" });
            gradovi.Add(new Grad { Naziv = "Brčko" });
            gradovi.Add(new Grad { Naziv = "Prijedor" });
            gradovi.Add(new Grad { Naziv = "Trebinje" });
            gradovi.Add(new Grad { Naziv = "Cazin" });
            gradovi.Add(new Grad { Naziv = "Doboj" });
            gradovi.Add(new Grad { Naziv = "Sanski Most" });
            gradovi.Add(new Grad { Naziv = "Goražde" });
            gradovi.Add(new Grad { Naziv = "Travnik" });
            gradovi.Add(new Grad { Naziv = "Široki Brijeg" });
            gradovi.Add(new Grad { Naziv = "Gradačac" });
            gradovi.Add(new Grad { Naziv = "Bihać" });
            gradovi.Add(new Grad { Naziv = "Zvornik" });
            gradovi.Add(new Grad { Naziv = "Gradiška" });
            gradovi.Add(new Grad { Naziv = "Lukavac" });
            gradovi.Add(new Grad { Naziv = "Visoko" });

            kategorije.Add(new Kategorija { Naziv = "Životinje" });//0
            kategorije.Add(new Kategorija { Naziv = "Građevinarstvo" });//1
            kategorije.Add(new Kategorija { Naziv = "Elektronika" });//2
            kategorije.Add(new Kategorija { Naziv = "Zdravstvo" });//3
            kategorije.Add(new Kategorija { Naziv = "Edukacija" });//4
            kategorije.Add(new Kategorija { Naziv = "Ugostiteljstvo" });//5
            kategorije.Add(new Kategorija { Naziv = "Poljoprivreda" });//6
            kategorije.Add(new Kategorija { Naziv = "Finansije" });//7
            kategorije.Add(new Kategorija { Naziv = "Kupovina" });//8
            kategorije.Add(new Kategorija { Naziv = "Dizajn i umjetnost" });//9
            kategorije.Add(new Kategorija { Naziv = "Ljepota i moda" });//10
            kategorije.Add(new Kategorija { Naziv = "Administracija" });//11
            kategorije.Add(new Kategorija { Naziv = "Automobili" });//12
            kategorije.Add(new Kategorija { Naziv = "Proizvodnja" });//13
            kategorije.Add(new Kategorija { Naziv = "Energetika" });//14
            kategorije.Add(new Kategorija { Naziv = "Turizam" });//15
            kategorije.Add(new Kategorija { Naziv = "Nekretnine" });//16
            kategorije.Add(new Kategorija { Naziv = "Kućni poslovi" });//17
            kategorije.Add(new Kategorija { Naziv = "Popravke" });//18
            kategorije.Add(new Kategorija { Naziv = "Osobne usluge" });//19
            kategorije.Add(new Kategorija { Naziv = "Zdravlje i fitness" });//20
            kategorije.Add(new Kategorija { Naziv = "IT i tehnologija" });//21
            kategorije.Add(new Kategorija { Naziv = "Kreativne usluge" });//22
            kategorije.Add(new Kategorija { Naziv = "Marketing i prodaja" });//23
            kategorije.Add(new Kategorija { Naziv = "Prijevoz i dostava" });//24
            kategorije.Add(new Kategorija { Naziv = "Ostalo" });//25


            zadaci.Add(new Zadatak { Naziv = "Čuvanje djece", Opis = "Čuvanje djece svih uzrasta", Kategorija = kategorije[19] });
            zadaci.Add(new Zadatak { Naziv = "Dostava hrane", Opis = "Dostavljanje naručenih obroka na kućnu adresu", Kategorija = kategorije[24] });
            zadaci.Add(new Zadatak { Naziv = "Prijevoz putnika", Opis = "Prijevoz putnika taksi vozilom", Kategorija = kategorije[24] });
            zadaci.Add(new Zadatak { Naziv = "Popravka kućanskih aparata", Opis = "Servis i popravka kućanskih aparata", Kategorija = kategorije[2] });
            zadaci.Add(new Zadatak { Naziv = "Branje voća", Opis = "Sezonsko branje voća u voćnjaku", Kategorija = kategorije[6] });
            zadaci.Add(new Zadatak { Naziv = "Održavanje farme", Opis = "Održavanje i briga o farmi i životinjama", Kategorija = kategorije[6] });
            zadaci.Add(new Zadatak { Naziv = "Čišćenje stana", Opis = "Redovno čišćenje i održavanje stana", Kategorija = kategorije[17] });
            zadaci.Add(new Zadatak { Naziv = "Pranje rublja", Opis = "Pranje, sušenje i peglanje odjeće", Kategorija = kategorije[17] });
            zadaci.Add(new Zadatak { Naziv = "Kuhanje obroka", Opis = "Priprema svakodnevnih obroka za porodicu", Kategorija = kategorije[17] });
            zadaci.Add(new Zadatak { Naziv = "Njega starijih osoba", Opis = "Pomoć i briga o starijim osobama u njihovom domu", Kategorija = kategorije[19] });
            zadaci.Add(new Zadatak { Naziv = "Šetanje pasa", Opis = "Redovno izvođenje pasa u šetnju", Kategorija = kategorije[19] });
            zadaci.Add(new Zadatak { Naziv = "Popravka slavine", Opis = "Popravka curenja vode iz slavine", Kategorija = kategorije[18] });
            zadaci.Add(new Zadatak { Naziv = "Popravka namještaja", Opis = "Popravka oštećenog namještaja u domu", Kategorija = kategorije[18] });
            zadaci.Add(new Zadatak { Naziv = "Zamjena sijalica", Opis = "Zamjena pregorjelih sijalica u stanu", Kategorija = kategorije[18] });
            zadaci.Add(new Zadatak { Naziv = "Dnevna kupovina namirnica", Opis = "Kupovina namirnica i potrepština za domaćinstvo", Kategorija = kategorije[8] });
            zadaci.Add(new Zadatak { Naziv = "Kupovina odjeće", Opis = "Pomoć u izboru i kupovini odjeće", Kategorija = kategorije[8] });
            zadaci.Add(new Zadatak { Naziv = "Kupovina poklona", Opis = "Pronalaženje i kupovina poklona za posebne prilike", Kategorija = kategorije[8] });
            zadaci.Add(new Zadatak { Naziv = "Pranje automobila", Opis = "Ručno pranje i poliranje automobila", Kategorija = kategorije[12] });
            zadaci.Add(new Zadatak { Naziv = "Zamjena ulja", Opis = "Zamjena motornog ulja i filtera na automobilu", Kategorija = kategorije[12] });
            zadaci.Add(new Zadatak { Naziv = "Vulkanizacija", Opis = "Promjena i balansiranje guma na automobilu", Kategorija = kategorije[12] });
            zadaci.Add(new Zadatak { Naziv = "Upravljanje dokumentacijom", Opis = "Organizacija i arhiviranje poslovne dokumentacije", Kategorija = kategorije[11] });
            zadaci.Add(new Zadatak { Naziv = "Zakazivanje sastanaka", Opis = "Organizacija i zakazivanje sastanaka i događaja", Kategorija = kategorije[11] });
            zadaci.Add(new Zadatak { Naziv = "Izrada izvještaja", Opis = "Kreiranje i analiza izvještaja za poslovne potrebe", Kategorija = kategorije[11] });
            zadaci.Add(new Zadatak { Naziv = "Podučavanje matematike", Opis = "Privatne lekcije iz matematike za osnovce i srednjoškolce", Kategorija = kategorije[4] });
            zadaci.Add(new Zadatak { Naziv = "Časovi stranih jezika", Opis = "Podučavanje stranih jezika poput engleskog ili njemačkog", Kategorija = kategorije[4] });
            zadaci.Add(new Zadatak { Naziv = "Održavanje vrtova", Opis = "Održavanje vrtova i zelenih površina", Kategorija = kategorije[6] });


            vremenaIzvrsavanja.Add(new VrijemeIzvrsavanja { KrajVremena = new DateTime(2024, 8, 22, 10, 50, 16, 400) });
            vremenaIzvrsavanja.Add(new VrijemeIzvrsavanja { KrajVremena = new DateTime(2024, 9, 15, 10, 50, 16, 400) });
            vremenaIzvrsavanja.Add(new VrijemeIzvrsavanja { KrajVremena = new DateTime(2024, 10, 25, 10, 50, 16, 400) });
            vremenaIzvrsavanja.Add(new VrijemeIzvrsavanja { KrajVremena = new DateTime(2024, 11, 12, 10, 50, 16, 400) });


            korisnici.Add(new Korisnik { Ime = "Aida", Prezime = "Kovačević", Username = "aida05", Lozinka = "aida123", Telefon = "123456789", Adresa = "Mahala bb", Email = "aida_aaiiddaa3@fit.ba", Zanimanje = "Profesor" });
            korisnici.Add(new Korisnik { Ime = "Samra", Prezime = "Kevelj", Username = "samra05", Lozinka = "123samra", Telefon = "123456789", Adresa = "Zalik bb", Email = "samra3@fit.ba", Zanimanje = "Doktor" });

            poslodavci.Add(new Poslodavac { Korisnik = korisnici[0], NazivKompanije="JU Srednja elektrotehnčka škola" });
            poslodavci.Add(new Poslodavac { Korisnik = korisnici[1], NazivKompanije = "Safet Mujić" });

            posloprimaoci.Add(new Posloprimaoc { Korisnik = korisnici[0], Strucnost="Viša stručna sprema" });
            posloprimaoci.Add(new Posloprimaoc { Korisnik = korisnici[1], Strucnost = "Visoka stručna sprema" });

            poslovi.Add(new Posao { Adresa ="Alekse Šantića 12", Cijena=20 , DatumObjave=DateTime.Now, UkljucenGPS=true, OpisPosla="Šetnja pasa kroz park", FazaPosla = fazePoslova[0], Grad = gradovi[4], Poslodavac = poslodavci[1], VrijemeIzvrsavanja = vremenaIzvrsavanja[2], Zadatak = zadaci[10] });
            poslovi.Add(new Posao { Adresa = "Alekse Šantića 6", Cijena = 50, DatumObjave = DateTime.Now, UkljucenGPS = false, OpisPosla = "Čuvanje djece u stanu", FazaPosla = fazePoslova[0], Grad = gradovi[4], Poslodavac = poslodavci[0], VrijemeIzvrsavanja = vremenaIzvrsavanja[1], Zadatak = zadaci[0] });
            poslovi.Add(new Posao { Adresa = "Splitska 23", Cijena = 80, DatumObjave = DateTime.Now, UkljucenGPS = false, OpisPosla = "Briga o starijim osobama", FazaPosla = fazePoslova[0], Grad = gradovi[4], Poslodavac = poslodavci[1], VrijemeIzvrsavanja = vremenaIzvrsavanja[3], Zadatak = zadaci[9] });
            poslovi.Add(new Posao { Adresa = "Dr.Ante Starčevića", Cijena = 45, DatumObjave = DateTime.Now, UkljucenGPS = false, OpisPosla = "Čišćenje stana", FazaPosla = fazePoslova[0], Grad = gradovi[4], Poslodavac = poslodavci[0], VrijemeIzvrsavanja = vremenaIzvrsavanja[1], Zadatak = zadaci[6] });
            poslovi.Add(new Posao { Adresa = "Braće Fejića", Cijena = 6, DatumObjave = DateTime.Now, UkljucenGPS = true, OpisPosla = "Dostava hrane", FazaPosla = fazePoslova[0], Grad = gradovi[4], Poslodavac = poslodavci[1], VrijemeIzvrsavanja = vremenaIzvrsavanja[2], Zadatak = zadaci[1] });
            poslovi.Add(new Posao { Adresa = "Adema Buća", Cijena = 50, DatumObjave = DateTime.Now, UkljucenGPS = false, OpisPosla = "Branje voća", FazaPosla = fazePoslova[0], Grad = gradovi[4], Poslodavac = poslodavci[0], VrijemeIzvrsavanja = vremenaIzvrsavanja[1], Zadatak = zadaci[4] });
            poslovi.Add(new Posao { Adresa = "Franjevačka", Cijena = 50, DatumObjave = DateTime.Now, UkljucenGPS = false, OpisPosla = "Održavanje životinja i bašče na farmi", FazaPosla = fazePoslova[0], Grad = gradovi[4], Poslodavac = poslodavci[0], VrijemeIzvrsavanja = vremenaIzvrsavanja[1], Zadatak = zadaci[5] });
            poslovi.Add(new Posao { Adresa = "Kneza Mihajla Viševića Humskog", Cijena = 20, DatumObjave = DateTime.Now, UkljucenGPS = false, OpisPosla = "Popravka slavine", FazaPosla = fazePoslova[0], Grad = gradovi[4], Poslodavac = poslodavci[0], VrijemeIzvrsavanja = vremenaIzvrsavanja[1], Zadatak = zadaci[11] });
            poslovi.Add(new Posao { Adresa = "Sjeverni logor bb", Cijena = 70, DatumObjave = DateTime.Now, UkljucenGPS = false, OpisPosla = "Popravka namještaja", FazaPosla = fazePoslova[0], Grad = gradovi[4], Poslodavac = poslodavci[0], VrijemeIzvrsavanja = vremenaIzvrsavanja[1], Zadatak = zadaci[12] });
            poslovi.Add(new Posao { Adresa = "Donja Drežnica bb", Cijena = 5, DatumObjave = DateTime.Now, UkljucenGPS = false, OpisPosla = "Zamjena sijalice", FazaPosla = fazePoslova[0], Grad = gradovi[4], Poslodavac = poslodavci[0], VrijemeIzvrsavanja = vremenaIzvrsavanja[1], Zadatak = zadaci[13] });

            _dbContext.AddRange(fazePoslova);
            _dbContext.AddRange(gradovi);
            _dbContext.AddRange(kategorije);
            _dbContext.AddRange(zadaci);
            _dbContext.AddRange(vremenaIzvrsavanja);
            _dbContext.AddRange(korisnici);
            _dbContext.AddRange(poslodavci);
            _dbContext.AddRange(posloprimaoci);
            _dbContext.AddRange(poslovi);

            _dbContext.SaveChanges();

            return Count();
        }
    }
}
