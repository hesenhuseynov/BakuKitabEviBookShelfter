import { Component } from '@angular/core';
import { Router, RouterOutlet } from '@angular/router';
import { HeaderComponent } from '../../components/header/header.component';
import { FooterComponent } from '../../components/footer/footer.component';
import { CarouselComponent } from "../../components/carousel/carousel.component";
import { CommonModule } from '@angular/common';
import { BannerComponent } from "../../components/banner/banner.component";

@Component({
  selector: 'app-main-layout',
  standalone:true,
  imports: [
    RouterOutlet,
    HeaderComponent,
    FooterComponent,
    CarouselComponent,
    CommonModule,
    BannerComponent
],
  templateUrl: './main-layout.component.html',
  styleUrl: './main-layout.component.css'
})
export class MainLayoutComponent {
  constructor(private router:Router){

  }
  isHomePage(): boolean {
    return this.router.url === '/';  
  }
}
