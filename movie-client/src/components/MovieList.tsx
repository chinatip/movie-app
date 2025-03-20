import { useEffect, useState } from "react";
import { MovieDetail } from "@/types";
import { MovieCard } from "./MovieCard";
import { GetMovieList } from "@/api";

const MovieList = () => {
  const [movies, setMovies] = useState<MovieDetail[]>([]);

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
      <h1 className="text-2xl font-bold sm:text-3xl">
        Find the Best Movie Deals
      </h1>
      <div className="mt-6">{renderMovieList()}</div>
    </div>
  );
};

export default MovieList;
