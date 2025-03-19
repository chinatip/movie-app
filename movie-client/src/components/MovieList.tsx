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
        <h1 className="text-3xl font-bold font-stretch-50%">Find the Best Movie Deals</h1>
        <div className="mt-6">
          {renderMovieList()}
        </div>
    </div>
  );
};

export default MovieList;