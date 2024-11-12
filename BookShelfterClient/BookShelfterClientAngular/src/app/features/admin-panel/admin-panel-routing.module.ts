import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AdminGuard } from '../../core/guards/admin.guard';
import { AdminDashboardComponent } from './admin-dashboard/admin-dashboard.component';
import { BookManagementComponent } from './book-management/book-management.component';
import { OrderManagementComponent } from './order-management/order-management.component';
import { UserManagementComponent } from './user-management/user-management.component';
import { AccessDeniedComponent } from '../access-denied/access-denied.component';
import { AddBookComponent } from './book-management/add-book/add-book.component';


const routes: Routes = [
  // {
  //   path: '',
  //   component: AdminDashboardComponent,
  //   children: [
  //     { path: 'books', component: BookManagementComponent },
  //     { path: 'books/add', component: AddBookComponent },
  //   ],
  // },
];
@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AdminPanelRoutingModule { }
