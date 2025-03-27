import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import {MojConfig} from "../../moj-config";

@Component({
  selector: 'app-korisnicki-profil',
  templateUrl: './korisnicki-profil.component.html',
  styleUrls: ['./korisnicki-profil.component.css']
})
export class KorisnickiProfilComponent implements OnInit {
  updateForm!: FormGroup;
  korisnik: any = {};
  korisnikId: number = 0;
  editMode: boolean = false;
  activeTab: string = 'profil';
  currentPage: number = 1; // Trenutna stranica
  totalPages: number = 0; // Ukupan broj stranica


  constructor(
    private fb: FormBuilder,
    private http: HttpClient,
    private router: Router
  ) {}

  ngOnInit(): void {
    const prijavljeniKorisnik = localStorage.getItem('trenutniKorisnik');
    if (prijavljeniKorisnik) {
      this.korisnik = JSON.parse(prijavljeniKorisnik);
      this.korisnikId = this.korisnik.korisnikID;
    }
    this.updateForm = this.fb.group({
      ime: [''],
      prezime: [''],
      zanimanje: [''],
      adresa: [''],
      telefon: ['']
    });

    this.ucitajProfil();
  }

  ucitajProfil() {
    this.http.get<any>(`https://localhost:7027/korisnik-preuzmi/${this.korisnikId}`).subscribe(
      (data) => {
        this.korisnik = data.korisnici[0];
        this.updateForm.patchValue({
          ime: data.ime,
          prezime: data.prezime,
          zanimanje: data.zanimanje,
          adresa: data.adresa,
          telefon: data.telefon
        });
      },
      (error) => {
        console.error('Greška pri učitavanju profila:', error);
      }
    );
  }

  urediProfil() {
    // Prvo, postavi editMode na true da omogući uređivanje
    this.editMode = true;

    // Ažuriraj formu sa trenutnim podacima korisnika
    this.updateForm.patchValue({
      ime: this.korisnik.ime,
      prezime: this.korisnik.prezime,
      zanimanje: this.korisnik.zanimanje,
      adresa: this.korisnik.adresa,
      telefon: this.korisnik.telefon
    });
  }

  loadPage(page: number) {
    if (page < 1) return; // Prevenira negativne stranice
    this.http.get<any>(`https://localhost:7027/Zadatak-preuzmi/${this.korisnikId}?page=${page}&pageSize=${this.pageSize}`).subscribe(response => {
      this.zadaci = response.zadaci;
      this.totalPages = response.brojStranica; // Ukupan broj stranica
      this.currentPage = page;
    });
  }
  zadaci : any;
  pageSize: number = 4;
  selectedFile : any;
  imgSrc : any;

  // Funkcija za selektovanje taba
  selectTab(tab: string) {
    this.activeTab = tab;
    if (tab === 'poslovi') {
      this.loadPage(1); // Učitaj prvi set poslova kad je aktivan tab 'poslovi'
    }
  }
  users = [
    { isActive: false }
  ];

  toggleAnimation(index: number): void {
    this.users[index].isActive = !this.users[index].isActive;
  }

  spasiProfil() {
    if (this.updateForm.invalid) {
      return;
    }

    const requestData: any = {};
    Object.keys(this.updateForm.controls).forEach((key) => {
      const originalValue = this.korisnik[key];
      const newValue = this.updateForm.get(key)?.value;
      if (newValue !== '' && newValue !== originalValue) {
        requestData[key] = newValue;
      }
    });

    if (Object.keys(requestData).length === 0 && !this.selectedFile) {
      alert('Nema izmjena.');
      return;
    }

    this.http.put<any>(`https://localhost:7027/api/Korisnik/${this.korisnikId}`, requestData).subscribe(
      (response) => {
        alert('Profil uspješno ažuriran!');
        this.korisnik = { ...this.korisnik, ...requestData };
        this.editMode = false;

        if (this.selectedFile) {
          this.uploadProfilePicture();
        }
      },
      (error) => {
        console.error('Greška pri ažuriranju profila:', error);
      }
    );
  }

  changeProfilePicture() {
    const fileInput = document.createElement('input');
    fileInput.type = 'file';
    fileInput.accept = 'image/png, image/jpeg';
    fileInput.click();

    fileInput.onchange = (event: any) => {
      const file = event.target.files[0];
      if (file) {
        this.selectedFile = file;
        this.previewImage(file);
      }
    };
  }

  previewImage(file: File): void {
    const reader = new FileReader();

    reader.onloadend = () => {
      this.imgSrc = reader.result as string;
      this.korisnik.slika = this.imgSrc;
    };

    if (file) {
      reader.readAsDataURL(file);
    }
  }

  uploadProfilePicture() {
    if (this.selectedFile) {
      console.log("slika: ", this.selectedFile)
      const formData = new FormData();
      formData.append('slika', this.selectedFile);

      const url = `https://localhost:7027/api/Slika/postavi-sliku/${this.korisnikId}`;

      this.http.post(url, formData).subscribe(
        (response: any) => {
          this.korisnik.slika = response.url;
          this.selectedFile = null;
        },
        (error) => {
          console.error('Greška prilikom ažuriranja slike profila:', error);
        }
      );
    }
  }

  odjaviSe() {
    localStorage.removeItem('token');
    this.router.navigate(['/login']);
  }

  protected readonly MojConfig = MojConfig;
}
