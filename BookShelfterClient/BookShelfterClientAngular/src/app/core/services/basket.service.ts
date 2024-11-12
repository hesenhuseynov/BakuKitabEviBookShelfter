import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { map, switchMap, tap } from 'rxjs/operators';
import { Basket } from '../models/basket';
import { BasketItem } from '../models/basketitem';
import { BookService } from './book.service';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class BasketService {

  private baseUrl =environment.apiUrl+'/Basket';

  // private baseUrl = 'http://localhost:5287/api/Basket';
  private basketSubject = new BehaviorSubject<Basket | null>(null);
  basket$ = this.basketSubject.asObservable();

  private basketItemCount = new BehaviorSubject<number>(0);
  private basketTotalPrice = new BehaviorSubject<number>(0.0);
  basketItemCount$ = this.basketItemCount.asObservable();
  basketTotalPrice$ = this.basketTotalPrice.asObservable();

  constructor(private http: HttpClient ,private bookService:BookService) { }

  loadBasket(userId: string): void {
    this.http.get<any>(`${this.baseUrl}/${userId}`)
        .pipe(
            map(response => {
                if (!response || !response.basketItems) {
                    return this.createBasket(userId);
                }
                return this.mapBasketResponse(response, userId);
            }),
            tap(basket => {
                this.basketSubject.next(basket);
                this.updateBasketInfo(basket);
            })
        )
        .subscribe();
}

private createBasket(userId: string): Basket {
  const newBasket: Basket = {
      id: userId,
      userId: userId,
      items: [],
      total: 0
  };
  return newBasket;
}

  // addItemToBasket(productId: number, quantity: number, userId: string): Observable<any> {
  //   return this.http.post(`${this.baseUrl}/add-item`, { productId, quantity, userId }).pipe(
  //     tap(() => {
  //       this.loadBasket(userId); 
  //     })
  //   );
  // }
  addItemToBasket(bookId: number, quantity: number, userId: string): Observable<any> {debugger;
    return this.bookService.getBookById(bookId.toString()).pipe(
      
        switchMap(book => {
            let currentBasket = this.basketSubject.value;
            // if (!currentBasket) {
            //     throw new Error('Basket not found');
            // }
            if (!currentBasket) {
              currentBasket = this.createBasket(userId);
              this.basketSubject.next(currentBasket);
          }


            const existingItem = currentBasket.items.find(item => item.bookId === bookId);
            if (existingItem) {
                existingItem.quantity += quantity;
                existingItem.totalPrice = existingItem.quantity * existingItem.price;
            } else {
                currentBasket.items.push({
                    bookId,
                    productName: book.bookName,
                    quantity,
                    price: book.price,
                    totalPrice: book.price * quantity,
                    imageUrls: book.imageUrls || []
                });
            }

            this.updateBasketInfo(currentBasket);

            return this.http.post(`${this.baseUrl}/add-item`, { bookId, quantity, userId });
        }),
        tap(() => {
            this.loadBasket(userId); 
        })
    );
}

  
  removeItemFromBasket(bookId: number, userId: string): Observable<any> {
    return this.http.delete(`${this.baseUrl}/remove-item`, {
      body: { bookId, userId } 
    }).pipe(
      tap(() => this.loadBasket(userId))
    );
  }

  clearBasket(userId: string): Observable<any> {
    return this.http.post(`${this.baseUrl}/clear`, {}).pipe(
      tap(() =>{
        this.resetBasket();
        // this.loadBasket(userId)
        this.updateBasketInfoFromServer(userId);
      })
    );
  }

  getBasket(userId: string): Observable<Basket> {
    return this.http.get<any>(`${this.baseUrl}/${userId}`).pipe(
      map(response => this.mapBasketResponse(response, userId)),
      tap(basket => this.updateBasketInfo(basket))
    );
  }

  private mapBasketResponse(response: any, userId: string): Basket {
    return {
      id: userId,
      userId: userId,
      items: response.basketItems.map((item: any) => this.mapBasketItem(item)),
      total: response.basketItems.reduce((sum: number, item: any) => sum + (item.unitPrice * item.quantity), 0)
    };
  }

  private mapBasketItem(item: any): BasketItem {
    return {
      bookId: item.bookId,
      productName: item.book.bookName,
      quantity: item.quantity,
      price: item.unitPrice,
      totalPrice: item.unitPrice * item.quantity,
      imageUrls:item.book.imageUrls|| []
    };
  }

  private updateBasketInfo(basket: Basket): void {
    const items = basket.items || [];
    const itemCount = items.reduce((sum, item) => sum + item.quantity, 0);
    const totalPrice = items.reduce((sum, item) => sum + (item.price * item.quantity), 0.0);
    this.basketItemCount.next(itemCount);
    this.basketTotalPrice.next(totalPrice);
  }

  private updateBasketInfoFromServer(userId: string): void {
    this.getBasket(userId).subscribe(basket => {
      this.basketSubject.next(basket);
      this.updateBasketInfo(basket);
    });
  }

  resetBasket(): void {
    const emptyBasket: Basket = {
      id: '',
      userId: '', 
      items: [],
      total: 0
    };
    this.basketSubject.next(emptyBasket);
    this.updateBasketInfo(emptyBasket); 
  }
}
