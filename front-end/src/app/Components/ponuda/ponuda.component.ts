import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import {ActivatedRoute, Router} from '@angular/router';
import { MojConfig } from '../../moj-config';
import { JezikService } from "../../Services/jezik.service";

interface Posao {
  posaoID: number;
  nazivZadatka: string;
  cijena: number;
  cijenaString:string;
  zadatakStraniID: number;
  gradID: number;
}

interface Grad {
  gradID: number;
  naziv: string;
}



interface Kategorija {
  kategorijaID: number;
  naziv: string;
}
@Component({
  selector: 'app-ponuda',
  templateUrl: './ponuda.component.html',
  styleUrls: ['./ponuda.component.css']
})
export class PonudaComponent implements OnInit {
  poslovi: Posao[] = [];
  filteredPoslovi: Posao[] = [];
  gradovi: Grad[] = [];

  searchTerm: string = '';
  minPrice: number = 0;
  maxPrice: number = 1000;
  selectedGradID: number = 2;
  showFilterModal: boolean = false;
  nazivKategorije: string = 'Sve kategorije';
  kategorije: Kategorija[] = [];
  showChatBox: boolean = false;
  showSecondChatBox: boolean = false;
  showCanvas:boolean=true;
  constructor(private http: HttpClient, private router: Router, private route: ActivatedRoute, public jezikService: JezikService) {}

  ngOnInit(): void {
    this.route.queryParams.subscribe(params => {
      const kategorijaID = params['kategorijaID'];
      this.searchTerm = '';
      if (kategorijaID) {
        this.getPosloviByKategorija(kategorijaID).subscribe((data: any) => {
          this.poslovi = data.poslovi;
          this.filteredPoslovi = this.poslovi;
          this.getKategorijaNaziv(kategorijaID);
        });
      } else {
        this.getPoslovi().subscribe((data: any) => {
          this.poslovi = data.poslovi;
          this.filteredPoslovi = this.poslovi;
          this.nazivKategorije = '';
        });
      }

      if (params['fromHelp']) {
        this.showChatBox = true;

        setTimeout(() => {
          this.showChatBox = false;

          this.showSecondChatBox = true;
          this.initializeWebGL();
          setTimeout(() => {
            this.showCanvas=false;


            setTimeout(()=>{
              this.showSecondChatBox = false;
            },5000);

          }, 2500);
        }, 5000);
      } else {
        this.showChatBox = false;
        window.scrollTo({
          top: 0,
          behavior: 'smooth'
        });
      }

    });
    this.getGradovi();



  }

  getPoslovi(): Observable<any> {
    return this.http.get<any>(`${MojConfig.adresa_servera}/Posao-preuzmi`);
  }

  getPosloviByKategorija(kategorijaID: number): Observable<any> {
    return this.http.get<any>(`${MojConfig.adresa_servera}/Posao-preuzmi?kategorijaID=${kategorijaID}`);
  }

  getKategorijaNaziv(kategorijaID: number): void {
    this.http.get<{ kategorije: Kategorija[] }>(`${MojConfig.adresa_servera}/Kategorija-preuzmi`)
      .subscribe(response => {


        const kategorija = response.kategorije.find(k => +k.kategorijaID === +kategorijaID);

        if (kategorija) {
          this.nazivKategorije = kategorija.naziv;

        } else {
          this.nazivKategorije = 'Kategorija nije pronađena';
        }
      }, error => {
        console.error('Greška prilikom preuzimanja kategorije:', error);
        this.nazivKategorije = 'Greška pri preuzimanju podataka';
      });
  }




  getGradovi(): void {
    this.http.get<{ gradovi: Grad[] }>(`${MojConfig.adresa_servera}/Grad-preuzmi`).subscribe(response => {
      this.gradovi = response.gradovi;
    });
  }

  navigateToDetails(id: number): void {
    this.router.navigate(['/posao-detalji', id]);
  }

  onSearchChange(): void {
    this.applyFilter();
  }

  clearSearch(): void {
    this.searchTerm = '';
    this.applyFilter();
  }

  openFilterModal(): void {
    this.showFilterModal = true;
  }

  closeFilterModal(): void {
    this.showFilterModal = false;
  }

  applyFilter(): void {
    const searchTermLower = this.searchTerm.toLowerCase();

    this.filteredPoslovi = this.poslovi.filter(posao => {
      const matchesNaziv = !this.searchTerm || posao.nazivZadatka.toLowerCase().includes(searchTermLower);
      const matchesCijena = (this.minPrice === 0 && this.maxPrice === 1000) || (posao.cijena >= this.minPrice && posao.cijena <= this.maxPrice);
      const matchesGrad = this.selectedGradID === 2 || posao.gradID === this.selectedGradID;

      return matchesNaziv && matchesCijena && matchesGrad;
    });

    this.closeFilterModal();
  }

  getZadatakSlikaURL(zadatakID: number): string {
    return `${MojConfig.adresa_servera}/Zadatak/slika?id=${zadatakID}`;
  }

  clearFilter(): void {
    this.minPrice = 0;
    this.maxPrice = 1000;
    this.selectedGradID = 2;
    this.searchTerm = '';

    const kategorijaID = this.route.snapshot.queryParams['kategorijaID'];

    if (kategorijaID) {
      this.getPosloviByKategorija(kategorijaID).subscribe((data: any) => {
        this.poslovi = data.poslovi;
        this.applyFilter();
      });
    } else {
      this.getPoslovi().subscribe((data: any) => {
        this.poslovi = data.poslovi;
        this.applyFilter();
      });
    }

    this.closeFilterModal();
  }


  onGradChange(event: Event): void {
    const selectElement = event.target as HTMLSelectElement;
    this.selectedGradID = +selectElement.value;
    console.log(`Grad changed to: ${this.selectedGradID}`);
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
      -1.0, -1.0, 0.0,
      1.0, -1.0, 0.0,
      -1.0,  1.0, 0.0,
      1.0,  1.0, 0.0
    ]);

    const buffer = gl.createBuffer();
    if (!buffer) {
      console.log('Ne mogu kreirati bafer');
      return;
    }
    gl.bindBuffer(gl.ARRAY_BUFFER, buffer);
    gl.bufferData(gl.ARRAY_BUFFER, vertices, gl.STATIC_DRAW);

    const vertexShaderCode = `
    attribute vec3 position;
    void main() {
      gl_Position = vec4(position, 1.0);
    }
  `;

    const fragmentShaderCode = `
    precision mediump float;
    uniform vec2 u_resolution;
    uniform vec2 u_center;
    uniform float u_radius;
    uniform float u_time;

    void main() {
      vec2 st = gl_FragCoord.xy / u_resolution;
      vec2 dist = st - u_center;
      float len = length(dist);
      float border = 0.02;
      float edge = abs(len - u_radius);

      // Ako je pixel unutar granica bordera, boji ga crno, inače transparentno
      if (edge < border) {
          gl_FragColor = vec4(0.4, 0.4, 0.4, 1.0);
      } else {
        discard; // Transparentno izvan granica bordera
      }
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

    const resolutionLocation = gl.getUniformLocation(shaderProgram, 'u_resolution');
    const centerLocation = gl.getUniformLocation(shaderProgram, 'u_center');
    const radiusLocation = gl.getUniformLocation(shaderProgram, 'u_radius');
    const timeLocation = gl.getUniformLocation(shaderProgram, 'u_time');

    gl.uniform2f(resolutionLocation, canvas.width, canvas.height);
    gl.uniform2f(centerLocation, 0.5, 0.65);
    gl.uniform1f(radiusLocation, 0.3);

    gl.clear(gl.COLOR_BUFFER_BIT);
    gl.drawArrays(gl.TRIANGLE_STRIP, 0, 4);
  }


}
