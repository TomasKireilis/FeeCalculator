using System.Collections.Generic;

namespace Repository
{
    public interface IReadingFromFile
    {
        MerchantInformation GetMerchantDefaultValues(string merchantName);

        IAsyncEnumerable<Transaction> ReadTranslationsFromRepositoryAsync();

        IAsyncEnumerable<MerchantInformation> ReadMerchantsFromRepositoryAsync();
    }
}