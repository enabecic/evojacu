import { Component, OnInit, ElementRef } from '@angular/core';
import * as THREE from 'three';
import { FontLoader } from 'three/examples/jsm/loaders/FontLoader.js';
import { TextGeometry } from 'three/examples/jsm/geometries/TextGeometry.js';

@Component({
  selector: 'app-webgl-logo',
  templateUrl: './webgl-logo.component.html',
  styleUrls: ['./webgl-logo.component.css']
})
export class WebglLogoComponent implements OnInit {
  private scene!: THREE.Scene;
  private camera!: THREE.PerspectiveCamera;
  private renderer!: THREE.WebGLRenderer;
  private textMeshOmiljeni!: THREE.Mesh; // Mesh koji rotiramo

  constructor(private el: ElementRef) {}

  ngOnInit(): void {
    this.initThree();
    this.animate();
  }

  private initThree(): void {
    this.scene = new THREE.Scene();

    this.camera = new THREE.PerspectiveCamera(
      75,
      window.innerWidth / window.innerHeight,
      0.1,
      1000
    );
    this.camera.position.z = 13; // Malo dalje za bolji pregled

    this.renderer = new THREE.WebGLRenderer({ alpha: true });
    this.renderer.setClearColor(0x000000, 0);
    this.renderer.setSize(290, 100);
    this.el.nativeElement.appendChild(this.renderer.domElement);

    const fontLoader = new FontLoader();
    fontLoader.load(
      'https://threejs.org/examples/fonts/helvetiker_regular.typeface.json',
      (font) => {
        const textMaterial = new THREE.MeshBasicMaterial({ color: 0x000000 });

        const textGeometryOmiljeni = new TextGeometry('EVOJACU', {
          font: font,
          size: 3, // Malo povećano
          depth: 0.4,
        });

        this.textMeshOmiljeni = new THREE.Mesh(textGeometryOmiljeni, textMaterial);
        this.scene.add(this.textMeshOmiljeni);

        // Podešena pozicija
        this.textMeshOmiljeni.position.set(10, 0, 0);

        // Svetlo za bolji prikaz
        const ambientLight = new THREE.AmbientLight(0xffffff, 1);
        this.scene.add(ambientLight);
      }
    );
  }
  private angle: number = 0; // Ugao rotacije

  private animate(): void {
    requestAnimationFrame(() => this.animate());

    this.angle += 0.02; // Brzina rotacije
    const radius = 5; // Poluprečnik kružne putanje

    // Pomeranje teksta u krug
    if (this.scene.children.length > 0) {
      const textMesh = this.scene.children[0] as THREE.Mesh;
      textMesh.position.x = Math.cos(this.angle) * radius;
      textMesh.position.y = Math.sin(this.angle) * radius;
    }

    this.renderer.render(this.scene, this.camera);
  }
}
