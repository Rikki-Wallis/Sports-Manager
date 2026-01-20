import { Component } from '@angular/core';
import { FormBuilder, Validators, ReactiveFormsModule, FormGroup } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { AbstractControl } from '@angular/forms';
import { ValidationErrors } from '@angular/forms';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [ReactiveFormsModule,
            CommonModule,
            RouterModule,
  ],
  templateUrl: './register.html',
  styleUrl: './register.scss',
})
export class Register {
  
  registerForm!: FormGroup;

  constructor(private fb: FormBuilder) {
    this.registerForm = this.fb.group({
        email: ['', [Validators.required, Validators.email]],
        password: ['', Validators.required],
        confirmPassword: ['', Validators.required]
    }, 
    { validators: this.passwordsMatchValidator});
  } 

  // Confirm Password Validator
    passwordsMatchValidator(form: AbstractControl): ValidationErrors | null {
      
      const password = form.get('password')?.value;
      const confirmPassword = form.get('confirmPassword')?.value;

      if (password && confirmPassword && password !== confirmPassword) {
        return { passwordsMismatch: true };
      }

      return null;
    }

    // Submit Function
  submit() {
    if (this.registerForm.invalid) return;
    
    console.log(this.registerForm.value);
  }
}
