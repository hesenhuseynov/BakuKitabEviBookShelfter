import { AfterViewInit, Component } from '@angular/core';
import { AuthService } from '../../../core/services/auth.service';
import { LoginResponse } from '../../../core/models/loginresponse';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { BasketService } from '../../../core/services/basket.service';
declare var google: any;

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule,FormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})

export class LoginComponent implements AfterViewInit  {
  email = '';
  password = '';
  loginError: string | null = null;
  validationErrors: string[] = [];


  constructor(private authService: AuthService,
    private router:Router,
    private basketService:BasketService
  ) {}


 ngAfterViewInit(): void {
    if (typeof google !== 'undefined') {
      this.googleLogin();
    } else {
      console.error('Google API yüklenemedi.');
    }
  }


  onLogin() {
    this.authService.login(this.email, this.password).subscribe((response: LoginResponse) => {
      console.log('Giriş uğurlu !', response);
      if (response.token.accessToken) {
        localStorage.setItem('accessToken',response.token.accessToken);
        localStorage.setItem('refreshToken',response.token.refreshToken);
      
         
       this.router.navigate(['/home']).then(()=>{
        window.location.reload()
       })


      } 
    }, error => {      
      this.loginError = error.error.message || 'Giriş sırasında bir xəta vaş.';
      this.validationErrors = error.error.errors || [];
    });
    
  }


  // googleLogin() {
  //   google.accounts.id.initialize({
  //     client_id: '481116876345-91nqt31nkl5go3hffab10kaindap1n8g.apps.googleusercontent.com',
  //     callback: (response: any) => this.handleGoogleResponse(response) // Fonksiyon ismini düzelttik
  //   });
  //   google.accounts.id.prompt(); // Google login penceresini açar
  // }

  // ngAfterViewInit(): void {
  //   if (typeof google !== 'undefined') {
  //     this.googleLogin();
  //  } else {
  //     console.error('Google API yüklenemedi.');
  //  }
  //   }

  googleLogin() {
    google.accounts.id.initialize({
      client_id: '481116876345-91nqt31nkl5go3hffab10kaindap1n8g.apps.googleusercontent.com',
      callback: this.handleGoogleResponse.bind(this),
    });
    google.accounts.id.prompt();
  }
  
      

  
  handleGoogleResponse(response: any) {
    console.log("Google login response", response);
    const idToken = response.credential;
 
    this.authService.googleLogin(idToken).subscribe((serverResponse) => {
       console.log("Login successful", serverResponse);
       if (serverResponse && serverResponse.accessToken) {
          localStorage.setItem('accessToken', serverResponse.accessToken);
          this.router.navigate(['/home']).then(() => {
            
            window.location.reload();
        });
       } else {
          console.error('Failed to store the token.');
       }
    }, error => {
       console.error('Google login error', error);
    });
 }
 

   
}
