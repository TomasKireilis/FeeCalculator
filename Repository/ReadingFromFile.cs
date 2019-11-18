﻿using System;
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
                    Amount = decimal.Parse(lineObjects[2]),
                };
                yield return transaction;
            }

            file.Close();
        }

        public async IAsyncEnumerable<MerchantInformation> ReadMerchantsFromRepositoryAsync()
        {
            string line;
            StreamReader file = new StreamReader(MerchantPath);
            CultureInfo cultures = new CultureInfo("en-US");
            while ((line = await file.ReadLineAsync()) != null)
            {
                var lineObjects = line.Split(' ').ToList();
                lineObjects = ClearInput(lineObjects);
                var merchantInformation = new MerchantInformation()
                {
                    MerchantName = lineObjects[0],
                    Status = lineObjects[1] == "null" ? _defaultValues.Status : lineObjects[1],
                    BasicFee = lineObjects[2] == "null" ? _defaultValues.BasicFee : decimal.Parse(lineObjects[2], cultures),
                    MonthlyFee = lineObjects[3] == "null" ? _defaultValues.MonthlyFee : decimal.Parse(lineObjects[3], cultures),
                    BasicFeeDiscount = lineObjects[4] == "null" ? _defaultValues.BasicFeeDiscount : decimal.Parse(lineObjects[4], cultures)
                };
                yield return merchantInformation;
            }

            file.Close();
        }

        public MerchantInformation GetMerchantDefaultValues(string merchantName)
        {
            var merchantInformation = _defaultValues;
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
                    BasicFee = decimal.Parse(lineObjects[2]),
                    MonthlyFee = decimal.Parse(lineObjects[3]),
                    BasicFeeDiscount = decimal.Parse(lineObjects[4])
                };
                break;
            }

            file.Close();
        }

        private List<string> ClearInput(List<string> values)
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