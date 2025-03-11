import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  constructor() {}

  // Simulacija prijave - pohrana podataka u localStorage
  login(korisnik: any) {
    localStorage.setItem('trenutniKorisnik', JSON.stringify(korisnik));
  }

  // Dohvatanje trenutno prijavljenog korisnika
  getTrenutniKorisnik() {
    const korisnik = localStorage.getItem('trenutniKorisnik');
    return korisnik ? JSON.parse(korisnik) : null;
  }

  // Odjava - brisanje podataka
  logout() {
    localStorage.removeItem('trenutniKorisnik');
  }
}
