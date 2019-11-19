using Domain.Factories;
using Repository;
using System;
using System.Globalization;
using System.Threading.Tasks;

namespace FeeCalculator
{
    internal class Program
    {
        private static IReadingFromFile _reader;
        private static IFeeCalculator _feeCalculator;
        private static readonly CultureInfo Culture = new CultureInfo("en-US");

        public static async Task Main(string[] args)
        {
            await CalculateFee();
        }

        public static async Task CalculateFee()
        {
            _reader = new ReadingFromFile();
            _feeCalculator = new FeeCalculator(
                new MerchantFactory(
                    _reader,
                    new FeeFactory()));
            await foreach (var transaction in _reader.ReadTranslationsFromRepositoryAsync())
            {
                var calculatedTransaction = await _feeCalculator.Calculate(transaction);
                WriteToConsole(calculatedTransaction);
            }
        }

        public static void WriteToConsole(Transaction calculatedTransaction)
        {
            Console.WriteLine(
                $"{calculatedTransaction.Date.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)}" +
                $" {calculatedTransaction.MerchantName}" +
                $" {(calculatedTransaction.BasicFeeAmount + calculatedTransaction.MonthlyFeeAmount).ToString("0.00",Culture)}");
        }
    }
}