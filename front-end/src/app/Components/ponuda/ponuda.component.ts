import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import {ActivatedRoute, Router} from '@angular/router';
import { MojConfig } from '../../moj-config';
import { JezikService } from "../../Services/jezik.service";

interface Posao {
  posaoID: number;
  nazivZadatka: string;
  cijena: number;
  zadatakStraniID: number;
  gradID: number;
}

interface Grad {
  gradID: number;
  naziv: string;
}



interface Kategorija {
  kategorijaID: number;
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
  selectedGradID: number = 2;
  showFilterModal: boolean = false;
  nazivKategorije: string = 'Sve kategorije';
  kategorije: Kategorija[] = [];
  constructor(private http: HttpClient, private router: Router, private route: ActivatedRoute, public jezikService: JezikService) {}

  ngOnInit(): void {
    this.route.queryParams.subscribe(params => {
      const kategorijaID = params['kategorijaID'];
      if (kategorijaID) {
        this.getPosloviByKategorija(kategorijaID).subscribe((data: any) => {
          this.poslovi = data.poslovi;
          this.filteredPoslovi = this.poslovi;
          this.getKategorijaNaziv(kategorijaID);
        });
      } else {
        this.getPoslovi().subscribe((data: any) => {
          this.poslovi = data.poslovi;
          this.filteredPoslovi = this.poslovi;
          this.nazivKategorije = '';
        });
      }
    });
    this.getGradovi();
  }

  getPoslovi(): Observable<any> {
    return this.http.get<any>(`${MojConfig.adresa_servera}/Posao-preuzmi`);
  }

  getPosloviByKategorija(kategorijaID: number): Observable<any> {
    return this.http.get<any>(`${MojConfig.adresa_servera}/Posao-preuzmi?kategorijaID=${kategorijaID}`);
  }

  getKategorijaNaziv(kategorijaID: number): void {
    this.http.get<{ kategorije: Kategorija[] }>(`${MojConfig.adresa_servera}/Kategorija-preuzmi`)
      .subscribe(response => {


        const kategorija = response.kategorije.find(k => +k.kategorijaID === +kategorijaID);

        if (kategorija) {
          this.nazivKategorije = kategorija.naziv;

        } else {
          this.nazivKategorije = 'Kategorija nije pronađena';
        }
      }, error => {
        console.error('Greška prilikom preuzimanja kategorije:', error);
        this.nazivKategorije = 'Greška pri preuzimanju podataka';
      });
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

    this.filteredPoslovi = this.poslovi.filter(posao => {
      const matchesNaziv = !this.searchTerm || posao.nazivZadatka.toLowerCase().includes(searchTermLower);
      const matchesCijena = (this.minPrice === 0 && this.maxPrice === 1000) || (posao.cijena >= this.minPrice && posao.cijena <= this.maxPrice);
      const matchesGrad = this.selectedGradID === 2 || posao.gradID === this.selectedGradID;

      return matchesNaziv && matchesCijena && matchesGrad;
    });

    this.closeFilterModal();
  }

  getZadatakSlikaURL(zadatakID: number): string {
    return `${MojConfig.adresa_servera}/Zadatak/slika?id=${zadatakID}`;
  }

  clearFilter(): void {

    this.minPrice = 0;
    this.maxPrice = 1000;
    this.selectedGradID = 2;
    this.searchTerm = '';


    this.getPoslovi().subscribe((data: any) => {
      this.poslovi = data.poslovi;
      this.applyFilter();
    });


    this.closeFilterModal();
  }

  onGradChange(event: Event): void {
    const selectElement = event.target as HTMLSelectElement;
    this.selectedGradID = +selectElement.value; // Ažurirajte selectedGradID
    console.log(`Grad changed to: ${this.selectedGradID}`); // Debug log
  }


}
