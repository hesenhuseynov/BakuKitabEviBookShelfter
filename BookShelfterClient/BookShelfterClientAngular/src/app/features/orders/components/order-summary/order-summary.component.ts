import { Component, OnInit } from '@angular/core';
import { BasketService } from '../../../../core/services/basket.service';
import { Basket } from '../../../../core/models/basket';
import { OrderService } from '../../../../core/services/order.service';
import { CommonModule } from '@angular/common';
import { AuthService } from '../../../../core/services/auth.service';
import { CustomerService } from '../../../../core/services/customer.service';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { OrderStatus } from '../../../../core/models/order/orderstatus';
import { OrderDTO } from '../../../../core/models/order/orderDto';

@Component({
  selector: 'app-order-summary',
  standalone: true,
  imports: [CommonModule,FormsModule],
  templateUrl: './order-summary.component.html',
  styleUrl: './order-summary.component.css'
})
export class OrderSummaryComponent implements OnInit {
  basket: Basket | null = null;
  deliveryAddress: string = '';
  paymentMethod: string = '';
  phoneNumber: string = '';
  customerId!: number;
  orderNumber!:string;

  formSubmitted: boolean = false;
  errorMessage: string | null = null;

  constructor(private basketService:BasketService,
    private orderService:OrderService,
    private authService:AuthService,
    private customerService:CustomerService,
    private router:Router
  ){
 
  }
  ngOnInit(): void {
    this.authService.getUserId$().subscribe(userId => {
      if(!userId){
        console.error('Istifadeçi  ID tapılabilmedi, itdifadeçi login  olmamış olabilər.');
           return ;
      }
      this.customerService.getCustomerByUserId(userId!).subscribe(customer => {
        console.log('Customer ID:', customer.id);       
          this.customerId = customer.id; 
        this.deliveryAddress = customer.adress; 
        this.phoneNumber = customer.phoneNumber; 
      });

      this.basketService.basket$.subscribe(basket => {
        this.basket = basket;
        
      });
    });
  }
  confirmOrder(): void {
    if (!this.basket) return;
    this.formSubmitted = true;

    if (!this.deliveryAddress || !this.phoneNumber || !this.paymentMethod || !this.basket || this.basket.items.length === 0) {
      console.error('Form uyğun deyl və yaxud səbətiniz boşdur');
      return;
    }

    const order: OrderDTO = {
        id: 0,
        customerId: this.customerId,  
        orderDate: new Date(),
        status: OrderStatus.Pending,
        deliveryAddress: this.deliveryAddress,
        phoneNumber: this.phoneNumber,
        orderNumber:this.orderNumber,
        paymentMethod: this.paymentMethod,
        orderDetails: this.basket.items.map(item => ({
            orderDetailId: 0,
            orderId: 0,
            bookId:item.bookId,
            bookName: '',  
            bookImageUrl: '',
            BookId: item.bookId,
            quantity: item.quantity,
            price: item.price
        }))
    };

    this.orderService.createOrder(order).subscribe(
        response => {
          console.log('Sifariş uğurla tamamlandı:', response);
          this.basketService.clearBasket(this.authService.getUserId()!).subscribe(()=>{

          })
          this.router.navigate(['/order-success'], { queryParams: { orderId: response.id } });
         
        },
        error => {
            console.error('Sifariş tamamlanamadı:', error);
            this.errorMessage = 'Sifarişiniz tamamlanmadı, zəhmət olmasa yenidən cəhd edin.';
          }
    );
}


showErrorMessage(message: string): void {
  alert(message); 
}

}

  


