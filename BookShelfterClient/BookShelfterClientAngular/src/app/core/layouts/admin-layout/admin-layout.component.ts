  import { Component } from '@angular/core';
  import { RouterModule, RouterOutlet } from '@angular/router';
  import { AdminSidebarComponent } from '../../../features/admin-panel/admin-sidebar/admin-sidebar.component';
import { AuthService } from '../../services/auth.service';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

  @Component({
    selector: 'app-admin-layout',
    standalone:true,
  imports:[
    RouterOutlet,
    AdminSidebarComponent,
    RouterModule,
   
  ],
    templateUrl: './admin-layout.component.html',
    styleUrl: './admin-layout.component.css'
  })
  export class AdminLayoutComponent {

    constructor(private authService:AuthService){}


    logout (){
      this.authService.logout();
    }

  }
