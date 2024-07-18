import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

interface Posao {
  posaoID: number;
  nazivZadatka: string;
  cijena: number;
  // Dodajte ostale potrebne atribute
}

@Component({
  selector: 'app-ponuda',
  templateUrl: './ponuda.component.html',
  styleUrls: ['./ponuda.component.css']
})
export class PonudaComponent implements OnInit {
  poslovi: Posao[] = [];

  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    this.getPoslovi().subscribe((data: any) => {
      this.poslovi = data.poslovi;
    });
  }

  getPoslovi(): Observable<any> {
    return this.http.get<any>('https://localhost:7027/Posao-preuzmi');
  }
}
