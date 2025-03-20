export enum MovieProvider {
  CinemaWorld = 0,
  FilmWorld = 1,
}
export interface GetMovieListResponse {
  movieList: MovieDetail[];
}

export interface MovieDetail {
  id?: string;
  title: string;
  year: string;
  rated: string;
  released: string;
  runtime: string;
  genre: string;
  director: string;
  writer: string;
  actors: string;
  plot: string;
  language: string;
  country: string;
  awards?: string;
  poster: string;
  metascore: string;
  rating: string;
  votes: string;
  type: string;
  prices: Price[];
}

export interface Price {
  provider: MovieProvider;
  providerName: string;
  value: number;
}
