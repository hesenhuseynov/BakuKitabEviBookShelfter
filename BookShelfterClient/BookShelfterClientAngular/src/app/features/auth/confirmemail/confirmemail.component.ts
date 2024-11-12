import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../../core/services/auth.service';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-confirmemail',
  standalone: true,
  imports: [CommonModule,RouterModule],
  templateUrl: './confirmemail.component.html',
  styleUrl: './confirmemail.component.css'
})
export class ConfirmemailComponent implements OnInit {

  token:string='';
  email:string='';

  confirmationError: string | null = null;
  confirmationSuccess: string | null = null;



  constructor(private authService: AuthService, private route: ActivatedRoute, private router: Router) {}


  ngOnInit(): void {
    this.route.queryParams.subscribe(params=>{
      this.token=params['token']
      this.email=params['email']
       this.confirmEmail();

    })
  }



  confirmEmail() {
    this.authService.confirmEmail(this.token, this.email).subscribe(
      () => {
        this.confirmationSuccess = 'E-poçtun doğrulanması uğurlu oldu! İndi daxil ola bilərsiniz.';
        setTimeout(() => {
          this.router.navigate(['/login']);

        }, 2000); 
      },
      error => {
        this.confirmationError = 'E-poçtun doğrulanması uğursuz oldu. Yenidən cəhd edin.';
      }
    );
  }


  

}
