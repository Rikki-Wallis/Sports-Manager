import { Component } from '@angular/core';
import { FormBuilder, Validators, ReactiveFormsModule, FormGroup } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';


@Component({
  selector: 'app-login',
  standalone: true,
  imports: [ReactiveFormsModule,
            CommonModule,
            RouterModule
  ],
  templateUrl: './login.html',
  styleUrl: './login.scss',
})
export class Login {

    loginForm!: FormGroup;

    constructor(private fb: FormBuilder) {
      this.loginForm = this.fb.group({
        email: ['', [Validators.required, Validators.email]],
        password: ['', Validators.required]
      });
    }

    submit() {
      if (this.loginForm.invalid) return;

      console.log(this.loginForm.value);
    }
}
