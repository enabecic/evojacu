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
  timestamp?: number;
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

  trenutnaStranica: number = 1;
  brojStranica: number = 0;
  pageSize: number = 4;

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
      const successMessage = this.jezikService.isBosanski()
        ? 'Zadatak snimljen'
        : 'Task saved';

      alert(successMessage);
      this.novi_zadatak = null;
      this.getZadaci();
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
    const params = {
      PageNumber: this.trenutnaStranica.toString(),
      PageSize: this.pageSize.toString()
    };

    this.http.get<{ zadaci: Zadatak[], ukupanBrojZadataka: number, brojStranica: number }>(`${MojConfig.adresa_servera}/Zadatak-preuzmi/paged`, { params })
      .subscribe(response => {
        this.zadaciPodaci = response.zadaci.map(zadatak => ({
          ...zadatak,
          timestamp: Date.now() // Assign the current timestamp
        }));
        this.brojStranica = response.brojStranica;
      });
  }


  promijeniStranicu(stranica: number): void {
    if (stranica > 0 && stranica <= this.brojStranica) {
      this.trenutnaStranica = stranica;
      this.getZadaci();
    }
  }

  obrisiZadatak(zadatakId: number): void {
    const confirmMessage = this.jezikService.isBosanski()
      ? 'Da li ste sigurni da želite obrisati ovaj zadatak?'
      : 'Are you sure you want to delete this task?';

    const successMessage = this.jezikService.isBosanski()
      ? 'Zadatak obrisan'
      : 'Task deleted';

    const errorMessage = this.jezikService.isBosanski()
      ? 'Greška prilikom brisanja zadatka'
      : 'Error while deleting the task';

    if (confirm(confirmMessage)) {
      this.http.delete(`${MojConfig.adresa_servera}/Zadatak-obrisi`, { params: { ZadatakID: zadatakId.toString() } })
        .subscribe(() => {
          this.getZadaci(); // Osvježavanje liste zadataka nakon brisanja
          alert(successMessage);
        }, error => {
          alert(errorMessage);
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
