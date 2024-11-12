import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  // private baseUrl ='http://localhost:5287/api/Users'

   private apiUrl  =environment.apiUrl+'/Users';
   

  constructor(private http:HttpClient) {}



  getUserProfile(): Observable<any> {
    return this.http.get(`${this.apiUrl}/profile`);
  }

  getUserPanel(): Observable<any> {
    return this.http.get(`${this.apiUrl}/panel`);
  }

  getAllUsers(): Observable<any> {
    return this.http.get(this.apiUrl);
  }
}
