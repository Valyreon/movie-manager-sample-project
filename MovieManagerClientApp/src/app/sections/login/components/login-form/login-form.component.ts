import { LoginService } from './../../services/login.service';
import { Component, OnInit } from '@angular/core';
import { ILoginRequest } from '../../interfaces/login-request';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { fadeInAnimation } from 'src/app/animations/fade-in-animation';
import { AuthService } from 'src/app/services/auth-service.service';
import { tap } from 'rxjs/operators';

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
  error: string = null;

  constructor(
    private loginService: LoginService,
    private authService: AuthService,
    private router: Router,
    public fb: FormBuilder
  ) {}

  ngOnInit(): void {
    if (this.authService.isLoggedIn) {
      this.router.navigate(['']);
    }

    this.loginForm = this.fb.group({
      email: ['', Validators.compose([Validators.required, Validators.email])],
      password: ['', Validators.required],
      rememberMe: [false],
    });
  }

  handleSubmitClick() {
    this.error = null;
    if (this.loginForm.invalid) {
      return;
    }

    var loginRequest = {
      email: this.loginForm.controls.email.value,
      password: this.loginForm.controls.password.value,
      rememberMe: this.loginForm.controls.rememberMe.value,
    };

    this.loginService.login(loginRequest).subscribe({
      next: (response) => {},
      error : (err) => {
        console.log('Logging error.');
        console.log(err);
        console.log(err.error);
        this.error= err.error.Error.message;
      },
      complete: () => {
        this.router.navigate(['']);
      },
    });
  }
}
