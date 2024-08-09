import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { MojConfig } from '../../moj-config';
import {JezikService} from "../../Services/jezik.service"; // Importujte MojConfig

interface Posao {
  posaoID: number;
  nazivZadatka: string;
  cijena: number;
  gradID: number;
  nazivGrada: string;
  vrijemeIzvrsavanjaID: number;
  krajVremena: Date;
  poslodavacID: number;
  userName: string;
  opisPosla: string;
  adresa: string;
  fazaPoslaID: number;
  nazivFazePosla: string;
  zadatakStraniID: number;
  datumObjave: Date;
  ukljucenGPS: boolean;
}

@Component({
  selector: 'app-posao-detalji',
  templateUrl: './posao-detalji.component.html',
  styleUrls: ['./posao-detalji.component.css']
})
export class PosaoDetaljiComponent implements OnInit {
  posao: Posao | undefined;

  constructor(private route: ActivatedRoute, private http: HttpClient, public jezikService: JezikService) {}

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    this.getPosaoById(Number(id)).subscribe((data: any) => {
      this.posao = data;
    });
  }

  getPosaoById(id: number): Observable<Posao> {
    return this.http.get<Posao>(`${MojConfig.adresa_servera}/Posao-preuzmi/${id}`);
  }

  getZadatakSlikaURL(zadatakID: number): string {
    return `${MojConfig.adresa_servera}/Zadatak/slika?id=${zadatakID}`;
  }
}
