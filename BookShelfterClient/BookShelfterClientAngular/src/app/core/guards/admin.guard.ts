import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { AuthService } from '../services/auth.service';

@Injectable({
  providedIn: 'root'
})
export class AdminGuard implements CanActivate {

    constructor(private authService: AuthService, private router: Router) {}

    // canActivate(): boolean {
    //   const role = this.authService.getUserRole();
    
    //   console.log('istifadeci rolu',role);
    //   if (role === 'Admin') {
    //     return true;
    //   }
    //   this.router.navigate(['/access-denied']);
    //   return false;
    // }
  
    canActivate(): boolean {
      const role = this.authService.getUserRole();
    
      if (!role) {
        // Token doğrulanmamışsa veya token yoksa, kullanıcıyı login sayfasına yönlendir.
        this.router.navigate(['/login']);
        return false;
      }
    
      if (role === 'Admin') {
        return true;
      }
    
      this.router.navigate(['/access-denied']);
      return false;
    }
}
