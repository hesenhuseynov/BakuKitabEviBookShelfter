import { Component } from '@angular/core';
import { NavigationEnd, Router, RouterOutlet } from '@angular/router';
import { NavbarComponent } from "./core/components/navbar/navbar.component";
import { FooterComponent } from "./core/components/footer/footer.component";
import { HeaderComponent } from "./core/components/header/header.component";
import {HomeComponent} from "./features/home/home.component";
import { BookDetailComponent } from './features/books/book-detail/book-detail.component';
import { RelatedBooksComponent } from './features/books/related-books/related-books.component';
import { LoginComponent } from "./features/auth/login/login.component";
import { CommonModule } from '@angular/common';
import { AdminPanelModule } from './features/admin-panel/admin-panel.module';

@Component({
  selector: 'app-root',
  standalone: true,
    imports: [
      RouterOutlet,
    NavbarComponent,
    FooterComponent,
    HeaderComponent,
    CommonModule,
    AdminPanelModule
      
    ],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'BookShelfterClientAngular';
 
}
