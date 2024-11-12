import { Component } from '@angular/core';
import { UserService } from '../../../core/services/user.service';

@Component({
  selector: 'app-user-dashboard',
  standalone: true,
  imports: [],
  templateUrl: './user-dashboard.component.html',
  styleUrl: './user-dashboard.component.css'
})
export class UserDashboardComponent {
  panelData:any;


  constructor(private userService: UserService) { }

  ngOnInit(): void {
    this.userService.getUserPanel()
      .subscribe(data => {
        this.panelData = data;
      });

      
  }

}
