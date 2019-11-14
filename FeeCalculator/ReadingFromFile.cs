using Homework_Tomas_Kireilis.Interfaces;
using Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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

        public IEnumerable<Transaction> ReadTransactions()
        {
            string line;
            StreamReader file = new StreamReader(_transactionPath);
            while ((line = file.ReadLine()) != null)
            {
                var lineObjects = line.Split(' ').ToList();
                lineObjects = ClearInput(lineObjects);
                yield return (new Transaction
                {
                    Date = DateTimeOffset.Parse(lineObjects[0]),
                    MerchantName = lineObjects[1],
                    Amount = decimal.Parse(lineObjects[2])
                });
            }

            file.Close();
        }

        public List<Merchant> ReadSpecialMerchants()
        {
            string line;
            List<Merchant> merchants = new List<Merchant>();
            StreamReader file = new StreamReader(_specialMerchantsPath);
            while ((line = file.ReadLine()) != null)
            {
                var lineObjects = line.Split(' ').ToList();
                lineObjects = ClearInput(lineObjects);
                merchants.Add(new Merchant
                {
                    Name = lineObjects[0],
                    FeeDiscount = decimal.Parse(lineObjects[1])
                });
            }

            file.Close();
            return merchants;
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