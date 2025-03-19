import {
    Card,
    CardContent,
    CardDescription,
    CardHeader,
    CardTitle,
  } from "@/components/ui/card"

import {
    Accordion,
    AccordionContent,
    AccordionItem,
    AccordionTrigger,
  } from "@/components/ui/accordion"
import MovieCardDetails from "./MovieCardDetails";
import { useEffect, useState } from "react";
import { GetMovieDetail } from "../api";
import { MovieSummaryWithProviders, GetMovieDetailRequest, GetMovieDetailResponse, MovieProviderDetail, MovieSourceInfo } from "../types";
import { PriceComparisonTable } from "./PriceComparisonTable";
import { MoviePoster } from "./MoviePoster";

interface MovieCardProps {
  movie: MovieSummaryWithProviders;
}

export const MovieCard: React.FC<MovieCardProps> = ({ movie }) => {
  const [movieDetail, setMovieDetail] = useState<GetMovieDetailResponse | null>(null);
  const { title, poster, providerDetails } = movie;

  const mapRequest = (): GetMovieDetailRequest | null => {
      if (providerDetails.length === 0) return null;
  
      const movieSources: MovieSourceInfo[] = providerDetails.map((provider: MovieProviderDetail) => ({
          ProviderID: provider.providerID,
          MovieID: provider.movieID,
      }));
  
      return {
          MovieSourceInfos: movieSources
      };
  };

  const fetchAndUpdate = async (request: GetMovieDetailRequest | null) => {
      if (!request) return

      try {
          const data = await GetMovieDetail(request);
          
          setMovieDetail(data ?? null);
      } catch (error) {
          console.error("Error fetching movie details:", error);

          setMovieDetail(null);
      }
  }

  useEffect(() => {
      const loadMovieDetail = async () => {
          const request = mapRequest();
          fetchAndUpdate(request);
      };

      loadMovieDetail();
  }, []);

  const renderMovieDetail = () => {
    if (!movieDetail) return

    return (
      <Accordion type="single" collapsible className="min-w-2xs sm:min-w-md sm:max-w-md lg:min-w-lg lg:max-w-lg">
        <AccordionItem value="item-1">
          <AccordionTrigger>Movie Details</AccordionTrigger>
          <AccordionContent>
            <MovieCardDetails movieDetail={movieDetail} />
          </AccordionContent>
        </AccordionItem>
      </Accordion>
    )
  }

  const renderContent = () => (
    <CardContent>
      {movieDetail && <PriceComparisonTable providerPrices={movieDetail.providerPrices} />}
      {renderMovieDetail()}
    </CardContent>
  )

  const renderHeader = () => (
    <CardHeader className="min-w-2xs sm:min-w-md sm:max-w-md lg:min-w-lg lg:max-w-lg">
      <div className="flex flex-col sm:flex-row">
        <div className="size-full sm:size-1 min-w-[100px] min-h-[153px] mb-4 sm:mr-4 sm:mb-0 flex justify-center">
          <MoviePoster url={poster} altText={title} />
        </div>
        <div className="size-full">
          <CardTitle className="mb-4">{title}</CardTitle>
          {movieDetail && movieDetail.plot && <CardDescription>{movieDetail.plot}</CardDescription>}
        </div>
      </div>
    </CardHeader>
  )

  return (
    <Card className="text-left mb-4">
      {renderHeader()}
      {renderContent()}
    </Card>
    )
  }
  