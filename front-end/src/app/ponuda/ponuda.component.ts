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
  gradID: number; // Ensure this is correctly mapped from the backend
}

interface Grad {
  gradID: number;
  naziv: string;
}

@Component({
  selector: 'app-ponuda',
  templateUrl: './ponuda.component.html',
  styleUrls: ['./ponuda.component.css']
})
export class PonudaComponent implements OnInit {
  poslovi: Posao[] = [];
  filteredPoslovi: Posao[] = [];
  gradovi: Grad[] = [];
  searchTerm: string = '';
  minPrice: number = 0;
  maxPrice: number = 1000;
  selectedGradID: number = 2; // Default to 1 (city with ID 1)
  showFilterModal: boolean = false;

  constructor(private http: HttpClient, private router: Router, public jezikService: JezikService) {}

  ngOnInit(): void {
    this.getPoslovi().subscribe((data: any) => {
      this.poslovi = data.poslovi;
      this.filteredPoslovi = this.poslovi; // Initialize filtered list
    });
    this.getGradovi();
  }

  getPoslovi(): Observable<any> {
    return this.http.get<any>(`${MojConfig.adresa_servera}/Posao-preuzmi`);
  }

  getGradovi(): void {
    this.http.get<{ gradovi: Grad[] }>(`${MojConfig.adresa_servera}/Grad-preuzmi`).subscribe(response => {
      this.gradovi = response.gradovi;
    });
  }

  navigateToDetails(id: number): void {
    this.router.navigate(['/posao-detalji', id]);
  }

  onSearchChange(): void {
    this.applyFilter();
  }

  clearSearch(): void {
    this.searchTerm = '';
    this.applyFilter(); // Update filtered results
  }

  openFilterModal(): void {
    this.showFilterModal = true;
  }

  closeFilterModal(): void {
    this.showFilterModal = false;
  }

  applyFilter(): void {
    const searchTermLower = this.searchTerm.toLowerCase();
    this.filteredPoslovi = this.poslovi.filter(posao =>
      posao.nazivZadatka.toLowerCase().includes(searchTermLower) &&
      posao.cijena >= this.minPrice && posao.cijena <= this.maxPrice &&
      posao.gradID === this.selectedGradID
    );
    this.closeFilterModal(); // Close the filter modal
  }

  getZadatakSlikaURL(zadatakID: number): string {
    return `${MojConfig.adresa_servera}/Zadatak/slika?id=${zadatakID}`;
  }

  clearFilter(): void {
    // Reset filter values to default
    this.minPrice = 0;
    this.maxPrice = 1000;
    this.selectedGradID = 1;
    this.searchTerm = '';

    // Fetch all jobs again
    this.getPoslovi().subscribe((data: any) => {
      this.poslovi = data.poslovi;
      this.filteredPoslovi = this.poslovi; // Reset filtered list
    });

    // Close the filter modal if it's open
    this.closeFilterModal();
  }

  onGradChange(event: Event): void {
    const selectElement = event.target as HTMLSelectElement;
    this.selectedGradID = +selectElement.value; // Ažurirajte selectedGradID
    console.log(`Grad changed to: ${this.selectedGradID}`); // Debug log
  }


}
