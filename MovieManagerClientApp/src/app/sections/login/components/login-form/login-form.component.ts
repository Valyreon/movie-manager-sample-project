import { LoginService } from './../../services/login.service';
import { Component, OnInit } from '@angular/core';
import { ILoginRequest } from '../../interfaces/login-request';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { fadeInAnimation } from 'src/app/animations/fade-in-animation';

@Component({
  selector: 'app-login-form',
  templateUrl: './login-form.component.html',
  styleUrls: ['./login-form.component.scss'],
  animations: [fadeInAnimation],
  host: { '[@fadeInAnimation]': '' },
})
export class LoginFormComponent implements OnInit {
  processing: boolean = false;
  loginForm: FormGroup;

  constructor(
    private loginService: LoginService,
    private router: Router,
    public fb: FormBuilder
  ) {}

  ngOnInit(): void {
    if (this.loginService.isLoggedIn) {
      this.router.navigate(['']);
    }

    this.loginForm = this.fb.group({
      email: ['', Validators.compose([Validators.required, Validators.email])],
      password: ['', Validators.required],
      rememberMe: [false],
    });
  }

  handleSubmitClick() {
    if (this.loginForm.invalid) {
      return;
    }

    this.processing = true;

    var loginRequest = {
      email: this.loginForm.controls.email.value,
      password: this.loginForm.controls.password.value,
      rememberMe: this.loginForm.controls.rememberMe.value,
    };

    this.loginService.login(loginRequest).subscribe({
      next(response) {
        this.processing = false;
      },
      error(err) {
        this.processing = false;
      },
      complete() {
        this.processing = false;
        this.router.navigate(['']);
      },
    });
  }
}
