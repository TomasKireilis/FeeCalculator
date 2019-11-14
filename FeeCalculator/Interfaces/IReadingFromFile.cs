using Models;
using System.Collections.Generic;

namespace Homework_Tomas_Kireilis.Interfaces
{
    public interface IReadingFromFile
    {
        IEnumerable<Transaction> ReadTransactions();

        List<Merchant> ReadSpecialMerchants();
    }
}