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
  isFormTouched: boolean = false; // New variable to track form touch status

  constructor(private http: HttpClient, private router: Router) {}

  register() {
    const registerUrl = `${this.url}/korisnik/dodaj`;

    this.checkIfEmailExists(this.userData.email).pipe(
      switchMap(emailExists => {
        if (emailExists) {
          this.registrationErrorMessage = 'Korisnik s ovim emailom već postoji.';
          this.registrationSuccessMessage = null;
          this.isRegistrationSuccessful = false;
          return of(null);
        }
        return this.checkIfUsernameExists(this.userData.username);
      }),
      switchMap(usernameExists => {
        if (usernameExists) {
          this.registrationErrorMessage = 'Korisnik s ovim korisničkim imenom već postoji.';
          this.registrationSuccessMessage = null;
          this.isRegistrationSuccessful = false;
          return of(null);
        }
        return this.http.post(registerUrl, this.userData).pipe(
          map(response => {
            this.registrationErrorMessage = null;
            this.registrationSuccessMessage = 'Registracija uspješna!';
            this.isRegistrationSuccessful = true;
            this.clearForm();
            this.resetFormErrors();

            setTimeout(() => {
              this.registrationSuccessMessage = null;
              this.isRegistrationSuccessful = false;
            }, 3000);

            return response;
          }),
          catchError(error => {
            this.registrationErrorMessage = 'Registracija nije uspjela. Pokušajte ponovo.';
            this.registrationSuccessMessage = null;
            this.isRegistrationSuccessful = false;
            return of(null);
          })
        );
      }),
      catchError(error => {
        this.registrationErrorMessage = 'Greška prilikom provjere emaila ili korisničkog imena.';
        this.registrationSuccessMessage = null;
        this.isRegistrationSuccessful = false;
        return of(null);
      })
    ).subscribe();
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

  resetFormErrors() {
    this.isFormTouched = false; // Reset form touch status
  }

  resetRegistrationMessages(): void {
    if (!this.registrationSuccessMessage) {
      this.registrationErrorMessage = null;
    }
  }

  resetLoginMessages(): void {
    this.loginErrorMessage = null;
  }

  checkIfEmailExists(email: string): Observable<boolean> {
    const checkEmailUrl = `${this.url}/korisnik/provjeri-email?email=${encodeURIComponent(email)}`;
    return this.http.get<{ Exists: boolean }>(checkEmailUrl).pipe(
      map(response => response.Exists),
      catchError(error => {
        console.error('Greška prilikom provjere emaila:', error);
        return of(false);
      })
    );
  }

  checkIfUsernameExists(username: string): Observable<boolean> {
    const checkUsernameUrl = `${this.url}/korisnik/provjeri-username?username=${encodeURIComponent(username)}`;
    return this.http.get<{ Exists: boolean }>(checkUsernameUrl).pipe(
      map(response => response.Exists),
      catchError(error => {
        console.error('Greška prilikom provjere korisničkog imena:', error);
        return of(false);
      })
    );
  }

  login() {
    const loginUrl = `${this.url}/korisnik/prijava`;

    this.loginUser(this.loginData).pipe(
      switchMap(success => {
        if (success) {
          this.loginErrorMessage = null;
          this.router.navigate(['/pocetna']);
        } else {
          this.loginErrorMessage = 'Prijavljivanje nije uspjelo. Proverite email/lozinku.';
        }
        return of(null);
      }),
      catchError(error => {
        this.loginErrorMessage = 'Greška prilikom prijavljivanja.';
        return of(null);
      })
    ).subscribe();
  }

  loginUser(loginData: LoginData): Observable<boolean> {
    const loginUrl = `${this.url}/korisnik/prijava`;
    return this.http.post<{ success: boolean }>(loginUrl, loginData).pipe(
      map(response => response.success),
      catchError(error => {
        console.error('Greška prilikom prijavljivanja:', error);
        return of(false);
      })
    );
  }

  url = 'https://localhost:7027';
}
