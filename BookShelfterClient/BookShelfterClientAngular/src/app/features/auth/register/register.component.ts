import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { AuthService } from '../../../core/services/auth.service';
import { Router } from '@angular/router';
import { RegisterModel } from '../../../core/models/registermodel';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-register',
  standalone:true,
  imports:[CommonModule,ReactiveFormsModule],
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  registerForm: FormGroup = this.fb.group({});
    registrationError: string | null = null;
  registrationSuccess: string | null = null;

  constructor(private fb: FormBuilder, private authService: AuthService, private router: Router) {}

  ngOnInit(): void {
    this.registerForm = this.fb.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      userName: ['', [Validators.required, Validators.minLength(3)]],
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]],
      passwordConfirm: ['', Validators.required]
    }, { validator: this.passwordMatchValidator });
  }

  passwordMatchValidator(form: FormGroup) {
    const password = form.get('password')?.value;
    const confirmPassword = form.get('passwordConfirm')?.value;
    
    return password === confirmPassword ? null : { passwordMismatch: true };
  }

  onRegister(): void {

    this.registerForm.markAllAsTouched();
    if (this.registerForm.invalid) {
      return;
    }

    const registerData: RegisterModel = {
      userName: this.registerForm.value.userName,
      email: this.registerForm.value.email,
      password: this.registerForm.value.password,
      nameSurname: `${this.registerForm.value.firstName} ${this.registerForm.value.lastName}`,
      passwordConfirm: this.registerForm.value.passwordConfirm
    };
    
    this.authService.register(registerData).subscribe(
      () => {
        this.registrationSuccess = 'Qeydiyyat uğurlu oldu! E-poçt ünvanınızı doğrulamaq üçün linkə klikləyin.';
        this.router.navigate(['/email-verification']);
      },
      error => {
        // this.registrationError = error.error.message || 'Qeydiyyat zamanı xəta baş verdi.';

        this.registrationError = "Xəta baş verdi. Daha sonra yenidən cəhd edin.";
      }
    );
  }
}
