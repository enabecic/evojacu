import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { MojConfig } from '../moj-config';
import { Observable } from "rxjs";
import { Router } from "@angular/router";
import { JezikService } from "../Services/jezik.service";

interface Kategorija {
  kategorijaID: number;
  naziv: string;
  slika: string;
}

interface Posao {
  posaoID: number;
  nazivZadatka: string;
  cijena: number;
  zadatakStraniID: number;
  gradID: number;
  datumObjave: Date;
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

  poslovi: Posao[] = [];

  constructor(private http: HttpClient, private router: Router, public jezikService: JezikService) {}

  ngOnInit(): void {
    this.getKategorije();
    this.getPoslovi().subscribe((data: any) => {
      this.poslovi = this.filterAndSortPoslovi(data.poslovi);
    });
  }

  getZadatakSlikaURL(zadatakID: number): string {
    return `${MojConfig.adresa_servera}/Zadatak/slika?id=${zadatakID}`;
  }

  getPoslovi(): Observable<any> {
    return this.http.get<any>(`${MojConfig.adresa_servera}/Posao-preuzmi`);
  }

  filterAndSortPoslovi(poslovi: Posao[]): Posao[] {
    return poslovi
      .sort((a, b) => new Date(b.datumObjave).getTime() - new Date(a.datumObjave).getTime()) // Sortiraj po datumu objave, najnoviji prvo
      .slice(0, 9); // Uzmi prvih 9 poslova
  }

  navigateToDetails(id: number): void {
    this.router.navigate(['/posao-detalji', id]);
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
