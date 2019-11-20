using System.Collections.Generic;

namespace Repository
{
    public interface IReadingFromFile
    {
        MerchantInformation GetMerchantDefaultValues(string merchantName);

        IAsyncEnumerable<Transaction> ReadTransactionsFromRepositoryAsync();

        IAsyncEnumerable<MerchantInformation> ReadMerchantsFromRepositoryAsync();
    }
}