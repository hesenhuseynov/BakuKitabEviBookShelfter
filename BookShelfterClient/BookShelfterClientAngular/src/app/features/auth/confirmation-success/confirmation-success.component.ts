import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from '../../../core/services/auth.service';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-confirmation-success',
  standalone: true,
  imports: [],
  templateUrl: './confirmation-success.component.html',
  styleUrl: './confirmation-success.component.css'
})
export class ConfirmationSuccessComponent implements OnInit {
  confirmationMessage: string = 'Emailiniz uğurla  doğrulandı Ana səhifəyə yönləndirilirsiniz...';


  constructor(
    private router: Router,
    private route:ActivatedRoute,
    private authService: AuthService
  ) {}

 
  ngOnInit(): void {
    this.route.queryParams.subscribe(params => {
      const token = params['token']; 
      if (token) {
        this.authService.saveToken(token);
        this.authService.currentUserSubject.next(this.authService.getUserName()); 
        setTimeout(() => {
          this.router.navigate(['/home']); 
        }, 2000);
      } else {
        this.confirmationMessage = 'Email confirmation failed. Redirecting to error page...';
        setTimeout(() => {
          this.router.navigate(['/error']);
        }, 2000);
      }
    });
  }
}

