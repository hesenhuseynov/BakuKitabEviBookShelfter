import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { Category } from '../../../../core/models/category';
import { BookService } from '../../../../core/services/book.service';
import { CategoryService } from '../../../../core/services/category.service';
import { editBookDto } from '../../../../core/models/editbookDto';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { MatButtonModule } from '@angular/material/button';

@Component({
  selector: 'app-edit-book',
  standalone: true,
  imports: [CommonModule, 
    FormsModule,
     ReactiveFormsModule,
     RouterModule
    
     
    ],
  templateUrl: './edit-book.component.html',
  styleUrls: ['./edit-book.component.css']
})
export class EditBookComponent implements OnInit {

  book: editBookDto = {
    bookId: 0,
    bookName: '',
    price: 0,
    stock: 0,
    authorName: '',
    description: '',
    categoryId: 0,
    languageId: 1 // Default value set to 'az'
  };

  languages = [
    { id: 1, name: 'az' },
    { id: 2, name: 'en' }
  ];
  categories: Category[] = [];
  selectedFile: File | null = null;
  loading = false;

  constructor(
    private bookService: BookService,
    private categoryService: CategoryService,
    private router: Router,
    private activatedRoute: ActivatedRoute
  ) { }

  ngOnInit(): void {
    this.categoryService.getCategories().subscribe(data => {
      this.categories = data;
      this.getFormBookAll();
    });
  }

  onFileSelected(event: any): void {
    this.selectedFile = event.target.files[0];
  }

  getFormBookAll(): void {
    const bookId = this.activatedRoute.snapshot.paramMap.get('id');
    if (bookId) {
      this.bookService.getBookById(bookId).subscribe(data => {
        this.book = data;
        this.book.languageId = this.book.languageId || this.languages[0]?.id || 1; // Default to 'az' if null
        this.book.categoryId = this.book.categoryId || this.categories.find(c => c.categoryId === this.book.categoryId)?.categoryId || this.categories[0]?.categoryId;
      });
    }
  }

  onSubmit(): void {
    if (!this.book.bookId || !this.book.categoryId) {
      alert('Please ensure all required fields are filled.');
      return;
    }

    this.loading = true;

    this.bookService.updateBook(this.book).subscribe(
      updatedBook => {
        if (this.selectedFile) {
          const formData = new FormData();
          formData.append('file', this.selectedFile);
          formData.append('bookId', updatedBook.bookId.toString());

          this.bookService.uploadBookImage(formData).subscribe(
            () => {
              alert('Book successfully updated and image uploaded!');
              this.router.navigate(['/admin-panel/books']);
            },
            () => {
              alert('Book updated, but an error occurred while uploading the image.');
            }
          );
        } else {
          alert('Book successfully updated!');
          this.router.navigate(['/admin-panel/books']);
        }
      },
      () => {
        alert('An error occurred while updating the book.');
      }
    ).add(() => this.loading = false); // Ensure loading stops after operation
  }
}
