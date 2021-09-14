import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { IBaseResponse } from 'src/app/interfaces/base-response';
import { ISignupRequest } from '../interfaces/signup-request';

@Injectable({
  providedIn: 'root',
})
export class SignupService {
  private readonly URL = 'https://localhost:44321/api/Auth/Signup';

  constructor(private http: HttpClient) {}

  register(request: ISignupRequest) {
    console.log('sending signup request');
    return this.http.post<IBaseResponse>(this.URL, request).pipe(
      map((r) => {
        console.log(r);
        if (!!!r.error) {
          return null;
        }
        console.log(r.error.message);
        return r.error.message;
      })
    );
  }
}
