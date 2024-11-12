import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { BasketService } from '../../core/services/basket.service';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class BasketGuard implements CanActivate {

  constructor(private basketService: BasketService, private router: Router) {}

  canActivate(): Observable<boolean> {
    return this.basketService.basket$.pipe(
      map(basket => {
        if (basket && basket.items.length > 0) {
          return true;
        } else {
          this.router.navigate(['/basket']); 
       
          return false;
        }
      })
    );
  }
}
