import { Component, OnInit } from '@angular/core';
import { ActivatedRouteSnapshot } from '@angular/router';

@Component({
  selector: 'app-movie-detail-view',
  templateUrl: './movie-detail-view.component.html',
  styleUrls: ['./movie-detail-view.component.scss']
})
export class MovieDetailViewComponent implements OnInit {

  constructor(private route: ActivatedRouteSnapshot) { }

  fullPosterPath: string;

  ngOnInit(): void {


    this.fullPosterPath = !!this.movie.coverPath
      ? 'https://localhost:44321'+ this.movie.coverPath
      : 'https://api.acomart.tv/images/video-placeholder.jpg';
  }

}
