import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { MojConfig } from '../../../moj-config';

interface Kategorija {
  kategorijaID: number;
  naziv: string;
}

interface Zadatak {
  zadatakId: number;
  naziv: string;
}

@Component({
  selector: 'app-zadatak',
  templateUrl: './zadatak.component.html',
  styleUrls: ['./zadatak.component.css']
})
export class ZadatakComponent implements OnInit {

  novi_zadatak: any;
  kategorijePodaci: Kategorija[] = [];
  zadaciPodaci : Zadatak[] = [];

  constructor(private route: ActivatedRoute, private http: HttpClient) {}

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
  }

  snimiZadatak() {
    this.http.post(`${MojConfig.adresa_servera}/Zadatak-dodaj`, this.novi_zadatak).subscribe(() => {
      alert("Zadatak dodan");
      this.novi_zadatak = null;
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
    this.http.get<any>(`${MojConfig.adresa_servera}/Kategorija-preuzmi`).subscribe((response: any) => {
      if (response && Array.isArray(response.kategorije)) {
        this.kategorijePodaci = response.kategorije;
      } else {
        console.error('Unexpected data format', response);
      }
    });
  }

  getZadaci(): void {
    this.http.get<any>(`${MojConfig.adresa_servera}/Zadatak-preuzmi`).subscribe((response: any) => {
      if (response && Array.isArray(response.zadaci)) {
        this.zadaciPodaci = response.zadaci;
      } else {
        console.error('Unexpected data format', response);
      }
    });
  }

  protected readonly MojConfig = MojConfig;
}
