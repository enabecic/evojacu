import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { JezikService } from "../../Services/jezik.service";

@Component({
  selector: 'app-pomoc',
  templateUrl: './pomoc.component.html',
  styleUrls: ['./pomoc.component.css']
})
export class PomocComponent {

  constructor(private router: Router, public jezikService: JezikService) {}

  navigateToPonuda() {
    this.router.navigate(['/ponuda'], { queryParams: { fromHelp: true } });
  }

  navigateToDetalji(id:number) {
    this.router.navigate(['/posao-detalji', id], { queryParams: { fromHelp: true } });
  }

  navigateToJezik() {
    this.router.navigate([''], { queryParams: { fromHelp: true } });
  }
}
