import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { JezikService } from "../../Services/jezik.service";

@Component({
  selector: 'app-pomoc',
  templateUrl: './pomoc.component.html',
  styleUrls: ['./pomoc.component.css']
})
export class PomocComponent implements OnInit {

  private startTime: number = Date.now();

  constructor(private router: Router, public jezikService: JezikService) {}

  ngOnInit(): void {


  }



  navigateToPonuda() {
    this.router.navigate(['/ponuda'], { queryParams: { fromHelp: true } });
  }

  navigateToDetalji(id:number) {
    this.router.navigate(['/posao-detalji', id], { queryParams: { fromHelp: true } });
  }

  navigateToJezik() {
    this.router.navigate([''], { queryParams: { fromHelp: true } });
  }

  navigateToPocetna() {
    this.router.navigate(['/pocetna'], { queryParams: { fromHelp: true } });
  }

  navigateToPocetna2() {
    this.router.navigate(['/pocetna'], { queryParams: { fromHelp2: true } });
  }
}
