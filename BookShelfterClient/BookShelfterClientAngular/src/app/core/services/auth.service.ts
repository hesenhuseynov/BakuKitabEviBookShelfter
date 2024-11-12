import { Injectable, Inject, PLATFORM_ID } from '@angular/core';
import { isPlatformBrowser } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators'; 
import { LoginResponse } from '../models/loginresponse';
import { RegisterModel } from '../models/registermodel';
import { BasketService } from './basket.service';
import { ActivatedRoute, Route, Router } from '@angular/router';
 import { jwtDecode } from "jwt-decode";
import { environment } from '../../../environments/environment';



@Injectable({
  providedIn: 'root'
})
export class AuthService {
  public currentUserSubject: BehaviorSubject<string | null>;
  public currentUser$: Observable<string | null>;
   private isLoggedInSubject = new BehaviorSubject<boolean>(false);
   isLoggedIn$: Observable<boolean> = this.isLoggedInSubject.asObservable();

   private baseUrl = environment.apiUrl+'/Auth';
  // private apiUrl = 'http://localhost:5287/api/Auth';

  constructor(
    private http: HttpClient,
    @Inject(PLATFORM_ID) private platformId: Object,
    private  basketService:BasketService,
    private router:Router,
  ) {
    const userName = this.getUserName();
    this.currentUserSubject = new BehaviorSubject<string | null>(userName);
    this.currentUser$ = this.currentUserSubject.asObservable();
    if (isPlatformBrowser(this.platformId)) {
      const token = this.getToken();
      // this.isLoggedInSubject.next(!!token);
    }
  }

  login(userName: string, password: string): Observable<LoginResponse> {
    return this.http.post<LoginResponse>(`${this.baseUrl}/login`, { userName, password }).pipe(
      tap(response => {
        if (isPlatformBrowser(this.platformId)) {
          if (response.token.accessToken) {
            localStorage.setItem('accessToken', response.token.accessToken);
            localStorage.setItem('refreshToken', response.token.refreshToken);
      
            this.currentUserSubject.next(this.getUserName());

            const userId = this.getUserId(); 
            if (userId) {
              this.basketService.loadBasket(userId); 
            }
            this.router.navigate(['/home']);

          }
        }
      })
    );
  }


  googleLogin(idToken: string): Observable<any> {
    return this.http.post('http://localhost:5287/api/Auth/google-login', { idToken });
  }




  getUserName(): string | null {
    if (isPlatformBrowser(this.platformId)) {
      const token = localStorage.getItem('accessToken');
      if (token) {
        const payload: any =jwtDecode(token);
        return payload.unique_name || null;
      }
    }
    return null;
  }

  getUserRole(): string | null {
    const token = this.getToken(); 
    if (token) {
      const payload:any=jwtDecode(token)
      return payload.role || null;
    }
    return null;
  }







  logout(): void {
    if (isPlatformBrowser(this.platformId)) {
      localStorage.removeItem('accessToken');
      localStorage.removeItem('refreshToken');

      this.isLoggedInSubject.next(false);
      this.currentUserSubject.next(null);
       this.basketService.resetBasket();
    }
  }
  checkLoginStatus(): void {
    if (isPlatformBrowser(this.platformId)) {
      const token = localStorage.getItem('accessToken');
      if (token) {
        this.isLoggedInSubject.next(true);
      }
    }
  }
  register(registerData: RegisterModel): Observable<any> {
    return this.http.post(`${this.baseUrl}/register`, registerData);
  }

  confirmEmail(token: string, email: string): Observable<any> {
    return this.http.post(`${this.baseUrl}/confirmemail`, { token, email });
  }

  saveToken(token: string): void {
    if (isPlatformBrowser(this.platformId)) {
      localStorage.setItem('accessToken', token);
    }
  }

  getToken(): string | null { 
    if (isPlatformBrowser(this.platformId)) {
      return localStorage.getItem('accessToken');
    }

    return null;
  }

  getUserId(): string | null {
    const token = this.getToken();  
    if (token) {
      const payload: any = jwtDecode(token);
      return payload.nameid; 
    }
    return null;
  }



  getUserId$(): Observable<string | null> {
    return new Observable(observer => {
      const userId = this.getUserId(); 
      observer.next(userId); 
      observer.complete(); 
    });
  }
  


  
  }



