import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { LoginService } from 'src/app/sections/login/services/login.service';
import { ISignupRequest } from '../../interfaces/signup-request';
import { SignupService } from '../../services/signup.service';

@Component({
  selector: 'app-signup-form',
  templateUrl: './signup-form.component.html',
  styleUrls: ['./signup-form.component.scss'],
})
export class SignupFormComponent implements OnInit {
  processing: boolean = false;
  signupForm: FormGroup;

  constructor(
    private signupService: SignupService,
    private loginService: LoginService,
    private router: Router,
    public fb: FormBuilder
  ) {

  }

  ngOnInit(): void {
    if (this.loginService.isLoggedIn) {
      this.router.navigate(['']);
    }

    this.signupForm = this.fb.group({
      email: [
        "",
        Validators.compose([Validators.required, Validators.email]),
      ],
      password: ["", Validators.required],
      confirmPassword: ["", Validators.required],
      username: ["", Validators.required],
      about: [""],
      isPrivate: [true]
    });
  }

  handleSubmitClick() {
    if (this.signupForm.invalid) {
      return;
    }

    if (
      this.signupForm.controls.password.value !=
      this.signupForm.controls.confirmPassword.value
    ) {
      return;
    }

    var signupRequest = {
      email: this.signupForm.controls.email.value,
      username: this.signupForm.controls.username.value,
      about: this.signupForm.controls.about.value,
      password: this.signupForm.controls.password.value,
      isPrivate: this.signupForm.controls.isPrivate.value,
    };

    console.log(signupRequest);

    this.processing = true;
    this.signupService.register(signupRequest).subscribe({
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
