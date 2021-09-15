import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { MoviePageRequest } from '../interfaces/movie-page-request';
import { MoviesPageResponse } from '../interfaces/movies-page-response';

@Injectable({
  providedIn: 'root',
})
export class MoviesService {
  private readonly pageMoviesURL = 'https://localhost:44321/api/Movies/page';
  private readonly moviesURL = 'https://localhost:44321/api/Movies/';

  constructor(private httpClient: HttpClient) {}

  getPage(request: MoviePageRequest): Observable<MoviesPageResponse> {
    return this.httpClient.get<MoviesPageResponse>(
      `${this.pageMoviesURL}?token=${
        !!request.token ? request.token : ''
      }&pageNumber=${request.pageNumber - 1}&pageSize=${
        request.pageSize
      }&orderBy=${request.orderBy}&ascending=${request.ascending}`
    );
  }
}
