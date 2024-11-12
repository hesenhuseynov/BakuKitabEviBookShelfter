import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AdminDashboardComponent } from './admin-dashboard/admin-dashboard.component';
import { BookManagementComponent } from './book-management/book-management.component';
import { OrderManagementComponent } from './order-management/order-management.component';
import { UserManagementComponent } from './user-management/user-management.component';
import { RouterModule } from '@angular/router';
import { AdminSidebarComponent } from './admin-sidebar/admin-sidebar.component';
import { AccessDeniedComponent } from '../access-denied/access-denied.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AddBookComponent } from './book-management/add-book/add-book.component';
import { FormsModule } from '@angular/forms';


@NgModule({
  declarations: [ 
    
    OrderManagementComponent,
    UserManagementComponent,
    AccessDeniedComponent
  ],
  imports: [
    RouterModule,
    AdminSidebarComponent  ,
    BookManagementComponent,
    AdminDashboardComponent,
    CommonModule,
    FormsModule
    // Standalone bileşeni import ediyoruz, declare etmiyoruz!
  ]
  
   

})
export class AdminPanelModule { }
