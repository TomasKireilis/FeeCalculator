using Domain.Factories;
using Repository;
using System;
using System.Globalization;
using System.Threading.Tasks;

namespace FeeCalculatorService
{
    public class Program
    {
        private static readonly CultureInfo Culture = new CultureInfo("en-US");
        private const string TransactionPath = "transactions.txt";
        private const string MerchantPath = "Merchants.txt";
        private const string DefaultMerchantPath = "DefaultMerchant.txt";

        public static async Task Main(string[] args)
        {
            var reader = new ReadingFromFile(TransactionPath, DefaultMerchantPath, MerchantPath);

            var feeCalculator = new FeeCalculatorService.FeeCalculator(
                new MerchantFactory(new FeeFactory()),
                reader);

            await CalculateFee(reader, feeCalculator);
        }

        public static async Task CalculateFee(IReadingFromFile readingFromFile, IFeeCalculator feeCalculator)
        {
            await foreach (var transaction in readingFromFile.ReadTransactionsFromRepositoryAsync())
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
                $" {(calculatedTransaction.TransactionPercentageFeeAmount + calculatedTransaction.InvoiceFixedFeeAmount).ToString("0.00", Culture)}");
        }
    }
}