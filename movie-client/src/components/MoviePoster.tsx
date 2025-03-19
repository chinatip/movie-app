import { useState } from "react";

interface ImageProps {
  url: string;
  altText: string;
}

export const MoviePoster: React.FC<ImageProps> = ({ url, altText }) => {
  const [imgError, setImgError] = useState<boolean>(false);

  return imgError ? (
    <div className="w-[100px] h-[153px] flex items-center justify-center border text-center border-gray-400 text-sm text-gray-500">
      Image Not Found
    </div>
  ) : (
    <img
      src={url}
      alt={altText}
      width="100"
      height="153"
      className="w-[100px] h-[153px] object-fill"
      onError={() => setImgError(true)}
    />
  );
};
