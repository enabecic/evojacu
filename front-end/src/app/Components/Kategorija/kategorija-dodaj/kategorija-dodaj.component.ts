import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { MojConfig } from '../../../moj-config';

interface Kategorija {
  kategorijaID: number;
  naziv: string;
}

@Component({
  selector: 'app-kategorija-dodaj',
  templateUrl: './kategorija-dodaj.component.html',
  styleUrls: ['./kategorija-dodaj.component.css']
})
export class KategorijaDodajComponent implements OnInit {

  kategorije: Kategorija[] = [];
  nova_kategorija: any;

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
  }

  snimiKategoriju() {
    this.http.post("https://localhost:7027/Kategorija-dodaj", this.nova_kategorija).subscribe((x: any) => {
      alert("uredu");
      this.getKategorije(); // osveži listu kategorija nakon dodavanja nove
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
      this.kategorije = response.kategorije;
    });
  }

  obrisiKategoriju(kategorijaID: number): void {
    if (confirm('Da li ste sigurni da želite obrisati ovu kategoriju?')) {
      this.http.delete(`${MojConfig.adresa_servera}/Kategorija-obrisi`, { params: { KategorijaID: kategorijaID.toString() } })
        .subscribe(() => {
          this.getKategorije(); // Osveži listu kategorija nakon brisanja
          alert("Kategorija obrisana");
        }, error => {
          alert("Greška prilikom brisanja kategorije");
        });
    }
  }

  protected readonly MojConfig = MojConfig;

  urediKategoriju(item: Kategorija) {
    
  }
}
