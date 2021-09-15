export interface MoviePageRequest {
  token: string ;
  pageNumber: number;
  pageSize: number;
  orderBy: string;
  ascending: boolean;
}