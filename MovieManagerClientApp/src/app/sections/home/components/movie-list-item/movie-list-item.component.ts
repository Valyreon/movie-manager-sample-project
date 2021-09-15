import { Component, Input, OnInit } from '@angular/core';
import { MovieListItem } from '../../interfaces/movie-list-item';

@Component({
  selector: 'app-movie-list-item',
  templateUrl: './movie-list-item.component.html',
  styleUrls: ['./movie-list-item.component.scss'],
})
export class MovieListItemComponent implements OnInit {

  @Input() movie: MovieListItem;
  fullPosterPath: string;

  constructor() {}

  ngOnInit(): void {
    this.fullPosterPath = !!this.movie.coverPath
      ? 'https://localhost:44321'+ this.movie.coverPath
      : 'https://api.acomart.tv/images/video-placeholder.jpg';
  }


}
