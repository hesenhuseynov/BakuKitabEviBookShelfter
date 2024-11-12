import { Component, OnInit } from '@angular/core';
import { UserService } from '../../../core/services/user.service';

@Component({
  selector: 'app-user-profile',
  standalone: true,
  imports: [],
  templateUrl: './user-profile.component.html',
  styleUrl: './user-profile.component.css'
})
export class UserProfileComponent  implements OnInit{
  user:any ;


  constructor(private userService:UserService){}

  ngOnInit(): void {
   this.userService.getUserProfile().subscribe(data=>{
    this.user==data;
   })
  }


}
