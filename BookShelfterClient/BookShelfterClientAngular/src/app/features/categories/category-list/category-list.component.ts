import { Component, OnInit } from '@angular/core';
import { Category } from '../../../core/models/category';
import { CategoryService } from '../../../core/services/category.service';
import { CommonModule } from '@angular/common';
import { Router, RouterModule } from '@angular/router';

@Component({
  selector: 'app-category-list',
  standalone: true,
  imports: [CommonModule,RouterModule],
  templateUrl: './category-list.component.html',
  styleUrl: './category-list.component.css'
})
export class CategoryListComponent  implements OnInit{
  categories:Category[]=[];


  constructor(private  categoryService:CategoryService,private router:Router){}

  ngOnInit(): void {
     this.categoryService.getCategories().subscribe((data)=>{
      this.categories=data;
     })


  }

  

}
