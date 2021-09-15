import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { debounceTime, distinctUntilChanged, tap } from 'rxjs/operators';
import { fadeInAnimation } from 'src/app/animations/fade-in-animation';
import { AuthService } from 'src/app/services/auth-service.service';
import { MovieListItem } from '../../interfaces/movie-list-item';
import { MoviesService } from '../../services/movies.service';
import { RatingsService } from '../../services/ratings.service';

@Component({
  selector: 'app-movies-paged-list',
  templateUrl: './movies-paged-list.component.html',
  styleUrls: ['./movies-paged-list.component.scss'],
  animations: [fadeInAnimation],
  host: { '[@fadeInAnimation]': '' },
})
export class MoviesPagedListComponent implements OnInit {
  ascending: boolean = false;
  pageNumber: number = 1;
  canLoadMore: boolean = true;
  readonly pageSize: number = 4;
  movies: MovieListItem[];

  filterForm: FormGroup = this.fb.group({
    token: [''],
    orderBy: ['rating', Validators.required],
  });

  constructor(
    private movieService: MoviesService,
    private authService: AuthService,
    public fb: FormBuilder
  ) {}

  ngOnInit(): void {
    this.updateList();

    this.filterForm.controls.token.valueChanges
      .pipe(
        debounceTime(300),
        distinctUntilChanged(),
        tap((a) => {
          this.updateList();
        })
      )
      .subscribe();

    this.filterForm.controls.orderBy.valueChanges
      .pipe(
        tap((a) => {
          this.updateList();
        })
      )
      .subscribe();
  }

  updateList(loadMore: boolean = false) {
    if(!loadMore) {
      this.pageNumber = 1;
    }

    var request = {
      token: this.filterForm.controls.token.value,
      pageNumber: this.pageNumber,
      pageSize: this.pageSize,
      orderBy: this.filterForm.controls.orderBy.value,
      ascending: this.ascending,
    };

    this.movieService
      .getPage(request)
      .pipe(
        tap((r) => {
          if (loadMore) {
            console.log("concatinating");
            this.movies = this.movies.concat(r.items);
          } else {
            this.movies = r.items;
            this.pageNumber = 1;
          }
          this.canLoadMore = this.pageNumber < r.totalPages;
        })
      )
      .subscribe();
  }

  loadNextPage() {
    this.pageNumber++;
    this.updateList(true);
  }

  handleSortDirectionButtonClick() {
    this.ascending = !this.ascending;
    this.updateList();
  }
}
