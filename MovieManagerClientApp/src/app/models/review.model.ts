import { BaseEntity } from "./base-entity.model";

export class Review extends BaseEntity {
    rating: number;
    text: string;
    ratedBy: string;
    ratedWhen: string;
}
