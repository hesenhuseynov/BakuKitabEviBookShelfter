import { Component, OnInit } from '@angular/core';
import { OrderWithDetailsDTO } from '../../../../core/models/order/orderwithdetails';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { OrderService } from '../../../../core/services/order.service';
import { response } from 'express';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-orderdetails',
  standalone: true,
  imports: [CommonModule,RouterModule],
  templateUrl: './orderdetails.component.html',
  styleUrl: './orderdetails.component.css'
})
export class OrderdetailsComponent implements OnInit {
 orderDetails!:OrderWithDetailsDTO;

 constructor( private route:ActivatedRoute,private orderService:OrderService,private router:Router){

 }
  ngOnInit(): void {
    const orderId=this.route.snapshot.params['id'];
    this.orderService.getOrderWithDetailsById(orderId).subscribe(
      (response:OrderWithDetailsDTO)=>{
        this.orderDetails=response;
      },
      (error)=>{
        console.error('Sifraişin detalları gətirilə blmədi',error);
      }

    )
  
  }
  goBackToOrderManagement(): void {
    this.router.navigate(['/admin-panel/orders']);
  }

  



}
