import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { MojConfig } from '../../moj-config';
import { Observable } from "rxjs";
import {ActivatedRoute, Router} from "@angular/router";
import { JezikService } from "../../Services/jezik.service";
import {map} from "rxjs/operators";


interface Kategorija {
  kategorijaID: number;
  naziv: string;
  slika: string;
}

interface Grad {
  gradID: number;
  naziv: string;
}
interface Posao {
  posaoID: number;
  nazivZadatka: string;
  cijena: number;
  cijenaString:string;
  zadatakStraniID: number;
  gradID: number;
  datumObjave: Date;
}

interface Zadatak {
  zadatakId: number;
  naziv: string;
}

@Component({
  selector: 'app-pocetna',
  templateUrl: './pocetna.component.html',
  styleUrls: ['./pocetna.component.css'],

})
export class PocetnaComponent implements OnInit {
  kategorije: Kategorija[] = [];
  currentPage: number = 0;
  itemsPerPage: number = 5;

  poslovi: Posao[] = [];

  gradovi: Grad[] = [];
  zadaci: Zadatak[] = [];
  dodaj_posao:boolean =false;


  odabraniGradID: number = 1;
  odabraniZadatakID: number = 1;
  opisPosla: string = '';
  adresaPosla: string = '';
  cijenaPosla: number = 0;
  ukljucenGPS: boolean = false;



  validationMessages: string[] = [];
  today: string = new Date().toISOString().split('T')[0];
  odabraniDatum: string = '';

  showChatBox: boolean = false;
  showSecondChatBox: boolean = false;

  constructor(private http: HttpClient, private router: Router, public jezikService: JezikService,
              private route: ActivatedRoute) {}

  ngOnInit(): void {
    this.getKategorije();
    this.getPoslovi().subscribe((data: any) => {
      this.poslovi = this.filterAndSortPoslovi(data.poslovi);
    });
    this.getGradovi();
    this.getZadaci();

    this.route.queryParams.subscribe(params => {
      if (params['fromHelp']) {
        this.showChatBox = true;
        setTimeout(() => {
          this.showChatBox = false;
        }, 5000);


      } else {
        this.showChatBox = false;
        window.scrollTo({
          top: 0,
          behavior: 'smooth'
        });
      }

      if (params['fromHelp2']) {
        this.showSecondChatBox = true;


        setTimeout(() => {
          this.showSecondChatBox = false;
        }, 5000);

        setTimeout(() => {
          const pageHeight = document.documentElement.scrollHeight;
          window.scrollTo({
            top: pageHeight / 4,
            behavior: 'smooth'
          });
        }, 100);
      } else {
        this.showSecondChatBox = false;
        window.scrollTo({
          top: 0,
          behavior: 'smooth'
        });
      }


    });


  }




  getZadatakSlikaURL(zadatakID: number): string {
    return `${MojConfig.adresa_servera}/Zadatak/slika?id=${zadatakID}`;
  }

  getGradovi(): void {
    this.http.get<{ gradovi: Grad[] }>(`${MojConfig.adresa_servera}/Grad-preuzmi`).subscribe(response => {
      this.gradovi = response.gradovi;
    });
  }

  getZadaci(): void {
    this.http.get<{ zadaci: Zadatak[] }>(`${MojConfig.adresa_servera}/Zadatak-preuzmi`).subscribe(response => {
      this.zadaci = response.zadaci;
    });
  }
  getPoslovi(): Observable<any> {
    return this.http.get<any>(`${MojConfig.adresa_servera}/Posao-preuzmi`);
  }

  filterAndSortPoslovi(poslovi: Posao[]): Posao[] {
    return poslovi
      .map(posao => ({
        ...posao,
        datumObjaveDate: new Date(posao.datumObjave)
      }))
      .sort((a, b) => b.datumObjaveDate.getTime() - a.datumObjaveDate.getTime()) // Sortiraj po datumu objave, najnoviji prvo
      .slice(0, 9)
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

  navigateToPonuda(kategorijaID: number): void {
    this.router.navigate(['/ponuda'], { queryParams: { kategorijaID } });
  }


  dodajVrijemeIzvrsavanja(krajnjeVrijeme: string): Observable<number> {
    return this.http.post<{ vrijemeID: number }>(
      `${MojConfig.adresa_servera}/VrijemeIzvrsavanja-dodaj`,
      { krajnjeVr: krajnjeVrijeme }
    ).pipe(map(response => response.vrijemeID));
  }



  dodajPosao() {
    this.validationMessages = [];
    this.odabraniDatum = (document.querySelector('input[type="date"]') as HTMLInputElement).value;

    if (!this.opisPosla) {
      this.validationMessages.push('Opis posla je obavezan.');
    }

    if (!this.adresaPosla) {
      this.validationMessages.push('Adresa posla je obavezna.');
    }

    if (this.cijenaPosla < 0 || this.cijenaPosla > 1000) {
      this.validationMessages.push('Cijena mora biti između 0 i 1000.');
    }

    if (this.odabraniDatum < this.today) {
      this.validationMessages.push('Datum ne može biti stariji od današnjeg.');
    }

    if (this.validationMessages.length > 0) {
      return;
    }


    this.dodajVrijemeIzvrsavanja(this.odabraniDatum).subscribe(vrijemeIzvrsavanjaID => {
      const noviPosao = {
        VrijemeIzvrsavanjaID: vrijemeIzvrsavanjaID,
        GradID: this.odabraniGradID,
        FazaPoslaID: 1,
        OpisPosla: this.opisPosla,
        Adresa: this.adresaPosla,
        PoslodavacID: 1,
        ZadatakStraniID: this.odabraniZadatakID,
        Cijena: this.cijenaPosla,
        UkljucenGPS: this.ukljucenGPS
      };

      this.http.post(`${MojConfig.adresa_servera}/Posao-dodaj`, noviPosao).subscribe(response => {

        const successMessage = this.jezikService.isBosanski()
          ? 'Posao dodan'
          : 'Job added';

        alert(successMessage);


        this.opisPosla = '';
        this.adresaPosla = '';
        this.cijenaPosla = 0;
        this.ukljucenGPS = false;
        this.odabraniGradID = 1;
        this.odabraniZadatakID = 1;
        this.odabraniDatum = '';

        this.validationMessages = [];

        this.dodaj_posao = false;
        this.getPoslovi().subscribe((data: any) => {
          this.poslovi = this.filterAndSortPoslovi(data.poslovi);
        });
      });
    });
  }


}
