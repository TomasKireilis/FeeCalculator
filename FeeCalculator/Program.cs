using Homework_Tomas_Kireilis.Interfaces;
using Models;
using System;
using System.Globalization;

namespace Homework_Tomas_Kireilis
{
    internal class Program
    {
        private static IReadingFromFile _reader;
        private static IFeeCalculator _feeCalculator;

        public static void Main(string[] args)
        {
            CalculateFee("transactions.txt", "specialMerchants.txt");
        }

        public static void CalculateFee(string transactionPath, string specialMerchantPath)
        {
            _reader = new ReadingFromFile(transactionPath, specialMerchantPath);
            _feeCalculator = new FeeCalculator(new Fees(), 1, _reader.ReadSpecialMerchants(), 29);
            foreach (var transaction in _reader.ReadTransactions())
            {
                var calculatedTransaction = _feeCalculator.Calculate(transaction);
                WriteToConsole(calculatedTransaction);
            }
        }

        public static void WriteToConsole(Transaction calculatedTransaction)
        {
            Console.WriteLine(
                $"{calculatedTransaction.Date.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)}" +
                $" {calculatedTransaction.MerchantName}" +
                $" {calculatedTransaction.BasicFeeAmount + calculatedTransaction.MonthlyFeeAmount}");
        }
    }
}