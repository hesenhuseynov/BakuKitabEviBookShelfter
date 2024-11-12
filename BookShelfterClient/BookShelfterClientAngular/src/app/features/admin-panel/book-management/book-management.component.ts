import { Component, OnInit } from '@angular/core';
import { Book } from '../../../core/models/book';
import { BookService } from '../../../core/services/book.service';
import { Router, RouterModule } from '@angular/router';
import { AddBookComponent } from './add-book/add-book.component';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-book-management',
  templateUrl: './book-management.component.html',
  styleUrl: './book-management.component.css',
  standalone:true,
  imports:[CommonModule,RouterModule,FormsModule,ReactiveFormsModule]
})
export class BookManagementComponent  implements OnInit{
  books: Book[] = [];

   constructor(private bookService:BookService,private router:Router){

   }
  ngOnInit(): void {
   this.bookService.getBooks().subscribe(data=>{
    this.books=data.books;
   })
   

  }

  loadBooks(): void {
    this.bookService.getBooks().subscribe((books) => {
      this.books = books.books;
    });
  }

  editBook(id: number): void {
      
    this.router.navigate([`/admin-panel/books/edit`, id]).then((success) => {
      if (success) {
        console.log('Navigation successful');
      } else {
        console.log('Navigation failed');
      }
    }).catch(err => {
      console.error('Navigation error:', err);
    });

  }

  deleteBook(id: number): void {
    if (confirm('Bu kitabı silmek istediğinizden emin misiniz?')) {
      this.bookService.deleteBook(id).subscribe({
        next: () => {
          alert('Kitap başarıyla silindi!');
          this.loadBooks();  // Kitap listesini yeniden yükle
        },
        error: (error) => {
          console.error('Kitap silme hatası:', error);
          alert('Kitap silinirken bir hata oluştu.');
        }
      });
    }
  }


  
  


  // deleteBook(id: number): void {
  //   if (confirm('Bu kitabı silmek istediğinizden emin misiniz?')) {
  //     this.bookService.de(id).subscribe(() => {
  //       this.loadBooks();
  //     });
  //   }
  // }


}
