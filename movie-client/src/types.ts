export enum MovieProvider {
    CinemaWorld = 0,
    FilmWorld = 1,
}
  
export interface MovieSourceInfo {
    ProviderID: MovieProvider;
    MovieID: string;
}
  
export interface GetMovieDetailRequest {
    MovieSourceInfos: MovieSourceInfo[];
}

export interface Movie {
    id: string;
    title: string;
    year: string;
    poster: string;
}