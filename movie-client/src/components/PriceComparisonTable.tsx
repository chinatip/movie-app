import { ProviderPrice } from "@/types";

export const PriceComparisonTable: React.FC<{ providerPrices: ProviderPrice[] }> = ({ providerPrices }) => {
    if (!providerPrices || providerPrices.length == 0) return ;

    return (
      <table className="w-full border border-gray-300">
        <thead>
          <tr className="bg-gray-100">
            {providerPrices.map((priceItem, index) => (
              <th key={index} className="px-4 py-2 text-sm text-center font-medium border border-gray-300">
                {priceItem.providerName}
              </th>
            ))}
          </tr>
        </thead>
        <tbody>
          <tr>
            {providerPrices.map((priceItem, index) => (
              <td key={index} className="px-4 py-2 text-center border border-gray-300">
                {priceItem.priceValue}
              </td>
            ))}
          </tr>
        </tbody>
      </table>
    );
  };
  