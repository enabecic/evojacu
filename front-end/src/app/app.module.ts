import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';
import {RouterModule} from "@angular/router";
import { PocetnaComponent } from './pocetna/pocetna.component';
import { KorisnickiProfilComponent } from './korisnicki-profil/korisnicki-profil.component';
import { PotraznjaComponent } from './potraznja/potraznja.component';
import { PonudaComponent } from './ponuda/ponuda.component';
import {FormsModule} from "@angular/forms";
import { KategorijePoslovaComponent } from './kategorije-poslova/kategorije-poslova.component';
import {HttpClientModule} from "@angular/common/http";
import { RegistracijaloginComponent } from './registracijalogin/registracijalogin.component';
import { PosaoDetaljiComponent } from './posao-detalji/posao-detalji.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    PocetnaComponent,
    KorisnickiProfilComponent,
    PotraznjaComponent,
    PonudaComponent,
    KategorijePoslovaComponent,
    RegistracijaloginComponent,
    PosaoDetaljiComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    RouterModule.forRoot([
      {path: 'login', component: LoginComponent},
      {path: 'pocetna', component: PocetnaComponent},
      {path: 'korisnickiProfil', component: KorisnickiProfilComponent},
      {path: 'potraznja', component: PotraznjaComponent},
      {path: 'ponuda', component: PonudaComponent},
      {path: 'kategorijePoslova', component: KategorijePoslovaComponent},
      {path: 'registracijalogin' , component: RegistracijaloginComponent},
      { path: 'posao-detalji/:id', component: PosaoDetaljiComponent },


    ]),
    FormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
