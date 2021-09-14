import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class CookieService {
  private loginCookieName = "MovieCatalogTkn";

  constructor() { }

  public getLoginCookie(): string {
    let ca: Array<string> = document.cookie.split(';');
    let caLen: number = ca.length;
    let cookieName = `${this.loginCookieName}=`;
    let c: string;

    for (let i: number = 0; i < caLen; i += 1) {
      c = ca[i].replace(/^\s+/g, '');
      if (c.indexOf(cookieName) == 0) {
        return c.substring(cookieName.length, c.length);
      }
    }

    return null;
  }

  public deleteCookie() {
    document.cookie = "";
  }

  public setloginCookie(token: string, remember:boolean) {
    let d: Date = new Date();
    var rememberDays = 90;
    d.setTime(d.getTime() + rememberDays * 24 * 60 * 60 * 1000);
    var cookie = `${this.loginCookieName}=${token};`;
    if(remember) {
      cookie += `expires=${d.toUTCString()};`;
    }
    document.cookie = cookie;
  }
}
