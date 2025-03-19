import {
    Card,
    CardContent,
    CardDescription,
    CardFooter,
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

  const renderMovieDetail = () => {
    if (!movieDetail) return

    return (
      <Accordion type="single" collapsible>
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
        {providerPrices.map(priceItem => <p>Provider: {priceItem.providerName}, price: {priceItem.priceValue}</p>)}
      </>
    )
  }

  const renderContent = () => {
    return (
      <CardContent key={id}>
        <img src={poster} alt={title} width="100" />
        {renderPriceList()}
        {renderMovieDetail()}
      </CardContent>
    )
  }

  return (
    <Card>
      <CardHeader>
        <CardTitle>{title}</CardTitle>
        <CardDescription>{year}</CardDescription>
      </CardHeader>
      {renderContent()}
      <CardFooter>
        <p>Card Footer</p>
      </CardFooter>
    </Card>
    )
  }
  