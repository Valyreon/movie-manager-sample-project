import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { LoginRequest } from '../dtos/login-request';
import { LoginResponse } from '../dtos/login-response';

@Injectable({
  providedIn: 'root',
})
export class LoginService {
  private readonly URL = 'https://localhost:44321/api/Auth/Login';

  constructor(private http: HttpClient) {}

  login(request: LoginRequest): any {
    console.log('Sending login request.');
    return this.http.post(this.URL, request);
  }

  private setCookie(token: string, remember: boolean = true) {
    let d: Date = new Date();
    let expireDays = 90;
    d.setTime(d.getTime() + expireDays * 24 * 60 * 60 * 1000);
    let expires: string = 'expires=' + d.toUTCString();
    document.cookie =
      'MovieCatalogLoginTkn=' +
      token +
      '; ' +
      expires + ';';
  }
}
