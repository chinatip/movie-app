import { GetMovieDetailRequest, GetMovieDetailResponse, GetMovieListResponse } from "./types";

export const API_URL = '/api';

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

export async function GetMovieDetail(requestBody: GetMovieDetailRequest): Promise<GetMovieDetailResponse | null> {
    try {
        const response = await fetch(`${API_URL}/movie`, {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify(requestBody)
        });

        if (!response.ok) throw new Error('Failed to fetch movie details');

        return await response.json();
    } catch (error) {
        console.error('API Error:', error);
        return null;
    }
}