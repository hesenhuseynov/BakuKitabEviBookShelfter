import { Component, OnInit } from '@angular/core';
import { BasketService } from '../../core/services/basket.service';
import { Basket } from '../../core/models/basket';
import { BasketItem } from '../../core/models/basketitem';
import { AuthService } from '../../core/services/auth.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-basket',
  standalone: true,
  imports: [CommonModule,FormsModule],
  templateUrl: './basket.component.html',
  styleUrls: ['./basket.component.css']
})
export class BasketComponent implements OnInit {
  basket: Basket | null = null;
  userId!: string;
  isBasketEmpty: boolean = true; 

  constructor(private basketService: BasketService,
     private authService: AuthService,private router:Router) {}

  ngOnInit(): void {
    this.userId = this.authService.getUserId()!;
    this.basketService.basket$.subscribe(basket => {
      this.basket = basket;
      this.isBasketEmpty=!basket||basket.items.length===0;
    });
    this.loadBasket();
  }

  loadBasket(): void {
    this.basketService.getBasket(this.userId).subscribe(basket => {
      this.basket = basket;
      this.isBasketEmpty = !basket || basket.items.length === 0;
    });
  }

  removeItem(productId: number): void {
    this.basketService.removeItemFromBasket(productId, this.userId).subscribe(() => {
      this.loadBasket(); // Ürün silindikten sonra sepeti yeniden yükle
    });
  }

  updateQuantity(item: BasketItem, change: number): void {
    const newQuantity = item.quantity + change;
    if (newQuantity <= 0) {
        this.removeItem(item.bookId); // Eğer miktar 0 veya daha az olursa, ürünü sepetten kaldır
    } else {
        this.basketService.addItemToBasket(item.bookId, change, this.userId).subscribe(() => {
            this.loadBasket(); // Miktar güncellendikten sonra sepeti yeniden yükle
        });
    }
}

  clearBasket(): void {
    this.basketService.clearBasket(this.userId).subscribe(() => {
      this.loadBasket(); // Sepet temizlendikten sonra sepeti yeniden yükle
    });
  }


  goToOrderPage(){
    if (!this.isBasketEmpty) {
    this.router.navigate(['/orders/summary'])
    }
  }
}
