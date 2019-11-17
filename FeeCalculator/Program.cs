using DtoMapping;
using Factory;
using FeeCalculator.Interfaces;
using Models;
using System;
using System.Globalization;
using System.Threading.Tasks;

namespace FeeCalculator
{
    internal class Program
    {
        private static IReadingFromFile _reader;
        private static IFeeCalculator _feeCalculator;

        public static async Task Main(string[] args)
        {
            await CalculateFee("transactions.txt");
        }

        public static async Task CalculateFee(string transactionPath)
        {
            _reader = new ReadingFromFile(transactionPath);
            _feeCalculator = new FeeCalculator(new Mapper(), new MerchantFactory());
            await foreach (var transaction in _reader.ReadTransactions())
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