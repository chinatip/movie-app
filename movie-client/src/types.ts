export enum MovieProvider {
    CinemaWorld = 0,
    FilmWorld = 1,
}

// GetMovieDetail --start
export interface MovieSourceInfo {
    ProviderID: MovieProvider;
    MovieID: string;
}

export interface GetMovieDetailRequest {
    MovieSourceInfos: MovieSourceInfo[];
}

export interface GetMovieDetailResponse {
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
    providerPrices: ProviderPrice[];
}

export interface ProviderPrice {
    provider: MovieProvider;
    providerName: string;
    priceValue: number;
}

// GetMovieDetail --end
// GetMovieList --start
export interface GetMovieListResponse {
    movieList: MovieSummaryWithProviders[];
}

export interface MovieSummaryWithProviders {
    id: number;
    title: string;
    year: string;
    type: string;
    poster: string;
    providerDetails: MovieProviderDetail[];
}

export interface MovieProviderDetail {
    providerID: MovieProvider;
    movieID: string;
}
// GetMovieList --end
