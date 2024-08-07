import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Router } from '@angular/router';
import { MojConfig } from '../moj-config';
import { JezikService } from "../Services/jezik.service";

interface Posao {
  posaoID: number;
  nazivZadatka: string;
  cijena: number;
  zadatakStraniID: number;
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
  minPrice: number = 0;
  maxPrice: number = 1000;
  showFilterModal: boolean = false;

  constructor(private http: HttpClient, private router: Router, public jezikService: JezikService) {}

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
    this.filteredPoslovi = this.poslovi.filter(posao =>
      posao.nazivZadatka.toLowerCase().startsWith(searchTermLower) &&
      posao.cijena >= this.minPrice && posao.cijena <= this.maxPrice
    );
  }

  clearSearch(): void {
    this.searchTerm = '';
    this.onSearchChange(); // Update filtered results
  }

  openFilterModal(): void {
    this.showFilterModal = true;
  }

  closeFilterModal(): void {
    this.showFilterModal = false;
  }

  applyFilter(): void {
    this.onSearchChange(); // Apply both search term and filter
    this.closeFilterModal(); // Close the filter modal
  }

  getZadatakSlikaURL(zadatakID: number): string {
    return `${MojConfig.adresa_servera}/Zadatak/slika?id=${zadatakID}`;
  }

  clearFilter(): void {
    // Reset minPrice and maxPrice to default values
    this.minPrice = 0;
    this.maxPrice = 1000;
    this.searchTerm = '';

    // Fetch all jobs again
    this.getPoslovi().subscribe((data: any) => {
      this.poslovi = data.poslovi;
      this.filteredPoslovi = this.poslovi; // Reset filtered list
    });

    // Close the filter modal if it's open
    this.closeFilterModal();
  }

}


