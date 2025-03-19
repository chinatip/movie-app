import { GetMovieDetailResponse } from "../types";

interface MovieDetailProps {
    movieDetail: GetMovieDetailResponse;
}

const MovieDetail: React.FC<MovieDetailProps> = ({ movieDetail }) => {
    if (!movieDetail) return;

    return (
        <div>
            <h1>Movie Details</h1>
            <pre>{JSON.stringify(movieDetail, null, 2)}</pre>
        </div>
    );
};

export default MovieDetail;