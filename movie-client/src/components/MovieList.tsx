import { useEffect, useState } from 'react';
import { GetMovieList } from '../api';
import { MovieSummaryWithProviders } from '@/types';
import { MovieCard } from './MovieCard';

const MovieList = () => {
  const [movies, setMovies] = useState<MovieSummaryWithProviders[]>([]);

  useEffect(() => {
    const loadMovies = async () => {
      try {
        const data = await GetMovieList();

        setMovies(data?.movieList ?? []);
      } catch (error) {
        console.error("Error fetching movies:", error);
        setMovies([]);
      }
    };

    loadMovies();
  }, []);

  const renderMovieList = () => {
    if (movies.length === 0) return <p>No movies found.</p>;
  
    return movies.map((movie) => <MovieCard key={movie.id} movie={movie} />);
  };

  return (
    <div>
        <h1>Movie List</h1>
        {renderMovieList()}
    </div>
  );
};

export default MovieList;