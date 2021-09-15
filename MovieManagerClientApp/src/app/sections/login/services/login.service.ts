import { ILoginResponse } from './../interfaces/login-response';
import { ILoginRequest } from './../interfaces/login-request';
import { HttpClient, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map, tap } from 'rxjs/operators';
import { AuthService } from 'src/app/services/auth-service.service';

@Injectable({
  providedIn: 'root',
})
export class LoginService {
  private readonly URL = 'https://localhost:44321/api/Auth/Login';

  constructor(private http: HttpClient, private authService: AuthService) {
  }

  login(request: ILoginRequest): Observable<string> {
    console.log("sending login request");
    return this.http.post<ILoginResponse>(this.URL, request).pipe(
      map(r => {
        console.log(r);
        if(!!!r.error) {
          this.authService.setToken(r.token, request.rememberMe);
          return null;
        }
        console.log(r.error.message);
        return r.error.message;
      })
    );

    /*this.http.post<ILoginResponse>(this.URL, request).subscribe({
      next(response) { this.cookieService.setloginCookie(response.token, this.loginRequest.rememberMe) },
      error(err) { console.log("Logging error."); console.log(err); },
      complete() { }
    });*/
  }
}
