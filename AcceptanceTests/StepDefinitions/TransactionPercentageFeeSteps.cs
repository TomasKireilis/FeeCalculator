using AcceptanceTests.Classes;
using Domain.Factories;
using Moq;
using Repository;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using Xunit;

namespace AcceptanceTests.StepDefinitions
{
    [Binding]
    public partial class TransactionPercentageFeeSteps : Steps
    {
        private readonly CultureInfo _culture = new CultureInfo("en-US");

        private readonly List<MerchantInformation> _merchantInformation = new List<MerchantInformation>();

        private readonly MerchantInformation _defaultMerchantInformation = new MerchantInformation()
        {
            MerchantName = "DEFAULT",
            Status = "DEFAULT",
            TransactionPercentageFee = 1,
            TransactionPercentageDiscountFee = 0,
            InvoiceFixedFee = 29
        };

        [Given(@"Merchant repository is populated with Transaction Percentage Fee business logic")]
        public void MerchantRepositoryIsPopulateWithTransactionPercentageFeeBusinessLogic()

        {
            ScenarioContext.Add("DefaultMerchantInformation", _defaultMerchantInformation);
            ScenarioContext.Add("MerchantInformation", _merchantInformation);
        }

        [Given(@"that transactions where made")]
        public void TransactionIsMadeTo(Table table)

        {
            var transactionTable = table.CreateSet<Transaction>();
            var transactions = new List<Transaction>();
            foreach (var transaction in transactionTable)
            {
                transactions.Add(transaction);
            }
            ScenarioContext.Add("Transactions", transactions);
        }

        [When(@"fees calculation app is executed")]
        public async Task FeesCalculationAppIsExecuted()
        {
            var readerMock = new Mock<IReadingFromFile>();
            readerMock.Setup(x => x.GetMerchantDefaultValues(It.IsAny<string>())).Returns<string>(ReadDefaultMerchantsFromRepositoryAsync);
            readerMock.Setup(x => x.ReadMerchantsFromRepositoryAsync())
                .Returns(ReadMerchantsFromRepositoryAsync);

            var feeCalculator = new FeeCalculator.FeeCalculator(new MerchantFactory(new FeeFactory()), readerMock.Object);

            var calculatedTransactions = new List<Transaction>();
            await foreach (var transaction in ReadTranslationsFromRepositoryAsync())
            {
                var returnedTransaction = await feeCalculator.Calculate(transaction);
                calculatedTransactions.Add(returnedTransaction);
            }
            ScenarioContext.Add("CalculatedFees", calculatedTransactions);
        }

        [Then(@"the output for Transaction Percentage Fee is")]
        public void TheOutputIs(Table table)
        {
            var feeTable = table.CreateSet<Fee>();
            var fees = feeTable.Select(x => decimal.Parse(x.FeeAmount, _culture)).ToList();
            var counter = 0;
            foreach (var fee in (List<Transaction>)ScenarioContext["CalculatedFees"])
            {
                Assert.Equal(fees[counter], fee.TransactionPercentageFeeAmount);
                counter++;
            }
        }

        public async IAsyncEnumerable<Transaction> ReadTranslationsFromRepositoryAsync()
        {
            foreach (var merchant in (List<Transaction>)ScenarioContext["Transactions"])
            {
                yield return merchant;
            }
        }

        public MerchantInformation ReadDefaultMerchantsFromRepositoryAsync(string merchantName)
        {
            var merchantInformation = (MerchantInformation)ScenarioContext["DefaultMerchantInformation"];
            merchantInformation.MerchantName = merchantName;
            return merchantInformation.Clone();
        }

        public async IAsyncEnumerable<MerchantInformation> ReadMerchantsFromRepositoryAsync()
        {
            foreach (var merchant in (List<MerchantInformation>)ScenarioContext["MerchantInformation"])
            {
                yield return merchant;
            }
        }
    }
}