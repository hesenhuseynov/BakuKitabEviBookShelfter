import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { BookService } from '../../../core/services/book.service';
import { CommonModule } from '@angular/common';
import { ReviewComponent } from "../review/review.component";
import { BookDetailDto } from '../../../core/models/bookdetail';
import { FormsModule } from '@angular/forms';
import { RelatedBooksComponent } from '../related-books/related-books.component';
import { BasketService } from '../../../core/services/basket.service';
import { AuthService } from '../../../core/services/auth.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-book-detail',
  standalone: true,
  imports: [CommonModule, RouterModule, ReviewComponent,RelatedBooksComponent,FormsModule],
  templateUrl: './book-detail.component.html',
  styleUrls: ['./book-detail.component.css']
})
export class BookDetailComponent implements OnInit {
  book!: BookDetailDto;
  activeTab: string = 'description';
  averageRating: number = 0;
  totalReviews: number = 0;
  stars: number[] = [1, 2, 3, 4, 5];
  quantity: number = 1;


  private userId: string | null = null;


  @ViewChild('reviewsSection') reviewsSection!: ElementRef;

  constructor(
    private activatedRoute: ActivatedRoute,
    private router:Router,
    private bookService: BookService,
    private basketService:BasketService,
    private authService:AuthService,
    private toastr:ToastrService
  ) {}

  ngOnInit(): void {
    this.activatedRoute.paramMap.subscribe(params => {
      const bookId = params.get('id');
      if (bookId) {
      this.loadBookDetails(+bookId); 
        window.scrollTo({ top: 0, behavior: 'smooth' });

      }
    });

    // if (bookId) {
    //   this.bookService.getBookDetails(+bookId).subscribe({
    //     next: (response) => {
    //       if (response) {
    //         this.book = response;
    //         console.log(response);
    //         this.averageRating = this.book.averageRating;
    //         this.totalReviews = this.book.totalReviews;

    //         debugger;

    //         if (this.book.imageUrls && this.book.imageUrls.length > 0) {
    //         } else {
    //           console.warn("Image URLs are missing or empty");
    //         }
    //       } else {
    //         console.error("No book data received");
    //       }
    //     },
    //     error: (error) => {
    //       console.error('An error occurred:', error);
    //     }
    //   });
    // } else {
    //   console.error('Book ID is null');
    // }
  }


  
loadBookDetails(bookId: number) {
  this.bookService.getBookDetails(bookId).subscribe({
    next: (response) => {
      this.book = response;
      this.averageRating = this.book.averageRating;
      this.totalReviews = this.book.totalReviews;
    },
    error: (error) => {
    }
  });
}

  selectTab(tab: string) {
    this.activeTab = tab;
    if (tab === 'reviews' && this.reviewsSection) {
      this.scrollToReviews();
    }
  }

  scrollToReviews(): void {
    this.reviewsSection.nativeElement.scrollIntoView({ behavior: 'smooth' });
  }

  quickRate(rating: number): void {
    console.log(`User rated this product: ${rating} stars`);
  }


  addToBasket(booksId: number): void {
    this.userId = this.authService.getUserId();
  
    if (!this.userId) {
      this.toastr.warning("Səbətə kitap əlavə etmək üçün Daxil olun ")
      this.router.navigate(['/login']); 
      return;


    }


    if (this.quantity < 1) {
      alert('düzgün miqdar girin zəhmət olmasa');
      return;
    }
  
    this.basketService.addItemToBasket(booksId, this.quantity, this.userId).subscribe(() => {
      this.basketService.getBasket(this.userId!).subscribe();
    });
  }

  limitInputLength(event: KeyboardEvent) {
    const input = event.target as HTMLInputElement;
    if (input.value.length >= 2 && (event.key >= '0' && event.key <= '9')) {
      event.preventDefault();
    }
  }

  checkQuantity() {
    if (this.quantity > 99) {
      this.quantity = 99;
    } else if (this.quantity < 1) {
      this.quantity = 1;
    }
  }
}
