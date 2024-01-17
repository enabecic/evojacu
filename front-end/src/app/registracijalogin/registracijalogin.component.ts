import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { catchError, map, switchMap } from 'rxjs/operators';
import { of, Observable } from 'rxjs';

interface UserData {
  username: string;
  email: string;
  lozinka: string;
}

interface LoginData {
  email: string;
  lozinka: string;
}

@Component({
  selector: 'app-registracijalogin',
  templateUrl: './registracijalogin.component.html',
  styleUrls: ['./registracijalogin.component.css']
})
export class RegistracijaloginComponent {
  userData: UserData = {
    username: '',
    email: '',
    lozinka: ''
  };

  loginData: LoginData = {
    email: '',
    lozinka: ''
  };

  constructor(private http: HttpClient) {}

  register() {
    const registerUrl = 'https://localhost:7027/korisnik-dodaj';

    this.checkIfEmailAndUsernameExist(this.userData.email, this.userData.username)
      .pipe(
        switchMap(userExists => {
          if (userExists) {
            console.log('Registracija neuspjesna. Provjerite email/lozinku.');
            // Ovdje možete dodati odgovarajuću logiku (npr. prikazivanje poruke korisniku)
            return of(null);
          } else {
            return this.http.post(registerUrl, this.userData);
          }
        }),
        catchError(error => {
          console.error('Error checking email and username:', error);
          return of(null);
        })
      )
      .subscribe(response => {
        if (response) {
          console.log('Registracija uspjesna: ', response);
          // Ovdje možete dodati odgovarajuću logiku nakon uspješne registracije
        }
      }, error => {
        console.error('Registracija nije uspjela', error);
        // Ovdje možete dodati odgovarajuću logiku nakon neuspjele registracije
      });
  }

  checkIfEmailAndUsernameExist(email: string, username: string): Observable<boolean> {
    // Dohvati sve korisnike iz baze
    return this.http.get<{ korisnici: UserData[] }>('https://localhost:7027/korisnici-preuzmi')
      .pipe(
        map(response => {
          // Proveri tip odgovora i da li postoji niz korisnika
          if (!response || !response.korisnici || !Array.isArray(response.korisnici)) {
            console.error('Neispravan odgovor sa servera:', response);
            return false;
          }

          // Proveri postojanje e-maila i korisničkog imena u dobivenom nizu korisnika
          const users = response.korisnici;
          return users.some(user => user.email === email || user.username === username);
        })
      );
  }



  url = 'https://localhost:7027';

  login() {
    this.loginUser(this.loginData.email, this.loginData.lozinka)
      .pipe(
        switchMap(success => {
          if (success) {
            console.log('Prijavljivanje uspešno.');
            // Ovde možete dodati dodatnu logiku ili preusmeriti korisnika na drugu stranicu
          } else {
            console.log('Prijavljivanje nije uspelo. Proverite email/lozinku.');
            // Ovde možete dodati odgovarajuću logiku (npr. prikazivanje poruke korisniku)
          }
          return of(null);
        }),
        catchError(error => {
          console.error('Greška prilikom prijavljivanja:', error);
          return of(null);
        })
      )
      .subscribe();
  }

  loginUser(email: string, lozinka: string): Observable<boolean> {
    return this.http.get<boolean>(
      `${this.url}/korisnik-preuzmi?Email=${email}&Lozinka=${lozinka}`
    );
  }

}
