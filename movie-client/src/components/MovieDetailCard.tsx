import { useEffect, useState } from "react";
import { GetMovieDetail } from "../api";
import { GetMovieDetailRequest, MovieProvider } from "../types"; // Import types

const MovieDetail = () => {
    const [movieDetail, setMovieDetail] = useState(null);

    useEffect(() => {
        async function getMovieDetail() {
            const requestPayload: GetMovieDetailRequest = {
                MovieSourceInfos: [
                    { ProviderID: MovieProvider.CinemaWorld, MovieID: "cw0076759" },
                    { ProviderID: MovieProvider.FilmWorld, MovieID: "fw0076759" }
                ]
            };

            const data = await GetMovieDetail(requestPayload);
            setMovieDetail(data);
        }

        getMovieDetail();
    }, []);

    return (
        <div>
            <h1>Movie Details</h1>
            {movieDetail ? (
                <pre>{JSON.stringify(movieDetail, null, 2)}</pre>
            ) : (
                <p>Loading...</p>
            )}
        </div>
    );
};

export default MovieDetail;