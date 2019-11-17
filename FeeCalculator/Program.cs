using DtoMapping;
using Homework_Tomas_Kireilis.Interfaces;
using Models;
using System;
using System.Globalization;
using System.Threading.Tasks;

namespace Homework_Tomas_Kireilis
{
    internal class Program
    {
        private static IReadingFromFile _reader;
        private static IFeeCalculator _feeCalculator;

        public static async Task Main(string[] args)
        {
            await CalculateFee("transactions.txt", "specialMerchants.txt");
        }

        public static async Task CalculateFee(string transactionPath, string specialMerchantPath)
        {
            _reader = new ReadingFromFile(transactionPath, specialMerchantPath);
            _feeCalculator = new FeeCalculator(new Mapper(),new MerchantFactory());
            await foreach (var transaction in  _reader.ReadTransactions())
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