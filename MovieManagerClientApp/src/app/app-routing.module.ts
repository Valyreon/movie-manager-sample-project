import { MovieDetailViewComponent } from './sections/home/components/movie-detail-view/movie-detail-view.component';
import { animate, style, transition, trigger } from '@angular/animations';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { MoviesPagedListComponent } from './sections/home/components/movies-paged-list/movies-paged-list.component';
import { LoginFormComponent } from './sections/login/components/login-form/login-form.component';
import { SignupFormComponent } from './sections/signup/components/signup-form/signup-form.component';

const routes: Routes = [
  { path: 'login', component: LoginFormComponent },
  { path: 'signup', component: SignupFormComponent },
  { path: 'home', component: MoviesPagedListComponent },
  { path: '', redirectTo: 'home', pathMatch: 'full' },
  { path: 'movie/:id', component: MovieDetailViewComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
