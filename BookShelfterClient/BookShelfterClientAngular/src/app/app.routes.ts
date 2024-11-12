import {  Routes } from '@angular/router';
import {HomeComponent} from "./features/home/home.component";
import { BookDetailComponent } from './features/books/book-detail/book-detail.component';
import { CategoryListComponent } from './features/categories/category-list/category-list.component';
import { BookListComponent } from './features/books/book-list/book-list.component';
import { LoginComponent } from './features/auth/login/login.component';
import { EmailverificationComponent } from './features/auth/emailverification/emailverification.component';
import { RegisterComponent } from './features/auth/register/register.component';
import { ConfirmationSuccessComponent } from './features/auth/confirmation-success/confirmation-success.component';
import { ConfirmemailComponent } from './features/auth/confirmemail/confirmemail.component';
import { BasketComponent } from './features/basket/basket.component';
import { AccessDeniedComponent } from './features/access-denied/access-denied.component';
import { MainLayoutComponent } from './core/layouts/main-layout/main-layout.component';
import { AdminLayoutComponent } from './core/layouts/admin-layout/admin-layout.component';
import { AdminDashboardComponent } from './features/admin-panel/admin-dashboard/admin-dashboard.component';
import { BookManagementComponent } from './features/admin-panel/book-management/book-management.component';
import { OrderManagementComponent } from './features/admin-panel/order-management/order-management.component';
import { UserManagementComponent } from './features/admin-panel/user-management/user-management.component';
import { AdminGuard } from './core/guards/admin.guard';
import { AddBookComponent } from './features/admin-panel/book-management/add-book/add-book.component';
import { EditBookComponent } from './features/admin-panel/book-management/edit-book/edit-book.component';
import { OrderSummaryComponent } from './features/orders/components/order-summary/order-summary.component';
import { BasketGuard } from './core/guards/basket.guard';
import { OrderSuccessComponent } from './features/orders/components/order-success/order-success.component';
import { OrderdetailsComponent } from './features/admin-panel/order-management/orderdetails/orderdetails.component';

export const routes: Routes = [
  //  { path: 'user-panel', loadChildren: () => import('./features/user-panel/user-panel.module').then(m => m.UserPanelModule) },
  {
    path: '',
    component: MainLayoutComponent,
    children: [
      { path: '', component: HomeComponent },     
       { path: 'home', component: HomeComponent },
      { path: 'books/:id', component: BookDetailComponent },
      { path: 'categories', component: CategoryListComponent },
      { path: 'categories/:id/books', component: BookListComponent },
      { path: 'login', component: LoginComponent },
      { path: 'books', component: BookListComponent }, 
      { path: 'register', component: RegisterComponent },
      { path: 'basket', component: BasketComponent },
      {path:'confirmemail',component:ConfirmemailComponent},
       { path: 'confirmation-success', component: ConfirmationSuccessComponent },
          { path: 'email-verification', component: EmailverificationComponent }, 
          { path: 'orders/summary', 
            component: OrderSummaryComponent ,
            canActivate: [BasketGuard]
          },
          // { path: 'orders/details', component: OrderDetailsComponent },
          {path:'order-success',component:OrderSuccessComponent}


    ]
  },
  {
    path: 'admin-panel',
    component: AdminLayoutComponent,
    canActivate: [AdminGuard],
    children: [
      { path: 'dashboard', component: AdminDashboardComponent },
      { path: 'books', component: BookManagementComponent },
      { path: 'orders', component: OrderManagementComponent },
      { path: 'users', component: UserManagementComponent },
      { path: 'books/add', component: AddBookComponent  },
        {path:'books/edit/:id',component:EditBookComponent},
        { path: 'orders/:id', component: OrderdetailsComponent },

    ],

  },
  { path: 'access-denied', component: AccessDeniedComponent }

  //  {path:'',component:HomeComponent},
  //  {path:'home',component:HomeComponent},
  //  { path: 'books/:id', component: BookDetailComponent },
  //  { path: 'categories', component: CategoryListComponent },
  //  { path: 'categories/:id/books', component: BookListComponent },
  //  { path: 'login', component: LoginComponent },
  //  {path:'register',component:RegisterComponent},
  //  { path: 'email-verification', component: EmailverificationComponent }, 
  //   {path:'confirmemail',component:ConfirmemailComponent},
  //  { path: 'confirmation-success', component: ConfirmationSuccessComponent },
  //  {path:"basket",component:BasketComponent},
  //  { path: 'user-panel', loadChildren: () => import('./features/user-panel/user-panel.module').then(m => m.UserPanelModule) },
  //  { path: 'admin-panel', loadChildren: () => import('./features/admin-panel/admin-panel.module').then(m => m.AdminPanelModule) },
  //  { path: 'access-denied', component: AccessDeniedComponent },  // Burada AccessDeniedComponent genel rota içinde

];
