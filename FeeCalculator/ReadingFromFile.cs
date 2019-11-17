using Homework_Tomas_Kireilis.Interfaces;
using Models;
using Models.Merchants;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Homework_Tomas_Kireilis
{
    public class ReadingFromFile : IReadingFromFile
    {
        private readonly string _transactionPath;
        private readonly string _specialMerchantsPath;

        public ReadingFromFile(string transactionPath, string specialMerchantsPath)
        {
            _transactionPath = transactionPath;
            _specialMerchantsPath = specialMerchantsPath;
        }

        public async IAsyncEnumerable<Transaction> ReadTransactions()
        {
            string line;
            StreamReader file = new StreamReader(_transactionPath);
            while ((line = await file.ReadLineAsync()) != null)
            {
                var lineObjects = line.Split(' ').ToList();
                lineObjects = ClearInput(lineObjects);
                yield return new Transaction
                {
                    Date = DateTimeOffset.Parse(lineObjects[0]),
                    MerchantName = lineObjects[1],
                    Amount = decimal.Parse(lineObjects[2])
                };
            }

            file.Close();
        }

        private List<string> ClearInput(List<string> values)
        {
            var returnValues = new List<string>();
            for (int i = 0; i < values.Count; i++)
            {
                if (values[i] != "")
                {
                    returnValues.Add(values[i]);
                }
            }

            return returnValues;
        }
    }
}