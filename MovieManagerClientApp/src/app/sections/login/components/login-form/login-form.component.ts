import { LoginService } from './../../services/login.service';
import { Component, OnInit } from '@angular/core';
import { ILoginRequest } from '../../interfaces/login-request';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-login-form',
  templateUrl: './login-form.component.html',
  styleUrls: ['./login-form.component.scss']
})
export class LoginFormComponent implements OnInit {
  processing: boolean = false;
  loginRequest: ILoginRequest;
  loginForm: FormGroup;

  constructor(private loginService: LoginService, private router: Router, public fb: FormBuilder) {
    this.loginRequest = { email: "", password: "", rememberMe: false }
   }

  ngOnInit(): void {
    if(this.loginService.isLoggedIn) {
      this.router.navigate(['']);
    }

    this.loginForm = this.fb.group({
      email: [this.loginRequest.email, Validators.compose([Validators.required, Validators.email])],
      password: [this.loginRequest.password, Validators.required],
      rememberMe: [this.loginRequest.rememberMe]
    });
   }

  handleSubmitClick() {
    if(this.loginForm.invalid) {
      return;
    }

    this.processing = true;
    this.loginService.login(this.loginRequest).subscribe({
      next(response) { this.processing = false;},
      error(err) { this.processing = false; },
      complete() { this.processing = false; this.router.navigate(['']); }
    });
  }
}
