import { Component, OnInit } from '@angular/core';
import { MatCard } from '@angular/material/card';
import { LoginRequest } from '../../dtos/login-request';

@Component({
  selector: 'app-login-form',
  templateUrl: './login-form.component.html',
  styleUrls: ['./login-form.component.scss']
})
export class LoginFormComponent implements OnInit {

  loginRequest: LoginRequest;

  constructor() {
    this.loginRequest = new LoginRequest();
   }

  ngOnInit(): void {
  }

}
