import { GetMovieDetailResponse } from "../types";

interface MovieDetailProps {
    movieDetail: GetMovieDetailResponse;
}

const MovieDetail: React.FC<MovieDetailProps> = ({ movieDetail }) => {
    if (!movieDetail) return;

    const { year, director, writer, actors, language, country, awards, metascore, rating, votes } = movieDetail

    return (
        <ul>
            <li><strong>Year:</strong> {year}</li>
            <li><strong>Director:</strong> {director}</li>
            <li><strong>Writer:</strong> {writer}</li>
            <li><strong>Actors:</strong> {actors}</li>
            <li><strong>Language:</strong> {language}</li>
            <li><strong>Country:</strong> {country}</li>
            {awards && <li><strong>Awards:</strong> {awards}</li>}
            <li><strong>Metascore:</strong> {metascore}</li>
            <li><strong>Rating:</strong> {rating}</li>
            <li><strong>Votes:</strong> {votes}</li>
        </ul>
    );
};

export default MovieDetail;