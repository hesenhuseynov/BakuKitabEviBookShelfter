import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { CustomerDto } from '../models/customer.dto';
import { response } from 'express';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CustomerService {
  // private baseUrl = 'http://localhost:5287/api/Customer'; // Backend API URL
  
   private  apiUrl = environment.apiUrl +'/Customer';

  constructor(private http:HttpClient) { }


  getCustomerByUserId(userId: string): Observable<CustomerDto> {
    return this.http.get<{ message: string, success: boolean, customer: CustomerDto }>(`${this.apiUrl}/${userId}`)
      .pipe(
        map(response => {
          console.log('Customer Response:', response);
          return  response.customer
        }) 
      );
  }
}
  
