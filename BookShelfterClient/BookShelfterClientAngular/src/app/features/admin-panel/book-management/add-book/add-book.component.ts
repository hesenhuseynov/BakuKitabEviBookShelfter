import { Component, OnInit } from '@angular/core';
import { BookService } from '../../../../core/services/book.service';
import { Router, RouterModule } from '@angular/router';
import { addBookDto } from '../../../../core/models/addbookDto';
import { Category } from '../../../../core/models/category';
import { CategoryService } from '../../../../core/services/category.service';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { BookDetailComponent } from '../../../books/book-detail/book-detail.component';
import { catchError, tap, throwError } from 'rxjs';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-add-book',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule
    // BrowserAnimationsModule,
    // ToastrModule
    

  ],
  templateUrl: './add-book.component.html',
  styleUrls: ['./add-book.component.css']
})
export class AddBookComponent implements OnInit {
  book: addBookDto = {
    bookName: '',
    price: 0,
    stock: 0,
    authorName: '',
    description: '',
    categoryId: 0,
    languageId: 1
  };

  languages = [
    { id: 1, name: 'az' }
   
  ];

  categories: Category[] = [];
  selectedFile: File | null = null;

  constructor(
    private bookService: BookService,
    private categoryService: CategoryService,
    private router: Router,
    private toastr: ToastrService 
  ) {}

  ngOnInit(): void {
    this.categoryService.getCategories().subscribe(data => {
      this.categories = data;
    });
  }

  onFileSelected(event: any): void {
    this.selectedFile = event.target.files[0];
  }
  loading = false;

  onSubmit(): void {
    if (!this.isFormValid()) {
      // Eğer form boş veya eksikse Toastr ile uyarı mesajı gösteriyoruz
      this.toastr.error('Xahiş olunur məlumatları doldurun ', 'Form əksik');
      return;
    }
    this.loading = true; 
    this.bookService.addBooks(this.book).pipe(
        tap((newBook: { bookId: number; bookName: string; success: boolean; message: string }) => {
            if (this.selectedFile) {
                const formData = new FormData();
                formData.append('file', this.selectedFile);
                formData.append('bookId', newBook.bookId.toString());

                console.log(`newBookId: ${newBook.bookId}`);
                console.log(formData.get('bookId') + ' is the bookId');

                this.bookService.uploadBookImage(formData).subscribe({
                    next: () => {
                        alert('Book successfully added!');
                        this.router.navigate(['/admin-panel/books']);
                    },
                    error: (error) => {
                        console.error('Error uploading image:', error);
                        alert('Book added, but there was an error uploading the image.');
                    },
                    complete: () => {
                        this.loading = false; 
                    }
                });
            } else {
                alert('Book successfully added!');
                this.router.navigate(['/admin-panel/books']);
                this.loading = false; 
            }
        }),
        catchError(error => {
            console.error('Error adding book:', error);
            alert('There was an error adding the book.');
            this.loading = false; 
            return throwError(() => new Error(error.message || 'An unknown error occurred.'));
        })
    ).subscribe();
}

isFormValid(): boolean {
  return this.book.bookName.trim() !== '' &&
         this.book.authorName.trim() !== '' &&
         this.book.price > 0 &&
         this.book.stock >= 0 &&
         this.book.categoryId !== 0;
}
}

  
