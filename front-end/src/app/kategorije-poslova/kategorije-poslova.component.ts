import {Component, OnInit} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {KategorijaPreuzmiResponse, KategorijaPreuzmiResponseKategorija} from "./kategorija-preuzmi-response";
import {MojConfig} from "../moj-config";

@Component({
  selector: 'app-kategorije-poslova',
  templateUrl: './kategorije-poslova.component.html',
  styleUrls: ['./kategorije-poslova.component.css']
})
export class KategorijePoslovaComponent implements OnInit{
constructor(public httpClient:HttpClient) {
}
  kategorije:KategorijaPreuzmiResponseKategorija[]=[];
  ngOnInit(): void {
    let url=MojConfig.adresa_servera+`/Kategorija-preuzmi`
    this.httpClient.get<KategorijaPreuzmiResponse>(url).subscribe((x:KategorijaPreuzmiResponse)=>{
      this.kategorije=x.kategorije;
    })
  }







}
