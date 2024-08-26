import { Component, OnInit } from '@angular/core';
import {ActivatedRoute, Router} from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { MojConfig } from '../../moj-config';
import {JezikService} from "../../Services/jezik.service";
import { MatDialog } from '@angular/material/dialog';
import { ConfirmDialogComponent } from '../confirm-dialog/confirm-dialog.component';
import { ChangeDetectorRef } from '@angular/core';
interface Posao {
  posaoID: number;
  nazivZadatka: string;
  cijena: number;
  cijenaString:string;
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
  datumObjaveString: Date;
  ukljucenGPS: boolean;
  jeOdabran: boolean;
}

@Component({
  selector: 'app-posao-detalji',
  templateUrl: './posao-detalji.component.html',
  styleUrls: ['./posao-detalji.component.css']
})
export class PosaoDetaljiComponent implements OnInit {
  posao: Posao | null = null;
  showChatBox: boolean = false;



  constructor(private route: ActivatedRoute, private http: HttpClient, public jezikService: JezikService,
              public dialog: MatDialog,
              private router: Router,
              private cdr: ChangeDetectorRef
  ) {}

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    this.getPosaoById(Number(id)).subscribe((data: any) => {
      this.posao = data;
    });

    this.route.queryParams.subscribe(params => {
      if (params['fromHelp']) {
        this.showChatBox = true;
        setTimeout(() => {
          this.showChatBox = false;
        }, 5000);


        setTimeout(() => {
          window.scrollTo({
            top: document.body.scrollHeight,
            behavior: 'smooth'
          });
        }, 0);
      } else {
        this.showChatBox = false;
        window.scrollTo({
          top: 0,
          behavior: 'smooth'
        });
      }
    });
  }

  getPosaoById(id: number): Observable<Posao> {
    return this.http.get<Posao>(`${MojConfig.adresa_servera}/Posao-preuzmi/${id}`);
  }

  getZadatakSlikaURL(zadatakID: number): string {
    return `${MojConfig.adresa_servera}/Zadatak/slika?id=${zadatakID}`;
  }

  openConfirmDialog(): void {
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      width: '300px',
      data: {
        message: this.jezikService.isBosanski()
          ? 'Da li ste sigurni da želite odabrati posao?'
          : 'Are you sure you want to select this job?'
      }
    });

   dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.odaberiPosao();
      }
    });
  }

  odaberiPosao(): void {
    if (this.posao) {
      const posaoID = this.posao.posaoID ? parseInt(this.posao.posaoID.toString(), 10) : null;
      const odabraniPosao = {
        PosaoID: this.posao.posaoID,
        PosloprimaocID: 1,
      };


      this.http.post(`${MojConfig.adresa_servera}/OdabraniPosao-dodaj`, odabraniPosao)
        .subscribe(() => {
          this.http.post(`${MojConfig.adresa_servera}/Posao-update/odaberi/${posaoID}`, {})
            .subscribe(() => {
              this.getPosaoById(this.posao?.posaoID || 0).subscribe(updatedPosao => {
                this.posao = updatedPosao;
                this.cdr.detectChanges();
              });

              alert(this.jezikService.isBosanski()
                ? 'Uspješno ste odabrali posao!'
                : 'You have successfully selected the job!');
            });
        });
    }
  }



}
