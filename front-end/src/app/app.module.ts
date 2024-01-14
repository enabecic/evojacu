import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';
import {RouterModule} from "@angular/router";
import { PocetnaComponent } from './pocetna/pocetna.component';
import { KorisnickiProfilComponent } from './korisnicki-profil/korisnicki-profil.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    PocetnaComponent,
    KorisnickiProfilComponent
  ],
  imports: [
    BrowserModule,
    RouterModule.forRoot([
      {path:'login', component:LoginComponent},
      {path:'pocetna', component:PocetnaComponent},
      {path:'korisnickiProfil', component:KorisnickiProfilComponent}


    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
