import { Component } from '@angular/core';
import {Router} from "@angular/router";
import {JezikService} from "./Services/jezik.service";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'front-end';
  constructor(public router:Router, public jezikService: JezikService) {
  }


  idi(s:string){
    this.router.navigate([s])
  }

}
