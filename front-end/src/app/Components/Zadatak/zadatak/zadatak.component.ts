import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { MojConfig } from '../../../moj-config';
import {JezikService} from "../../../Services/jezik.service";

interface Kategorija {
  kategorijaID: number;
  naziv: string;
}

interface Zadatak {
  zadatakId: number;
  naziv: string;
  opis: string;
  kategorijaID: number;
  slika_base64_format?: string;
}

@Component({
  selector: 'app-zadatak',
  templateUrl: './zadatak.component.html',
  styleUrls: ['./zadatak.component.css']
})
export class ZadatakComponent implements OnInit {

  novi_zadatak: any;
  kategorijePodaci: Kategorija[] = [];
  zadaciPodaci: Zadatak[] = [];
  nazivInvalid: boolean = false;
  opisInvalid: boolean = false;

  constructor(private route: ActivatedRoute, private http: HttpClient, public jezikService: JezikService) {}

  ngOnInit(): void {
    this.getKategorije();
    this.getZadaci();
  }

  noviZadatak() {
    this.novi_zadatak = {
      zadatakId: 0,
      naziv: '',
      opis: '',
      kategorijaID: 1,
      slika_base64_format: ''
    };
    this.nazivInvalid = false;
    this.opisInvalid = false;
  }

  snimiZadatak() {
    if (!this.novi_zadatak?.naziv) {
      this.nazivInvalid = true;
    } else {
      this.nazivInvalid = false;
    }

    if (!this.novi_zadatak?.opis) {
      this.opisInvalid = true;
    } else {
      this.opisInvalid = false;
    }

    if (this.nazivInvalid || this.opisInvalid) {
      return;
    }

    const url = this.novi_zadatak.zadatakId === 0
      ? `${MojConfig.adresa_servera}/Zadatak-dodaj`
      : `${MojConfig.adresa_servera}/Zadatak-update`;

    this.http.post(url, this.novi_zadatak).subscribe(() => {
      alert("Zadatak snimljen");
      this.novi_zadatak = null;
      this.getZadaci(); // Osvježavanje liste zadataka
    });
  }

  generisi_preview() {
    var file = (document.getElementById("slika-input") as HTMLInputElement).files?.[0];
    if (file && this.novi_zadatak) {
      var reader = new FileReader();
      reader.onload = () => {
        this.novi_zadatak.slika_base64_format = reader.result?.toString();
      };
      reader.readAsDataURL(file);
    }
  }

  getKategorije(): void {
    this.http.get<{ kategorije: Kategorija[] }>(`${MojConfig.adresa_servera}/Kategorija-preuzmi`).subscribe(response => {
      this.kategorijePodaci = response.kategorije;
    });
  }

  getZadaci(): void {
    this.http.get<{ zadaci: Zadatak[] }>(`${MojConfig.adresa_servera}/Zadatak-preuzmi`).subscribe(response => {
      this.zadaciPodaci = response.zadaci;
    });
  }

  obrisiZadatak(zadatakId: number): void {
    if (confirm('Da li ste sigurni da želite obrisati ovaj zadatak?')) {
      this.http.delete(`${MojConfig.adresa_servera}/Zadatak-obrisi`, { params: { ZadatakID: zadatakId.toString() } })
        .subscribe(() => {
          this.getZadaci(); // Osvježavanje liste zadataka nakon brisanja
          alert("Zadatak obrisan");
        }, error => {
          alert("Greška prilikom brisanja zadatka");
        });
    }
  }

  urediZadatak(item: Zadatak) {
    this.novi_zadatak = {
      zadatakId: item.zadatakId,
      naziv: item.naziv,
      opis: item.opis,
      kategorijaID: item.kategorijaID,
      slika_base64_format: item.slika_base64_format || ''
    };
    this.nazivInvalid = false;
    this.opisInvalid = false;
  }

  onNazivChange() {
    this.nazivInvalid = !this.novi_zadatak?.naziv;
  }

  onOpisChange() {
    this.opisInvalid = !this.novi_zadatak?.opis;
  }

  protected readonly MojConfig = MojConfig;
}
