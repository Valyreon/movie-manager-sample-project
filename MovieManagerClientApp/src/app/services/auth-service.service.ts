import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  public isLoggedIn: boolean;
  public token: string;
  private readonly tokenName = 'MovieCatalogTkn';

  constructor() {
    var token = this.getToken();
    this.isLoggedIn = !!token;
    this.token = token;
  }

  public getToken(): string {
    let ca: Array<string> = document.cookie.split(';');
    let caLen: number = ca.length;
    let cookieName = `${this.tokenName}=`;
    let c: string;

    for (let i: number = 0; i < caLen; i += 1) {
      c = ca[i].replace(/^\s+/g, '');
      if (c.indexOf(cookieName) == 0) {
        return c.substring(cookieName.length, c.length);
      }
    }

    return null;
  }

  public clearToken() {
    document.cookie = '';
  }

  public setToken(token: string, remember: boolean = false) {
    let d: Date = new Date();
    var rememberDays = 90;
    d.setTime(d.getTime() + rememberDays * 24 * 60 * 60 * 1000);
    var cookie = `${this.tokenName}=${token};`;
    if (remember) {
      cookie += `expires=${d.toUTCString()};`;
    }
    document.cookie = cookie;
    this.isLoggedIn = true;
    this.token = token;
  }
}
