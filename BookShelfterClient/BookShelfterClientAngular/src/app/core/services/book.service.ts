import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Book } from '../models/book';
import { map, Observable } from 'rxjs';
import { BookDetailDto } from '../models/bookdetail';
import { response } from 'express';
import { addBookDto } from '../models/addbookDto';
import { editBookDto } from '../models/editbookDto';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class BookService {
  
  private apiUrl = environment.apiUrl + '/books';
    //  private apiUrl ='http://localhost:5287/api/books';

  constructor(private http:HttpClient) {
   }

   getBooks(pageNumber: number = 1, pageSize: number = 8): Observable<{ totalProductCount: number, books: Book[] }> {
    let params = new HttpParams()
      .set('PageNumber', pageNumber.toString())
      .set('PageSize', pageSize.toString());
  
    return this.http.get<{ totalProductCount: number, books: Book[] }>(this.apiUrl, { params });
  }
  
  searchBooks(keyword: string): Observable<Book[]> {
    return this.http.get<{ books: Book[] }>(`${this.apiUrl}/search`, { params: new HttpParams().set('keyword', keyword) })
      .pipe(map(response => response.books));
  }
  
  
  getFeaturedBooks(): Observable<Book[]> {
    return this.http.get<{ totalProductCount: number, books: Book[] }>(`${this.apiUrl}/featured`).pipe(
      map(response => response.books)
    );
  }

  getNewArrivals(): Observable<Book[]> {
    return this.http.get<{ totalProductCount: number, books: Book[] }>(`${this.apiUrl}/new-arrivals`).pipe(
      map(response => response.books)
    );
  }

  getMostViewedBooks(): Observable<Book[]> {
    return this.http.get<{ totalProductCount: number, books: Book[] }>(`${this.apiUrl}/most-viewed`).pipe(
      map(response => response.books)
    );
  }

  // getBookById(bookId:string) :Observable<Book>{
  //   return this.http.get<Book>(`${this.apiUrl}/${bookId}`)
  // }
  getBookById(id: string): Observable<Book> {
    return this.http.get<any>(`${this.apiUrl}/${id}`).pipe(
      map(response => response.book)
    );

  }



  

  getBookDetails(bookId: number): Observable<BookDetailDto> {
  
     return this.http.get<any>(`${this.apiUrl}/${bookId}/details`).pipe(
      map(response=>response.book)
     )
  }

  getRelatedBooks(categoryId: number): Observable<Book[]> {
    
    return this.http.get<{ bookDtos: Book[] }>(`${this.apiUrl}/related/${categoryId}`).pipe(
      map(response => response.bookDtos)
    );
  }

//   getBooksByCategoryId(categoryId: number): Observable<Book[]> {
//     const url = `http://localhost:5287/api/Category/${categoryId}/books`;
//     return this.http.get<{ booksDto: Book[] }>(url).pipe(
//         map(response => response.booksDto)
//     );
// }

getBooksByCategoryId(categoryId: number, pageNumber: number, pageSize: number): Observable<{ totalProductCount: number, books: Book[] }> {
  // const url = `http://localhost:5287/api/Category/${categoryId}/books`;
  const url = `${environment.apiUrl}/Category/${categoryId}/books`;

  let params = new HttpParams()
    .set('PageNumber', pageNumber.toString())
    .set('PageSize', pageSize.toString());

  return this.http.get<{ totalProductCount: number, booksDto: Book[] }>(url, { params }).pipe(
    map(response => ({
      totalProductCount: response.totalProductCount,
      books: response.booksDto
    }))
  );
}


addBooks(book: addBookDto): Observable<{ bookId: number, bookName: string, success: boolean, message: string }> {
  return this.http.post<{ bookId: number, bookName: string, success: boolean, message: string }>(this.apiUrl, book);
}


updateBook(book: editBookDto): Observable<{ bookId: number; bookName: string; success: boolean; message: string }> {
  return this.http.put<{ bookId: number; bookName: string; success: boolean; message: string }>(`${this.apiUrl}/updateBook`, book);
}


  uploadBookImage(formData: FormData): Observable<any> {
    return this.http.post(`${this.apiUrl}/uploadBookImage`, formData);
  }


  deleteBook(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
  

}

