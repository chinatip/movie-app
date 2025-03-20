import {
  GetMovieListResponse,
} from "./types";

export const API_URL = "/api";

export async function GetMovieList(): Promise<GetMovieListResponse> {
  try {
    const response = await fetch(`${API_URL}/movies`);

    if (!response.ok) throw new Error("Failed to fetch movies");

    const data: GetMovieListResponse = await response.json();

    return data;
  } catch (error) {
    console.error("API Error:", error);

    return { movieList: [] };
  }
}
