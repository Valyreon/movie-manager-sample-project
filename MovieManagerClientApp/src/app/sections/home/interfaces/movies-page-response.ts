import { MovieListItem } from "./movie-list-item";

export interface MoviesPageResponse {
    pageNumber: number;
    pageSize: number;
    totalPages: number;
    items: MovieListItem[];
}
