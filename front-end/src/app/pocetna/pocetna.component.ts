import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { MojConfig } from '../moj-config';

interface Kategorija {
  kategorijaID: number;
  naziv: string;
  slika: string;
}

@Component({
  selector: 'app-pocetna',
  templateUrl: './pocetna.component.html',
  styleUrls: ['./pocetna.component.css']
})
export class PocetnaComponent implements OnInit {
  kategorije: Kategorija[] = [];
  currentPage: number = 0;
  itemsPerPage: number = 5;

  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    this.getKategorije();
  }

  get currentKategorije(): Kategorija[] {
    const start = this.currentPage * this.itemsPerPage;
    const end = start + this.itemsPerPage;
    return this.kategorije.slice(start, end);
  }

  getKategorije(): void {
    this.http.get<{ kategorije: Kategorija[] }>(`${MojConfig.adresa_servera}/Kategorija-preuzmi`).subscribe(response => {
      this.kategorije = response.kategorije.map(kat => ({
        ...kat,
        slika: `${MojConfig.adresa_servera}/Kategorija/slika?id=${kat.kategorijaID}`
      }));
    });
  }

  prevPage(): void {
    if (this.currentPage > 0) {
      this.currentPage--;
    }
  }

  nextPage(): void {
    if ((this.currentPage + 1) * this.itemsPerPage < this.kategorije.length) {
      this.currentPage++;
    }
  }
}
