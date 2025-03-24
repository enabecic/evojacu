import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { catchError, map, switchMap } from 'rxjs/operators';
import { of, Observable } from 'rxjs';
import { Router } from '@angular/router';

interface UserData {
  username: string;
  email: string;
  lozinka: string;
  ime: string;
  prezime: string;
  zanimanje: string;
  adresa: string;
  telefon: string;
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
    lozinka: '',
    ime: '',
    prezime: '',
    zanimanje: '',
    adresa: '',
    telefon: ''
  };

  loginData: LoginData = {
    email: '',
    lozinka: ''
  };

  loginErrorMessage: string | null = null;
  registrationSuccessMessage: string | null = null;
  registrationErrorMessage: string | null = null;
  isRegistrationSuccessful: boolean = false;

  url = 'https://localhost:7027';

  constructor(private http: HttpClient, private router: Router) {}

  register() {
    if (!this.validateRegistrationData()) {
      this.registrationErrorMessage = 'Sva polja su obavezna!';
      return;
    }

    const registerUrl = `${this.url}/korisnik/dodaj`;

    this.checkIfEmailExists(this.userData.email).pipe(
      switchMap(emailExists => {
        if (emailExists) {
          this.registrationErrorMessage = 'Korisnik s ovim emailom već postoji.';
          return of(null);
        }
        return this.checkIfUsernameExists(this.userData.username);
      }),
      switchMap(usernameExists => {
        if (usernameExists) {
          this.registrationErrorMessage = 'Korisnik s ovim korisničkim imenom već postoji.';
          return of(null);
        }
        return this.http.post(registerUrl, this.userData);
      })
    ).subscribe({
      next: () => {
        this.registrationSuccessMessage = 'Registracija uspješna!';
        this.registrationErrorMessage = null;
        this.isRegistrationSuccessful = true;
        this.clearForm();

        setTimeout(() => {
          this.registrationSuccessMessage = null;
          this.isRegistrationSuccessful = false;
        }, 3000);
      },
      error: () => {
        this.registrationErrorMessage = 'Registracija nije uspjela. Pokušajte ponovo.';
      }
    });
  }

  login() {
    if (!this.loginData.email || !this.loginData.lozinka) {
      this.loginErrorMessage = 'Unesite email i lozinku.';
      return;
    }

    const loginUrl = `${this.url}/korisnik/prijava`;

    console.log("Šaljem zahtev na:", loginUrl);
    console.log("Podaci za prijavu:", this.loginData);

    this.http.post<{ success: boolean, korisnik: any }>(loginUrl, this.loginData).subscribe({
      next: (response) => {
        console.log("Odgovor od servera:", response);
        if (response.success) {
          localStorage.setItem('trenutniKorisnik', JSON.stringify(response.korisnik));

          if (response.korisnik.token) {
            localStorage.setItem('token', response.korisnik.token);
            console.log("JWT Token spremljen u localStorage:", response.korisnik.token);
          } else {
            console.error("GREŠKA: Backend nije poslao JWT token!");
          }

          this.router.navigate(['/pocetna']);
        } else {
          this.loginErrorMessage = 'Neispravni podaci za prijavu.';
        }
      },
      error: (error) => {
        console.error("Greška od servera:", error);
        this.loginErrorMessage = 'Greška prilikom prijavljivanja.';
      }
    });
  }


  validateRegistrationData(): boolean {
    return Object.values(this.userData).every(value => value.trim() !== '');
  }

  clearForm() {
    this.userData = {
      username: '',
      email: '',
      lozinka: '',
      ime: '',
      prezime: '',
      zanimanje: '',
      adresa: '',
      telefon: ''
    };
  }

  checkIfEmailExists(email: string): Observable<boolean> {
    return this.http.get<{ Exists: boolean }>(`${this.url}/korisnik/provjeri-email?email=${encodeURIComponent(email)}`)
      .pipe(map(response => response.Exists), catchError(() => of(false)));
  }

  checkIfUsernameExists(username: string): Observable<boolean> {
    return this.http.get<{ Exists: boolean }>(`${this.url}/korisnik/provjeri-username?username=${encodeURIComponent(username)}`)
      .pipe(map(response => response.Exists), catchError(() => of(false)));
  }

  isFormTouched: boolean = false;

  resetLoginMessages() {
    this.loginErrorMessage = null;
  }

  resetRegistrationMessages() {
    this.registrationErrorMessage = null;
    this.registrationSuccessMessage = null;
  }

}
