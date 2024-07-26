import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { MojConfig } from '../../../moj-config';

interface Kategorija {
  kategorijaID: number;
  naziv: string;
}

@Component({
  selector: 'app-kategorija-dodaj',
  templateUrl: './kategorija-dodaj.component.html',
  styleUrls: ['./kategorija-dodaj.component.css']
})
export class KategorijaDodajComponent implements OnInit {

  kategorije: Kategorija[] = [];
  nova_kategorija: any;

  constructor(private route: ActivatedRoute, private http: HttpClient) {}

  ngOnInit(): void {
    this.getKategorije();
  }

  novaKategorija() {
    this.nova_kategorija = {
      kategorijaID: 0,
      naziv: '',
      slika_base64_format: ''
    };
  }

  snimiKategoriju() {
    console.log("prije dodavanja");
    this.http.post("https://localhost:7027/Kategorija-dodaj", this.nova_kategorija).subscribe((x: any) => {
      alert("uredu");
      console.log("poslije dodavanja");
      this.getKategorije(); // osveži listu kategorija nakon dodavanja nove
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

  protected readonly MojConfig = MojConfig;

  getKategorije(): void {
    this.http.get<{ kategorije: Kategorija[] }>(`${MojConfig.adresa_servera}/Kategorija-preuzmi`).subscribe(response => {
      console.log(response);
      this.kategorije = response.kategorije;
    });
  }
}
