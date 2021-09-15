import { BaseEntity } from "./base-entity.model";
import { Review } from "./review.model";

export class Movie extends BaseEntity {
    title: string;
    description: string;
    coverPath: string;
    releaseYear: number;
    actors: string[];
    reviews: Review[];
    averageRating: number;
}
