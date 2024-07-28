import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { MojConfig } from '../../../moj-config';

interface Kategorija {
  kategorijaID: number;
  naziv: string;
  timestamp?: number;
}

@Component({
  selector: 'app-kategorija-dodaj',
  templateUrl: './kategorija-dodaj.component.html',
  styleUrls: ['./kategorija-dodaj.component.css']
})
export class KategorijaDodajComponent implements OnInit {

  kategorije: Kategorija[] = [];
  nova_kategorija: any;
  nazivInvalid: boolean = false;

  constructor(private route: ActivatedRoute, private http: HttpClient) {}

  ngOnInit(): void {
    this.getKategorije();
  }

  novaKategorija() {
    this.nova_kategorija = {
      kategorijaID: 0,
      naziv: '',
      slika_base64_format: ''
    };
    this.nazivInvalid = false; // Resetujte stanje validacije prilikom dodavanja nove kategorije
  }

  snimiKategoriju() {
    if (!this.nova_kategorija.naziv) {
      this.nazivInvalid = true; // Postavite stanje validacije na true
      return;
    }

    const url = this.nova_kategorija.kategorijaID === 0
      ? `${MojConfig.adresa_servera}/Kategorija-dodaj`
      : `${MojConfig.adresa_servera}/Kategorija-update`;

    this.http.post(url, this.nova_kategorija).subscribe((x: any) => {
      alert("uredu");
      this.getKategorije(); // osveži listu kategorija nakon dodavanja ili uređivanja
    });

    this.nova_kategorija = null;
  }

  generisi_preview() {
    var file = (document.getElementById("slika-input") as HTMLInputElement).files?.[0];
    if (file && this.nova_kategorija) {
      var reader = new FileReader();
      reader.onload = () => {
        this.nova_kategorija.slika_base64_format = reader.result?.toString();
      }
      reader.readAsDataURL(file);
    }
  }

  getKategorije(): void {
    this.http.get<{ kategorije: Kategorija[] }>(`${MojConfig.adresa_servera}/Kategorija-preuzmi`).subscribe(response => {
      this.kategorije = response.kategorije.map(kat => ({
        ...kat,
        timestamp: Date.now() // Dodavanje timestamp-a za cache busting
      }));
    });
  }

  obrisiKategoriju(kategorijaID: number): void {
    if (confirm('Da li ste sigurni da želite obrisati ovu kategoriju?')) {
      this.http.delete(`${MojConfig.adresa_servera}/Kategorija-obrisi`, { params: { KategorijaID: kategorijaID.toString() } })
        .subscribe(() => {
          this.getKategorije(); // Osveži listu kategorija nakon brisanja
          alert("Kategorija obrisana");
        }, error => {
          alert("Greška prilikom brisanja kategorije, kategorija se koristi u zadacima");
        });
    }
  }

  protected readonly MojConfig = MojConfig;

  urediKategoriju(item: Kategorija) {
    this.nova_kategorija = {
      kategorijaID: item.kategorijaID,
      naziv: item.naziv,
      slika_base64_format: ''
    };
    this.nazivInvalid = false; // Resetujte stanje validacije prilikom uređivanja kategorije
  }

  onNazivChange() {
    this.nazivInvalid = !this.nova_kategorija.naziv;
  }
}
