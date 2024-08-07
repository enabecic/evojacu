import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class JezikService {
  private jeBosanski: boolean = true;

  setLanguage(bosanski: boolean) {
    this.jeBosanski = bosanski;
  }

  isBosanski(): boolean {
    return this.jeBosanski;
  }
}
