import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { Category } from '../models/category';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {
// private apiUrl = 'http://localhost:5287/api/Category/GetAll';


private apiUrl =environment.apiUrl +'/Category';

  constructor(private http:HttpClient) { }

  getCategories():Observable<Category[]>{
    return this.http.get<{totalCount:number,category:Category[]}>(`${this.apiUrl}/GetAll`).pipe(
      map(response=>response.category)
    )
  }


}
