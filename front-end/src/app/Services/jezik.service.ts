import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class JezikService {
  private readonly LANGUAGE_KEY = 'selectedLanguage';

  constructor() {
    // Initialize language from localStorage
    const storedLanguage = localStorage.getItem(this.LANGUAGE_KEY);
    this.jeBosanski = storedLanguage === 'true'; // Default to Bosanski if not set
  }

  private jeBosanski: boolean = true;

  setLanguage(bosanski: boolean) {
    this.jeBosanski = bosanski;
    localStorage.setItem(this.LANGUAGE_KEY, bosanski.toString());
  }

  isBosanski(): boolean {
    return this.jeBosanski;
  }
}
