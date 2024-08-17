import { NgModule} from '@angular/core';

import { BrowserModule } from '@angular/platform-browser';
import { AppComponent } from './app.component';
import {RouterModule} from "@angular/router";
import { PocetnaComponent } from './Components/pocetna/pocetna.component';
import { KorisnickiProfilComponent } from './Components/korisnicki-profil/korisnicki-profil.component';
import { PonudaComponent } from './Components/ponuda/ponuda.component';
import {FormsModule} from "@angular/forms";
import { KategorijePoslovaComponent } from './Components/kategorije-poslova/kategorije-poslova.component';
import {HttpClientModule} from "@angular/common/http";
import { RegistracijaloginComponent } from './Components/registracijalogin/registracijalogin.component';
import { PosaoDetaljiComponent } from './Components/posao-detalji/posao-detalji.component';
import { KategorijaDodajComponent } from './Components/Kategorija/kategorija-dodaj/kategorija-dodaj.component';
import { ZadatakComponent } from './Components/Zadatak/zadatak/zadatak.component';
import { JezikComponent } from './Components/jezik/jezik.component';
import { ConfirmDialogComponent } from './Components/confirm-dialog/confirm-dialog.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatDialogModule } from '@angular/material/dialog';



@NgModule({
  declarations: [
    AppComponent,
    PocetnaComponent,
    KorisnickiProfilComponent,
    PonudaComponent,
    KategorijePoslovaComponent,
    RegistracijaloginComponent,
    PosaoDetaljiComponent,
    KategorijaDodajComponent,
    ZadatakComponent,
    JezikComponent,
    ConfirmDialogComponent,

  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    RouterModule.forRoot([
      {path: '' , component: JezikComponent},
      {path: 'pocetna', component: PocetnaComponent},
      {path: 'korisnickiProfil', component: KorisnickiProfilComponent},
      {path: 'ponuda', component: PonudaComponent},
      {path: 'kategorijePoslova', component: KategorijePoslovaComponent},
      {path: 'registracijalogin' , component: RegistracijaloginComponent},
      { path: 'posao-detalji/:id', component: PosaoDetaljiComponent },
      {path: 'kategorija-dodaj' , component: KategorijaDodajComponent},
      {path: 'zadatak' , component: ZadatakComponent},



    ]),
    FormsModule,
    BrowserAnimationsModule,
    MatDialogModule
  ],
  providers: [

  ],
  bootstrap: [AppComponent],
})
export class AppModule { }
