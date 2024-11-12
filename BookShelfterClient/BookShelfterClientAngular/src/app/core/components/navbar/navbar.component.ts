import { AfterViewInit, Component, OnInit } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { Router, RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [CommonModule,
    RouterModule
  
  ],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css'
})
export class NavbarComponent implements OnInit {

  userName :string| null=null;
  isAdmin: boolean = false;
  isUser: boolean = false;
  isDropdownOpen = false;

  constructor(public  authService:AuthService,private router:Router,private toastr:ToastrService){}

  ngOnInit(): void {
    this.authService.currentUser$.subscribe(userName=>{
      this.userName=userName;
      const role =this.authService.getUserRole();
      this.isAdmin=role==="Admin",
      this.isUser=role==='User'
      
    })
  }

 

  
  logout(): void {
    this.authService.logout();
    this.router.navigate(['/login']); 
    this.userName = null; 
    window.location.reload();

  }

  toggleDropdown() {
    this.isDropdownOpen = !this.isDropdownOpen;
  }

}
