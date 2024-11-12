import { Component, OnInit } from '@angular/core';
import { OrderService } from '../../../core/services/order.service';
import { OrderDTO } from '../../../core/models/order/orderDto';
import { Router } from '@angular/router';
import { Order } from '../../../core/models/order/order';
import { OrderStatus } from '../../../core/models/order/orderstatus';

@Component({
  selector: 'app-order-management',
  templateUrl: './order-management.component.html',
  styleUrl: './order-management.component.css'
})
export class OrderManagementComponent implements OnInit{
  orders: OrderDTO[] = [];
  constructor(private orderService:OrderService, private router:Router){

  }

  ngOnInit(): void {
    this.getOrders(); 
  }




  getOrders(): void {
    this.orderService.getAllOrders().subscribe(
      (response: OrderDTO[]) => {
        this.orders = response;
        console.log('Gelen Siparişler:', this.orders);  // Console'da siparişleri görmek için
      },
      (error) => {
        console.error('Siparişler getirilemedi:', error);
      }
    );
  }


  viewOrderDetails(orderId: number): void {
    this.router.navigate(['/admin-panel/orders', orderId]); 
  }



  getOrderStatusLabel(status: OrderStatus): string {
    switch (status) {
      case OrderStatus.Pending:
        return 'Gözləyir';
      case OrderStatus.Confirmed:
        return 'Rəsmiləşdi';
      case OrderStatus.Shipped:
        return 'Yola çıxdı';
      case OrderStatus.Delivered:
        return 'Təslim  Eildi';
      case OrderStatus.Canceled:
        return 'Ləğv edildi';
      default:
        return 'Bilinmir';
    }

}
}