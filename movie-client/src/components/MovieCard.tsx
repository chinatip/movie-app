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
import MovieDetail from "./MovieDetail";
import { useEffect, useState } from "react";
import { GetMovieDetail } from "../api";
import { MovieSummaryWithProviders, GetMovieDetailRequest, GetMovieDetailResponse, MovieProviderDetail, MovieSourceInfo } from "../types";

interface MovieCardProps {
  movie: MovieSummaryWithProviders;
}

export const MovieCard: React.FC<MovieCardProps> = ({ movie }) => {
  const [movieDetail, setMovieDetail] = useState<GetMovieDetailResponse | null>(null);
  const { id, title, poster, year, providerDetails } = movie;

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

  const renderImage = () => {
    const [imgError, setImgError] = useState(false);
    return imgError ? (
        <div 
            className="w-[100px] h-[153px] flex items-center justify-center border text-center border-gray-400 text-sm text-gray-500"
        >
            Image Not Found
        </div>
    ) : (
        <img
            src={poster} 
            alt={title} 
            width="100" 
            height="153"
            onError={() => setImgError(true)}
        />
    );
  }

  const renderMovieDetail = () => {
    if (!movieDetail) return

    return (
      <Accordion type="single" collapsible className="min-w-2xs sm:min-w-md sm:max-w-md lg:min-w-lg lg:max-w-lg">
        <AccordionItem value="item-1">
          <AccordionTrigger>MovieDetails</AccordionTrigger>
          <AccordionContent>
            <MovieDetail movieDetail={movieDetail} />
          </AccordionContent>
        </AccordionItem>
      </Accordion>
    )
  }

  const renderPriceList = () => {
    if (!movieDetail) return
    const { providerPrices } = movieDetail;
    return (
      <>
        <p><strong>Price</strong></p>
        {providerPrices.map(priceItem => <p>{priceItem.providerName}: {priceItem.priceValue}</p>)}
      </>
    )
  }

  const renderContent = () => (
    <CardContent>
      {renderPriceList()}
      {renderMovieDetail()}
    </CardContent>
  )

  const renderHeader = () => (
    <CardHeader className="min-w-2xs sm:min-w-md sm:max-w-md lg:min-w-lg lg:max-w-lg">
      <div className="flex flex-col sm:flex-row">
        <div className="size-full min-w-[100px] min-h-[153px] mb-4 sm:mr-4 sm:mb-0 flex justify-center">
          {renderImage()}
        </div>
        <div>
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
  