import { useEffect, useState } from 'react';
import { GetMovieList } from '../api';
import { Movie } from '../types';

const Movies = () => {
  const [movies, setMovies] = useState<Movie[]>([]);

  useEffect(() => {
    async function loadMovies() {
        const data = await GetMovieList();
        console.log("Fetched Movies:", data);

        if (data && data.movieList && Array.isArray(data.movieList)) {
            setMovies(data.movieList);
        } else {
            setMovies([]);
        }
    }

    loadMovies();
  }, []);

  return (
    <div>
        <h1>Movie List</h1>
        {movies.length > 0 ? (
        <ul>
            {movies.map((movie) => (
            <li key={movie.id}>
                <img src={movie.poster} alt={movie.title} width="100" />
                <p>{movie.title} ({movie.year})</p>
            </li>
            ))}
        </ul>
        ) : (
            <p>No movies found.</p>
        )}
    </div>
  );
};

export default Movies;