import {Component, OnInit} from '@angular/core';
import {Router} from "@angular/router";
import {JezikService} from "./Services/jezik.service";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'front-end';
  constructor(public router:Router, public jezikService: JezikService) {
  }

  ngOnInit() {

    const storedLanguage = localStorage.getItem('selectedLanguage');
    if (storedLanguage) {
      this.jezikService.setLanguage(storedLanguage === 'true');
    }
  }

  idi(s:string){
    this.router.navigate([s])
  }

}
