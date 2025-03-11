import { Component, Input, OnInit } from '@angular/core';
import { AuthService } from '../auth.service';

@Component({
  selector: 'app-korisnicki-profil',
  templateUrl: './korisnicki-profil.component.html',
  styleUrls: ['./korisnicki-profil.component.css']
})
export class KorisnickiProfilComponent implements OnInit {
  korisnik: any = {
    Ime: 'Edogaru',
    Prezime: 'User',
    Email: 'edogaru@mail.com.my',
    Telefon: '',
    Adresa: ''
  };

  constructor(private authService: AuthService) {}

  ngOnInit() {
    const prijavljeniKorisnik = this.authService.getTrenutniKorisnik();
    if (prijavljeniKorisnik) {
      this.korisnik = prijavljeniKorisnik;
    }
  }

  spasiProfil() {
    alert('Profil sačuvan!');
  }

  logout() {
    this.authService.logout();
    this.korisnik = null;
    alert('Odjavljeni ste!');
  }
}
