using Models;
using System.Collections.Generic;

namespace Homework_Tomas_Kireilis.Interfaces
{
    public interface IReadingFromFile
    {
        IAsyncEnumerable<Transaction> ReadTransactions();
    }
}