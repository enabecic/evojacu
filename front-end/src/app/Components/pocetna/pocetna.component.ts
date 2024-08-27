import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { MojConfig } from '../../moj-config';
import { Observable } from "rxjs";
import {ActivatedRoute, Router} from "@angular/router";
import { JezikService } from "../../Services/jezik.service";
import {map} from "rxjs/operators";


interface Kategorija {
  kategorijaID: number;
  naziv: string;
  slika: string;
}

interface Grad {
  gradID: number;
  naziv: string;
}
interface Posao {
  posaoID: number;
  nazivZadatka: string;
  cijena: number;
  cijenaString:string;
  zadatakStraniID: number;
  gradID: number;
  datumObjave: Date;
}

interface Zadatak {
  zadatakId: number;
  naziv: string;
}

@Component({
  selector: 'app-pocetna',
  templateUrl: './pocetna.component.html',
  styleUrls: ['./pocetna.component.css'],

})
export class PocetnaComponent implements OnInit {
  kategorije: Kategorija[] = [];
  currentPage: number = 0;
  itemsPerPage: number = 5;

  poslovi: Posao[] = [];

  gradovi: Grad[] = [];
  zadaci: Zadatak[] = [];
  dodaj_posao:boolean =false;


  odabraniGradID: number = 1;
  odabraniZadatakID: number = 1;
  opisPosla: string = '';
  adresaPosla: string = '';
  cijenaPosla: number = 0;
  ukljucenGPS: boolean = false;



  validationMessages: string[] = [];
  today: string = new Date().toISOString().split('T')[0];
  odabraniDatum: string = '';

  showChatBox: boolean = false;
  showSecondChatBox: boolean = false;


  showCanvas:boolean=true;
  constructor(private http: HttpClient, private router: Router, public jezikService: JezikService,
              private route: ActivatedRoute) {}

  ngOnInit(): void {
    this.getKategorije();
    this.getPoslovi().subscribe((data: any) => {
      this.poslovi = this.filterAndSortPoslovi(data.poslovi);
    });
    this.getGradovi();
    this.getZadaci();

    this.route.queryParams.subscribe(params => {
      if (params['fromHelp']) {


        setTimeout(() => {
          this.showChatBox = true;

          this.initializeWebGL();
          setTimeout(()=>{
            this.showCanvas=false;

            setTimeout(()=>{
              this.showChatBox = false;

            },3000)
          },2500)
        }, 1000);


      } else {
        this.showChatBox = false;
        window.scrollTo({
          top: 0,
          behavior: 'smooth'
        });
      }

      if (params['fromHelp2']) {
        this.showSecondChatBox = true;


        setTimeout(() => {
          this.showSecondChatBox = false;
        }, 5000);

        setTimeout(() => {
          const pageHeight = document.documentElement.scrollHeight;
          window.scrollTo({
            top: pageHeight / 4,
            behavior: 'smooth'
          });
        }, 100);
      } else {
        this.showSecondChatBox = false;
        window.scrollTo({
          top: 0,
          behavior: 'smooth'
        });
      }
    });

  }



  getZadatakSlikaURL(zadatakID: number): string {
    return `${MojConfig.adresa_servera}/Zadatak/slika?id=${zadatakID}`;
  }

  getGradovi(): void {
    this.http.get<{ gradovi: Grad[] }>(`${MojConfig.adresa_servera}/Grad-preuzmi`).subscribe(response => {
      this.gradovi = response.gradovi;
    });
  }

  getZadaci(): void {
    this.http.get<{ zadaci: Zadatak[] }>(`${MojConfig.adresa_servera}/Zadatak-preuzmi`).subscribe(response => {
      this.zadaci = response.zadaci;
    });
  }
  getPoslovi(): Observable<any> {
    return this.http.get<any>(`${MojConfig.adresa_servera}/Posao-preuzmi`);
  }

  filterAndSortPoslovi(poslovi: Posao[]): Posao[] {
    return poslovi
      .map(posao => ({
        ...posao,
        datumObjaveDate: new Date(posao.datumObjave)
      }))
      .sort((a, b) => b.datumObjaveDate.getTime() - a.datumObjaveDate.getTime()) // Sortiraj po datumu objave, najnoviji prvo
      .slice(0, 9)
  }



  navigateToDetails(id: number): void {
    this.router.navigate(['/posao-detalji', id]);
  }

  get currentKategorije(): Kategorija[] {
    const start = this.currentPage * this.itemsPerPage;
    const end = start + this.itemsPerPage;
    return this.kategorije.slice(start, end);
  }

  getKategorije(): void {
    this.http.get<{ kategorije: Kategorija[] }>(`${MojConfig.adresa_servera}/Kategorija-preuzmi`).subscribe(response => {
      this.kategorije = response.kategorije.map(kat => ({
        ...kat,
        slika: `${MojConfig.adresa_servera}/Kategorija/slika?id=${kat.kategorijaID}`
      }));
    });
  }

  prevPage(): void {
    if (this.currentPage > 0) {
      this.currentPage--;
    }
  }

  nextPage(): void {
    if ((this.currentPage + 1) * this.itemsPerPage < this.kategorije.length) {
      this.currentPage++;
    }
  }

  navigateToPonuda(kategorijaID: number): void {
    this.router.navigate(['/ponuda'], { queryParams: { kategorijaID } });
  }


  dodajVrijemeIzvrsavanja(krajnjeVrijeme: string): Observable<number> {
    return this.http.post<{ vrijemeID: number }>(
      `${MojConfig.adresa_servera}/VrijemeIzvrsavanja-dodaj`,
      { krajnjeVr: krajnjeVrijeme }
    ).pipe(map(response => response.vrijemeID));
  }



  dodajPosao() {
    this.validationMessages = [];
    this.odabraniDatum = (document.querySelector('input[type="date"]') as HTMLInputElement).value;

    if (!this.opisPosla) {
      this.validationMessages.push('Opis posla je obavezan.');
    }

    if (!this.adresaPosla) {
      this.validationMessages.push('Adresa posla je obavezna.');
    }

    if (this.cijenaPosla < 0 || this.cijenaPosla > 1000) {
      this.validationMessages.push('Cijena mora biti između 0 i 1000.');
    }

    if (this.odabraniDatum < this.today) {
      this.validationMessages.push('Datum ne može biti stariji od današnjeg.');
    }

    if (this.validationMessages.length > 0) {
      return;
    }


    this.dodajVrijemeIzvrsavanja(this.odabraniDatum).subscribe(vrijemeIzvrsavanjaID => {
      const noviPosao = {
        VrijemeIzvrsavanjaID: vrijemeIzvrsavanjaID,
        GradID: this.odabraniGradID,
        FazaPoslaID: 1,
        OpisPosla: this.opisPosla,
        Adresa: this.adresaPosla,
        PoslodavacID: 1,
        ZadatakStraniID: this.odabraniZadatakID,
        Cijena: this.cijenaPosla,
        UkljucenGPS: this.ukljucenGPS
      };

      this.http.post(`${MojConfig.adresa_servera}/Posao-dodaj`, noviPosao).subscribe(response => {

        const successMessage = this.jezikService.isBosanski()
          ? 'Posao dodan'
          : 'Job added';

        alert(successMessage);


        this.opisPosla = '';
        this.adresaPosla = '';
        this.cijenaPosla = 0;
        this.ukljucenGPS = false;
        this.odabraniGradID = 1;
        this.odabraniZadatakID = 1;
        this.odabraniDatum = '';

        this.validationMessages = [];

        this.dodaj_posao = false;
        this.getPoslovi().subscribe((data: any) => {
          this.poslovi = this.filterAndSortPoslovi(data.poslovi);
        });
      });
    });
  }


  initializeWebGL(): void {
    const canvas = document.getElementById('webgl-canvas') as HTMLCanvasElement;
    if (!canvas) {
      console.log('Canvas nije pronađen');
      return;
    }

    const gl = canvas.getContext('webgl');
    if (!gl) {
      console.log('WebGL nije podržan');
      return;
    }

    const vertices = new Float32Array([
      // Vrh strelice (trokut)
      0.6,  0.0, 0.0,  // Vrh strelice
      0.0, -0.4, 0.0,  // Donji levi ugao trokuta
      0.0, 0.4, 0.0,   // Gornji levi ugao trokuta

      // Pravougaonik ispod trokuta
      0.0, 0.15, 0.0,  // Gornji levi ugao pravougaonika
      -0.6, 0.15, 0.0, // Gornji desni ugao pravougaonika
      -0.6, -0.15, 0.0, // Donji desni ugao pravougaonika
      0.0, -0.15, 0.0  // Donji levi ugao pravougaonika
    ]);

    const indices = new Uint16Array([
      0, 1, 2, // Trokut
      3, 4, 5, // Pravougaonik (donji trougao)
      3, 5, 6  // Pravougaonik (gornji trougao)
    ]);

    const vertexBuffer = gl.createBuffer();
    if (!vertexBuffer) {
      console.log('Ne mogu kreirati vertex bafer');
      return;
    }
    gl.bindBuffer(gl.ARRAY_BUFFER, vertexBuffer);
    gl.bufferData(gl.ARRAY_BUFFER, vertices, gl.STATIC_DRAW);

    const indexBuffer = gl.createBuffer();
    if (!indexBuffer) {
      console.log('Ne mogu kreirati index bafer');
      return;
    }
    gl.bindBuffer(gl.ELEMENT_ARRAY_BUFFER, indexBuffer);
    gl.bufferData(gl.ELEMENT_ARRAY_BUFFER, indices, gl.STATIC_DRAW);

    const vertexShaderCode = `
    attribute vec3 position;
    uniform mat4 u_matrix;
    void main() {
      gl_Position = u_matrix * vec4(position, 1.0);
    }
  `;

    const fragmentShaderCode = `
    precision mediump float;
    uniform vec4 u_color;
    void main() {
      gl_FragColor = u_color;
    }
  `;

    const vertexShader = gl.createShader(gl.VERTEX_SHADER);
    if (!vertexShader) {
      console.log('Ne mogu kreirati vertex shader');
      return;
    }
    gl.shaderSource(vertexShader, vertexShaderCode);
    gl.compileShader(vertexShader);
    if (!gl.getShaderParameter(vertexShader, gl.COMPILE_STATUS)) {
      console.log('Greška u kompajliranju vertex shader-a: ' + gl.getShaderInfoLog(vertexShader));
      return;
    }

    const fragmentShader = gl.createShader(gl.FRAGMENT_SHADER);
    if (!fragmentShader) {
      console.log('Ne mogu kreirati fragment shader');
      return;
    }
    gl.shaderSource(fragmentShader, fragmentShaderCode);
    gl.compileShader(fragmentShader);
    if (!gl.getShaderParameter(fragmentShader, gl.COMPILE_STATUS)) {
      console.log('Greška u kompajliranju fragment shader-a: ' + gl.getShaderInfoLog(fragmentShader));
      return;
    }

    const shaderProgram = gl.createProgram();
    if (!shaderProgram) {
      console.log('Ne mogu kreirati shader program');
      return;
    }
    gl.attachShader(shaderProgram, vertexShader);
    gl.attachShader(shaderProgram, fragmentShader);
    gl.linkProgram(shaderProgram);
    if (!gl.getProgramParameter(shaderProgram, gl.LINK_STATUS)) {
      console.log('Greška u povezivanju shader programa: ' + gl.getProgramInfoLog(shaderProgram));
      return;
    }
    gl.useProgram(shaderProgram);

    const positionLocation = gl.getAttribLocation(shaderProgram, 'position');
    gl.enableVertexAttribArray(positionLocation);
    gl.vertexAttribPointer(positionLocation, 3, gl.FLOAT, false, 0, 0);

    const colorLocation = gl.getUniformLocation(shaderProgram, 'u_color');
    const matrixLocation = gl.getUniformLocation(shaderProgram, 'u_matrix');

    const angle = 1 * Math.PI / 180;
    const cos = Math.cos(angle);
    const sin = Math.sin(angle);

    const rotationMatrix = new Float32Array([
      cos, -sin, 0.0, 0.0,
      sin, cos,  0.0, 0.0,
      0.0, 0.0,  1.0, 0.0,
      0.0, 0.0,  0.0, 1.0
    ]);

    gl.uniformMatrix4fv(matrixLocation, false, rotationMatrix);

    let blinkValue = 0.0;
    let increment = 0.02; // Kontroliše brzinu animacije

    function animate() {
      blinkValue += increment;
      if (blinkValue >= 1.0 || blinkValue <= 0.0) {
        increment = -increment; // Obrće smer animacije
      }

      // Animiramo boju od bele do sive
      // Bela je (1.0, 1.0, 1.0), a siva je (blinkValue, blinkValue, blinkValue)
      // Ako je blinkValue 0.5, onda boja bude svetlo siva
      gl!.uniform4f(colorLocation, blinkValue, blinkValue, blinkValue, 1.0);

      gl!.clear(gl!.COLOR_BUFFER_BIT);
      gl!.drawElements(gl!.TRIANGLES, indices.length, gl!.UNSIGNED_SHORT, 0);

      requestAnimationFrame(animate);
    }

    animate();
  }


}
