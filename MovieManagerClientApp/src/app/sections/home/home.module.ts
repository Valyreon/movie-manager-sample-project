import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MovieDetailViewComponent } from './components/movie-detail-view/movie-detail-view.component';
import { MoviesPagedListComponent } from './components/movies-paged-list/movies-paged-list.component';
import { MovieListItemComponent } from './components/movie-list-item/movie-list-item.component';
import { StarRaterComponent } from './components/star-rater/star-rater.component';
import { AngularMaterialModule } from 'src/app/material.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';



@NgModule({
  declarations: [
    MovieDetailViewComponent,
    MoviesPagedListComponent,
    MovieListItemComponent,
    StarRaterComponent,
  ],
  imports: [
    CommonModule,
    AngularMaterialModule,
    FormsModule,
    RouterModule,
    ReactiveFormsModule,
  ],
})
export class HomeModule {}
