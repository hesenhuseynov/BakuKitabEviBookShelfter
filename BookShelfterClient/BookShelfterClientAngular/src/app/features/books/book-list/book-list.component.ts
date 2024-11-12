import { Component, OnInit } from '@angular/core';
import { Book } from '../../../core/models/book';
import { BookService } from '../../../core/services/book.service';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { CategoryListComponent } from '../../categories/category-list/category-list.component';
import { BasketService } from '../../../core/services/basket.service';
import { AuthService } from '../../../core/services/auth.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-book-list',
  standalone: true,
  imports: [CommonModule, RouterModule, CategoryListComponent],
  templateUrl: './book-list.component.html',
  styleUrl: './book-list.component.css'
})
export class BookListComponent implements OnInit {

  books: Book[] = [];
  pageNumber:number=1;
  pageSize :number=8;
  categoryId!: number;
  private userId: string | null = null;
  totalProductCount: number = 0; 
  constructor(
    private bookService: BookService,
    private router: ActivatedRoute,
    private basketService:BasketService,
    private authService:AuthService,
    private navigation:Router,
    private toastr:ToastrService

  ) {}

  // ngOnInit(): void {
  //   this.router.paramMap.subscribe((params) => {
  //     this.categoryId = Number(params.get('id'));
  //     this.loadBooks();
  //   });

  ngOnInit(): void {
    this.router.paramMap.subscribe((params) => {
      this.categoryId = Number(params.get('id'));
      
      this.router.queryParams.subscribe(queryParams => {
        const keyword = queryParams['keyword'];
        
        if (keyword) {
          this.bookService.searchBooks(keyword).subscribe((books) => {
            this.books = books;
            this.totalProductCount = books.length; 
          }, error => {
            console.error("Error loading search results:", error);
          });
        } else {
          this.loadBooks();
        }
      });
    });

    console.log('TotalProductCount:', this.totalProductCount);
console.log('Books:', this.books);
console.log('PageNumber:', this.pageNumber);
  }

  loadBooks(): void {
    this.books = [];

    if (this.categoryId) {
      this.bookService.getBooksByCategoryId(this.categoryId, this.pageNumber, this.pageSize).subscribe((data) => {
        this.books = data.books; 
        this.totalProductCount = data.totalProductCount; 
        console.log(data);
      }, error => {
        console.error("Error loading books:", error);
      });
    } else {
      this.bookService.getBooks(this.pageNumber, this.pageSize).subscribe((data) => {
        this.books = data.books;  
        this.totalProductCount = data.totalProductCount;  
        console.log(data);
      }, error => {
        console.error("Error loading books:", error);
      });
    }
  }
  

  nextPage(): void {
    const totalPages = Math.ceil(this.totalProductCount / this.pageSize);  
    if (this.pageNumber < totalPages) {  
      this.pageNumber++;
      this.loadBooks();
    }
  }
  
previousPage(): void {
  if (this.pageNumber > 1) {  
    this.pageNumber--;
    this.loadBooks();
  }
}

addToBasket(bookId: number): void {
  this.userId = this.authService.getUserId();

  if (!this.userId) {
    this.toastr.warning("Səbətə kitap əlavə etmək üçün Daxil olun ")
    this.navigation.navigate(['/login']);
    return;
  }

  this.basketService.addItemToBasket(bookId, 1, this.userId).subscribe(() => {
    this.basketService.getBasket(this.userId!).subscribe();
  });
}
}
