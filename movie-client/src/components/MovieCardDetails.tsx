import { MovieDetail } from "../types";

interface MovieCardDetailsProps{
  movieDetail: MovieDetail
}

export const MovieCardDetails: React.FC<MovieCardDetailsProps> = ({ movieDetail }) => {
  if (!movieDetail) return;

  const {
    year,
    rated,
    released,
    director,
    writer,
    actors,
    language,
    country,
    awards,
    metascore,
    rating,
    votes,
  } = movieDetail;

  return (
    <ul>
      <li>
        <strong>Year:</strong> {year}
      </li>
      <li>
        <strong>Rated:</strong> {rated}
      </li>
      <li>
        <strong>Released:</strong> {released}
      </li>
      <li>
        <strong>Director:</strong> {director}
      </li>
      <li>
        <strong>Writer:</strong> {writer}
      </li>
      <li>
        <strong>Actors:</strong> {actors}
      </li>
      <li>
        <strong>Language:</strong> {language}
      </li>
      <li>
        <strong>Country:</strong> {country}
      </li>
      {awards && (
        <li>
          <strong>Awards:</strong> {awards}
        </li>
      )}
      <li>
        <strong>Metascore:</strong> {metascore}
      </li>
      <li>
        <strong>Rating:</strong> {rating}
      </li>
      <li>
        <strong>Votes:</strong> {votes}
      </li>
    </ul>
  );
};

export default MovieCardDetails;
