using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace Repository.IntegrationTests
{
    public class ReadingFromFile_Should
    {
        [Fact]
        public async Task ReadMerchantsFromRepositoryAsync()
        {
            //Setup
            var merchants = new List<string>()
            {
                {"TELIA DEFAULT 12 12.12 0"},
            };
            StreamWriter file = new StreamWriter("MerchantsTest.txt");
            foreach (var merchant in merchants)
            {
                file.WriteLine(merchant);
            }
            file.Close();

            var reader = new ReadingFromFile("", "DefaultMerchantTest.txt", "MerchantsTest.txt");
            //Act & Assert

            var expectedResponce = new List<MerchantInformation>()
            {
                new MerchantInformation()
                {
                    MerchantName = "TELIA",
                    Status = "DEFAULT",
                    InvoiceFixedFee = (decimal) 12.12,
                    TransactionPercentageDiscountFee = 0,
                    TransactionPercentageFee = 12
                }
            };
            var counter = 0;
            await foreach (var merchant in reader.ReadMerchantsFromRepositoryAsync())
            {
                Assert.True(BothMerchantInformationEqual(expectedResponce[counter], merchant));
                counter++;
            }
        }

        private bool BothMerchantInformationEqual(MerchantInformation merchantInformation1, MerchantInformation merchantInformation2)
        {
            return merchantInformation1.MerchantName == merchantInformation2.MerchantName &&
                   merchantInformation1.Status == merchantInformation2.Status &&
                   merchantInformation1.InvoiceFixedFee == merchantInformation2.InvoiceFixedFee &&
                   merchantInformation1.TransactionPercentageDiscountFee ==
                   merchantInformation2.TransactionPercentageDiscountFee &&
                   merchantInformation1.TransactionPercentageFee == merchantInformation2.TransactionPercentageFee;
        }

        [Fact]
        public async Task ReadTransactionsFromRepositoryAsync()
        {
            //Setup
            var transactions = new List<string>()
            {
                {"2019-12-12 CIRCLE_K 100"},
            };
            StreamWriter file = new StreamWriter("TranslationsTest.txt");
            foreach (var transaction in transactions)
            {
                file.WriteLine(transaction);
            }
            file.Close();

            var reader = new ReadingFromFile("TranslationsTest.txt", "DefaultMerchantTest.txt", "MerchantsTest.txt");
            //Act & Assert

            var expectedResponce = new List<Transaction>()
            {
                new Transaction()
                {
                    Date = DateTimeOffset.Parse("2019-12-12"),
                    MerchantName = "CIRCLE_K",
                    Amount = 100
                }
            };
            var counter = 0;
            await foreach (var merchant in reader.ReadTransactionsFromRepositoryAsync())
            {
                Assert.True(BothTransactionsEqual(expectedResponce[counter], merchant));
                counter++;
            }
        }

        private bool BothTransactionsEqual(Transaction transaction1, Transaction transaction2)
        {
            return transaction1.Date == transaction2.Date &&
                   transaction1.MerchantName == transaction2.MerchantName &&
                   transaction1.Amount == transaction2.Amount;
        }
    }
}