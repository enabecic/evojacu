import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { MojConfig } from '../../../moj-config';
import {JezikService} from "../../../Services/jezik.service";

interface Kategorija {
  kategorijaID: number;
  naziv: string;
  timestamp?: number;
}

@Component({
  selector: 'app-kategorija-dodaj',
  templateUrl: './kategorija-dodaj.component.html',
  styleUrls: ['./kategorija-dodaj.component.css']
})
export class KategorijaDodajComponent implements OnInit {

  kategorije: Kategorija[] = [];
  nova_kategorija: any;
  nazivInvalid: boolean = false;

  constructor(private route: ActivatedRoute, private http: HttpClient, public jezikService: JezikService) {}

  ngOnInit(): void {
    this.getKategorije();
  }

  novaKategorija() {
    this.nova_kategorija = {
      kategorijaID: 0,
      naziv: '',
      slika_base64_format: ''
    };
    this.nazivInvalid = false;
  }

  snimiKategoriju() {
    if (!this.nova_kategorija.naziv) {
      this.nazivInvalid = true;
      return;
    }

    const url = this.nova_kategorija.kategorijaID === 0
      ? `${MojConfig.adresa_servera}/Kategorija-dodaj`
      : `${MojConfig.adresa_servera}/Kategorija-update`;

    this.http.post(url, this.nova_kategorija).subscribe((x: any) => {
      const successMessage = this.jezikService.isBosanski()
        ? 'Kategorija snimljena'
        : 'Category saved';

      alert(successMessage);
      this.getKategorije();
    });

    this.nova_kategorija = null;
  }

  generisi_preview() {
    var file = (document.getElementById("slika-input") as HTMLInputElement).files?.[0];
    if (file && this.nova_kategorija) {
      var reader = new FileReader();
      reader.onload = () => {
        this.nova_kategorija.slika_base64_format = reader.result?.toString();
      }
      reader.readAsDataURL(file);
    }
  }

  getKategorije(): void {
    this.http.get<{ kategorije: Kategorija[] }>(`${MojConfig.adresa_servera}/Kategorija-preuzmi`).subscribe(response => {
      this.kategorije = response.kategorije.map(kat => ({
        ...kat,
        timestamp: Date.now()
      }));
    });
  }

  obrisiKategoriju(kategorijaID: number): void {

    const confirmMessage = this.jezikService.isBosanski()
      ? 'Da li ste sigurni da želite obrisati ovu kategoriju?'
      : 'Are you sure you want to delete this category?';

    const successMessage = this.jezikService.isBosanski()
      ? 'Kategorija obrisan'
      : 'Category deleted';

    const errorMessage = this.jezikService.isBosanski()
      ? 'Greška prilikom brisanja kategorije'
      : 'Error while deleting the category';

    if (confirm(confirmMessage)) {
      this.http.delete(`${MojConfig.adresa_servera}/Kategorija-obrisi`, { params: { KategorijaID: kategorijaID.toString() } })
        .subscribe(() => {
          this.getKategorije();
          alert(successMessage);
        }, error => {
          alert(errorMessage);
        });
    }
  }

  protected readonly MojConfig = MojConfig;

  urediKategoriju(item: Kategorija) {
    this.nova_kategorija = {
      kategorijaID: item.kategorijaID,
      naziv: item.naziv,
      slika_base64_format: ''
    };
    this.nazivInvalid = false;
  }

  onNazivChange() {
    this.nazivInvalid = !this.nova_kategorija.naziv;
  }
}
