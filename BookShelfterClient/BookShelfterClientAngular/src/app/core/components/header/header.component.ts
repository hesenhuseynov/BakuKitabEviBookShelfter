  import { Component, OnInit } from '@angular/core';
  import { NavbarComponent } from "../navbar/navbar.component";
  import { CategoryListComponent } from "../../../features/categories/category-list/category-list.component";
  import { Router, RouterLink } from '@angular/router';
  import { BasketService } from '../../services/basket.service';
  import { CommonModule } from '@angular/common';
  import { AuthService } from '../../services/auth.service';
  import { Observable } from 'rxjs';
  import { Basket } from '../../models/basket';
import { FormsModule } from '@angular/forms';

  @Component({
    selector: 'app-header',
    standalone: true,
    imports: [NavbarComponent,
       CategoryListComponent, 
       RouterLink,
       CommonModule,FormsModule],
    templateUrl: './header.component.html',
    styleUrl: './header.component.css'
  })
  export class HeaderComponent implements OnInit {
    itemCount$: Observable<number | null>; 
    totalPrice$: Observable<number | null>;
    basket$: Observable<Basket | null>;
    isLoggedIn$!: Observable<boolean>;
    isDropdownOpen = false;
    private userId: string | null = null;
    searchKeyword: string = '';  


    constructor(private basketService: BasketService, private authService: AuthService,private router:Router) {
      this.itemCount$ = this.basketService.basketItemCount$;
      this.totalPrice$ = this.basketService.basketTotalPrice$;
      this.basket$ = this.basketService.basket$;
      this.isLoggedIn$ = this.authService.isLoggedIn$;
    }

    ngOnInit(): void {
      this.userId=this.authService.getUserId();
      this.basketService.basket$.subscribe(basket => {
        this.authService.checkLoginStatus();
      });

  
      // this.isLoggedIn$.subscribe(isLoggedIn => {
      //   if (isLoggedIn) {
      //     this.userId = this.authService.getUserId();
      //     if (this.userId) {
      //       this.basketService.loadBasket(this.userId);
      //     }
      //   }
      // });
 
  

      if (this.userId) {
        this.basketService.loadBasket(this.userId);
      } else {
        // console.error("UserId alına bilmədi, istifadəçi daxil olmamış ola bilər ")
      }
    }

    searchBooks() {
      if (this.searchKeyword.trim() !== '') {
        this.router.navigate(['/books/search'], { queryParams: { keyword: this.searchKeyword } });
      }
    }

    toggleDropdown(): void {
      this.isDropdownOpen = !this.isDropdownOpen;
    }

    logout(): void {
      this.authService.logout();
      this.basketService.clearBasket(this.userId || ''); 
    }

    removeItem(bookId: number): void {
      if (this.userId) {
        this.basketService.removeItemFromBasket(bookId, this.userId).subscribe(() => {
      
          this.basketService.loadBasket(this.userId!);
        });
      } else {
        console.error("UserId alına bilmədi, kitab səbətdən silinə bilmədi.");
      }
    }

    
  }
