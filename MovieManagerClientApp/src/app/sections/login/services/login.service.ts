import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class LoginService {

  private readonly URL = 'https://localhost:44321/api/Auth/Login';

  constructor(private http: HttpClient) { }

  login(email: string, password: string, remember: boolean) {
    console.log("Sending login request.");
    return this.http.post(
      this.URL,
      {
        email: email,
        password: password,
        rememberMe: remember
      }
    );
  }
}
