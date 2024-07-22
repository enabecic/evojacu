import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Router } from '@angular/router';
import { MojConfig } from '../moj-config';

interface Posao {
  posaoID: number;
  nazivZadatka: string;
  cijena: number;
}

@Component({
  selector: 'app-ponuda',
  templateUrl: './ponuda.component.html',
  styleUrls: ['./ponuda.component.css']
})
export class PonudaComponent implements OnInit {
  poslovi: Posao[] = [];
  filteredPoslovi: Posao[] = [];
  searchTerm: string = '';

  constructor(private http: HttpClient, private router: Router) {}

  ngOnInit(): void {
    this.getPoslovi().subscribe((data: any) => {
      this.poslovi = data.poslovi;
      this.filteredPoslovi = this.poslovi; // Initialize filtered list
    });
  }

  getPoslovi(): Observable<any> {
    return this.http.get<any>(`${MojConfig.adresa_servera}/Posao-preuzmi`);
  }

  navigateToDetails(id: number): void {
    this.router.navigate(['/posao-detalji', id]);
  }

  onSearchChange(): void {
    const searchTermLower = this.searchTerm.toLowerCase();
    if (searchTermLower === '') {
      this.filteredPoslovi = this.poslovi; // Prikaz svih poslova ako je pretraga prazna
    } else {
      this.filteredPoslovi = this.poslovi.filter(posao =>
        posao.nazivZadatka.toLowerCase().startsWith(searchTermLower)
      );
    }

  }

  clearSearch(): void {
    this.searchTerm = '';
    this.onSearchChange(); // Pozovite onSearchChange da ažurirate filtrirane rezultate
  }




}
