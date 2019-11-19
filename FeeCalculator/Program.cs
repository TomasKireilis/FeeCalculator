using Domain.Factories;
using Repository;
using System;
using System.Globalization;
using System.Threading.Tasks;

namespace FeeCalculator
{
    internal class Program
    {
        private static readonly CultureInfo Culture = new CultureInfo("en-US");

        public static async Task Main(string[] args)
        {
            var reader = new ReadingFromFile();

            var feeCalculator = new FeeCalculator(
                new MerchantFactory(new FeeFactory()),
                reader);

            await CalculateFee(reader, feeCalculator);
        }

        public static async Task CalculateFee(IReadingFromFile readingFromFile, IFeeCalculator feeCalculator)
        {
            await foreach (var transaction in readingFromFile.ReadTranslationsFromRepositoryAsync())
            {
                var calculatedTransaction = await feeCalculator.Calculate(transaction);
                WriteToConsole(calculatedTransaction);
            }
        }

        public static void WriteToConsole(Transaction calculatedTransaction)
        {
            Console.WriteLine(
                $"{calculatedTransaction.Date.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)}" +
                $" {calculatedTransaction.MerchantName}" +
                $" {(calculatedTransaction.BasicFeeAmount + calculatedTransaction.MonthlyFeeAmount).ToString("0.00", Culture)}");
        }
    }
}