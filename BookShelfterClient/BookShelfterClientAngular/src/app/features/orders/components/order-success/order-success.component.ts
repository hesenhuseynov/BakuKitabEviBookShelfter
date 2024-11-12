import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-order-success',
  standalone: true,
  imports: [],
  templateUrl: './order-success.component.html',
  styleUrl: './order-success.component.css'
})
export class OrderSuccessComponent implements OnInit {
  orderNumber!: string;

  constructor(private router: Router) {}

  ngOnInit(): void {
    this.orderNumber = '12345'; // Bu numara dinamik olarak gelmeli.
  }

  goToHomePage() {
    this.router.navigate(['/home']);
  }
}
