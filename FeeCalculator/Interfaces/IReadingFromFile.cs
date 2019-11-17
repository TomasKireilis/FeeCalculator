using Models;
using System.Collections.Generic;

namespace FeeCalculator.Interfaces
{
    public interface IReadingFromFile
    {
        IAsyncEnumerable<Transaction> ReadTransactions();
    }
}