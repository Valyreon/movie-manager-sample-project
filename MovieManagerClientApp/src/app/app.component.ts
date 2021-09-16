import { LoginService } from './sections/login/services/login.service';
import { Component } from '@angular/core';
import { AuthService } from './services/auth-service.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'Movie Catalog';
  showMobileMenu: boolean = false;

  constructor(public authService: AuthService) {}

  toggleMobileMenu() {
    this.showMobileMenu = !this.showMobileMenu;
  }

  logOut() {
    console.log("loggin out");
    this.authService.clearToken();
  }
}
