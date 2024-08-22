import { Component } from '@angular/core';
import { Router } from '@angular/router';
import {JezikService} from "../../Services/jezik.service";


@Component({
  selector: 'app-jezik',
  templateUrl: './jezik.component.html',
  styleUrls: ['./jezik.component.css']
})
export class JezikComponent {
  constructor(private router: Router, private jezikService: JezikService) {}

  setLanguage(bosanski: boolean) {
    this.jezikService.setLanguage(bosanski);
    this.router.navigate(['/pocetna']);
  }
}
