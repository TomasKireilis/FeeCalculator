using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Repository
{
    public class ReadingFromFile : IReadingFromFile
    {
        private MerchantInformation _defaultValues;
        private const string TransactionPath = "transactions.txt";
        private const string MerchantPath = "Merchants.txt";
        private const string DefaultMerchantPath = "DefaultMerchant.txt";
        private readonly CultureInfo _culture = new CultureInfo("en-US");

        public ReadingFromFile()
        {
            FetchDefaultMerchantInformation(DefaultMerchantPath);
        }

        public async IAsyncEnumerable<Transaction> ReadTranslationsFromRepositoryAsync()
        {
            string line;
            StreamReader file = new StreamReader(TransactionPath);
            while ((line = await file.ReadLineAsync()) != null)
            {
                var lineObjects = line.Split(' ').ToList();
                lineObjects = ClearInput(lineObjects);
                var transaction = new Transaction()
                {
                    Date = DateTimeOffset.Parse(lineObjects[0]),
                    MerchantName = lineObjects[1],
                    Amount = decimal.Parse(lineObjects[2], _culture),
                };
                yield return transaction;
            }

            file.Close();
        }

        public async IAsyncEnumerable<MerchantInformation> ReadMerchantsFromRepositoryAsync()
        {
            string line;
            StreamReader file = new StreamReader(MerchantPath);

            while ((line = await file.ReadLineAsync()) != null)
            {
                var lineObjects = line.Split(' ').ToList();
                lineObjects = ClearInput(lineObjects);
                var merchantInformation = new MerchantInformation()
                {
                    MerchantName = lineObjects[0],
                    Status = lineObjects[1] == "NULL" ? _defaultValues.Status : lineObjects[1],
                    TransactionPercentageFee = lineObjects[2] == "NULL" ? _defaultValues.TransactionPercentageFee : decimal.Parse(lineObjects[2], _culture),
                    InvoiceFixedFee = lineObjects[3] == "NULL" ? _defaultValues.InvoiceFixedFee : decimal.Parse(lineObjects[3], _culture),
                    TransactionPercentageDiscountFee = lineObjects[4] == "NULL" ? _defaultValues.TransactionPercentageDiscountFee : decimal.Parse(lineObjects[4], _culture)
                };
                yield return merchantInformation;
            }

            file.Close();
        }

        public MerchantInformation GetMerchantDefaultValues(string merchantName)
        {
            var merchantInformation = _defaultValues.Clone();
            merchantInformation.MerchantName = merchantName;
            return merchantInformation;
        }

        private async void FetchDefaultMerchantInformation(string defaultMerchantValuesPath)
        {
            string line;
            StreamReader file = new StreamReader(defaultMerchantValuesPath);
            while ((line = await file.ReadLineAsync()) != null)
            {
                var lineObjects = line.Split(' ').ToList();
                lineObjects = ClearInput(lineObjects);
                _defaultValues = new MerchantInformation()
                {
                    MerchantName = lineObjects[0],
                    Status = lineObjects[1],
                    TransactionPercentageFee = decimal.Parse(lineObjects[2], _culture),
                    InvoiceFixedFee = decimal.Parse(lineObjects[3], _culture),
                    TransactionPercentageDiscountFee = decimal.Parse(lineObjects[4], _culture)
                };
                break;
            }

            file.Close();
        }

        private List<string> ClearInput(IEnumerable<string> values)
        {
            var returnValues = new List<string>();
            foreach (var value in values)
            {
                if (value != "")
                {
                    returnValues.Add(value);
                }
            }

            return returnValues;
        }
    }
}