import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginFormComponent } from './sections/login/components/login-form/login-form.component';
import { SignupFormComponent } from './sections/signup/components/signup-form/signup-form.component';

const routes: Routes = [
  { path: 'login', component: LoginFormComponent },
  { path: 'signup', component: SignupFormComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
