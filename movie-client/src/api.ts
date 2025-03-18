import { GetMovieDetailRequest } from "./types";

export const API_URL = '/api';

export async function GetMovieList() {
  try {
    const response = await fetch(`${API_URL}/movies`);

    if (!response.ok) throw new Error('Failed to fetch movies');
        return await response.json();
    } catch (error) {
        console.error('API Error:', error);
        return [];
    }
}

export async function GetMovieDetail(requestBody: GetMovieDetailRequest) {
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