using FeeCalculator.Interfaces;
using Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FeeCalculator
{
    public class ReadingFromFile : IReadingFromFile
    {
        private readonly string _transactionPath;

        public ReadingFromFile(string transactionPath)
        {
            _transactionPath = transactionPath;
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