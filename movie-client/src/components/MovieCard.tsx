import {
  Card,
  CardContent,
  CardDescription,
  CardHeader,
  CardTitle,
} from "@/components/ui/card";

import {
  Accordion,
  AccordionContent,
  AccordionItem,
  AccordionTrigger,
} from "@/components/ui/accordion";
import MovieCardDetails from "./MovieCardDetails";
import { MovieDetail } from "../types";
import { PriceComparisonTable } from "./PriceComparisonTable";
import { MoviePoster } from "./MoviePoster";

export const MovieCard: React.FC<{ movie: MovieDetail }> = ({ movie }) => {
  if (!movie) return;

  const { title, poster, plot } = movie;

  const renderMovieDetail = () => {
    return (
      <Accordion type="single" collapsible>
        <AccordionItem value="item-1">
          <AccordionTrigger>Movie Details</AccordionTrigger>
          <AccordionContent>
            <MovieCardDetails movieDetail={movie} />
          </AccordionContent>
        </AccordionItem>
      </Accordion>
    );
  };

  const renderContent = () => (
    <CardContent>
      {movie && <PriceComparisonTable providerPrices={movie.prices} />}
      {renderMovieDetail()}
    </CardContent>
  );

  const renderHeader = () => (
    <CardHeader>
      <div className="flex flex-col sm:flex-row">
        <div className="size-full sm:size-1 min-w-[100px] min-h-[153px] mb-4 sm:mr-4 sm:mb-0 flex justify-center">
          <MoviePoster url={poster} altText={title} />
        </div>
        <div className="size-full">
          <CardTitle className="mb-4">{title}</CardTitle>
          <CardDescription>{plot}</CardDescription>
        </div>
      </div>
    </CardHeader>
  );

  return (
    <Card className="w-full min-w-2xs sm:min-w-md sm:max-w-md lg:min-w-lg lg:max-w-lg mb-4 text-left">
      {renderHeader()}
      {renderContent()}
    </Card>
  );
};
