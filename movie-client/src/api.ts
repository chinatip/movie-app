export const API_URL = '/api';

export async function fetchMovies() {
  try {
    const response = await fetch(`${API_URL}/movies`);

    if (!response.ok) throw new Error('Failed to fetch movies');
        return await response.json();
    } catch (error) {
        console.error('API Error:', error);
        return [];
    }
}

export async function fetchMovieDetail() {
    try {
        const response = await fetch(`${API_URL}/movie`);

        if (!response.ok) throw new Error('Failed to fetch movie details');

        return await response.json();
    } catch (error) {
        console.error('API Error:', error);
        return null;
    }
}