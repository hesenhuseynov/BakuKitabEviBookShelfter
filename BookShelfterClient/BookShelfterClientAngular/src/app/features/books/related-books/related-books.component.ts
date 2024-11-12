import { Component, OnInit, Input } from '@angular/core';
import { Book } from '../../../core/models/book';
import { BookService } from '../../../core/services/book.service';
import { CommonModule } from '@angular/common';
import { Router, RouterLink, RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-related-books',
  templateUrl: './related-books.component.html',
  styleUrls: ['./related-books.component.css'],
  imports: [CommonModule,FormsModule,RouterLink,RouterModule],
  standalone: true,
})
export class RelatedBooksComponent implements OnInit {
  @Input() categoryId!: number;
  @Input() currentBookId!: number;  
  relatedBooks: Book[] = [];

  constructor(private bookService: BookService, private router: Router) {}

  ngOnInit(): void {

    console.log('Category ID:', this.categoryId);
    console.log('Current Book ID:', this.currentBookId);
  
    if (this.categoryId) {
      this.bookService.getRelatedBooks(this.categoryId).subscribe((books) => {
        this.relatedBooks = books.filter(book => book.bookId !== this.currentBookId);
        console.log(this.relatedBooks);
      });
    }
  }

  navigateToDetail(bookId: number): void {
    this.router.navigate(['/books', bookId]);
  }
  
  // ViewBookDetails(bookId: number): void {
  //   this.router.navigate(['/books', bookId]);
  // }
}
