import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Book } from '../../core/models/book';
import { BookService } from '../../core/services/book.service';
import { error } from 'node:console';
import { CoreModule } from '../../core/core.module';
import { HeaderComponent } from "../../core/components/header/header.component";
import { BasketService } from '../../core/services/basket.service';
import { AuthService } from '../../core/services/auth.service';
import { Router, RouterModule } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule, RouterModule,  HeaderComponent],
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {



  activeTab: string = 'featured';
  featuredProducts: Book[] = [];
  newArrivals: Book[] = [];
  mostViewProducts: Book[] = [];

  private userId: string | null = null;

  constructor(private bookService:BookService,private router:Router,private basketService:BasketService
    ,private authService:AuthService,private toastr:ToastrService
  ){}

  ngOnInit(): void {
    
    this.loadFeaturedBooks();
    this.loadNewArrivals();
    this.loadMostViewedBooks();
  }


  selectTab(tab: string) {
    this.activeTab = tab;
  }

  loadFeaturedBooks() {
    this.bookService.getFeaturedBooks().subscribe(books => {
      this.featuredProducts = books;
    
    });
  }

  loadNewArrivals() {
    this.bookService.getNewArrivals().subscribe(books => {
      this.newArrivals = books;
      
    },
  error=>{
    console.log('Error fetching featured books',error)
  });
  }

  loadMostViewedBooks() {
    this.bookService.getMostViewedBooks().subscribe(books => {
      this.mostViewProducts = books;
    });
  }


  ViewBookDetails(bookId:number):void{
    this.router.navigate(['/books',bookId])
  }



  addToBasket(booksId: number): void {
    
    this.userId = this.authService.getUserId();
  
    if (!this.userId) {
      
      // alert('Səbətə kitap əlavə etmək üçün Daxil olun .');
      this.toastr.warning("Səbətə kitap əlavə etmək üçün Daxil olun ")
      this.router.navigate(['/login']); 
      return;
    }
  
    this.basketService.addItemToBasket(booksId, 1, this.userId).subscribe(() => {
      this.basketService.getBasket(this.userId!).subscribe();
    });
  }

}
