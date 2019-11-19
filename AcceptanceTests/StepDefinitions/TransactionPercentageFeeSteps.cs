using AcceptanceTests.Constants;
using Domain.Factories;
using Moq;
using Repository;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using Xunit;

namespace AcceptanceTests.StepDefinitions
{
    [Binding]
    public class TransactionPercentageFeeSteps : Steps
    {
        private readonly CultureInfo _culture = new CultureInfo("en-US");

        private readonly List<MerchantInformation> _merchantInformation = new List<MerchantInformation>();

        private readonly MerchantInformation _defaultMerchantInformation = new MerchantInformation()
        {
            MerchantName = "default",
            Status = "default",
            BasicFee = 1,
            BasicFeeDiscount = 0,
            MonthlyFee = 29
        };

        public TransactionPercentageFeeSteps()
        {
            TestContext.Merchants = _merchantInformation;
            TestContext.TransactionFees = new List<decimal>();
            TestContext.Transaction = new List<Transaction>();
            TestContext.DefaultMerchant = _defaultMerchantInformation;
        }

        [Given(@"that transactions where made")]
        public void TransactionIsMadeTo(Table table)

        {
            var transactionTable = table.CreateSet<Transaction>();
            foreach (var transaction in transactionTable)
            {
                TestContext.Transaction.Add(transaction);
            }
        }

        [When(@"fees calculation app is executed")]
        public async Task FeesCalculationAppIsExecuted()
        {
            var readerMock = new Mock<IReadingFromFile>();
            readerMock.Setup(x => x.GetMerchantDefaultValues(It.IsAny<string>())).Returns(ReadDefaultMerchantsFromRepositoryAsync);
            readerMock.Setup(x => x.ReadMerchantsFromRepositoryAsync())
                .Returns(ReadMerchantsFromRepositoryAsync);
            readerMock.Setup(x => x.ReadTranslationsFromRepositoryAsync())
                .Returns(ReadTranslationsFromRepositoryAsync());
            var feeCalculator = new FeeCalculator.FeeCalculator(new MerchantFactory(new FeeFactory()), readerMock.Object);
            foreach (var transaction in TestContext.Transaction)
            {
                var returnedTransaction = await feeCalculator.Calculate(transaction);
                TestContext.TransactionFees.Add(returnedTransaction.BasicFeeAmount);
            }
        }

        [Then(@"the output is")]
        public void FeesCalculationAppIsExecuted(Table table)
        {
            var feeTable = table.CreateSet<Fee>();
            var fees = feeTable.Select(x=>decimal.Parse(x.FeeAmount,_culture)).ToList();
            var counter = 0;
            foreach (var fee in TestContext.TransactionFees)
            {
                Assert.Equal(fee, fees[counter]);
                counter++;
            }
        }

        public async IAsyncEnumerable<Transaction> ReadTranslationsFromRepositoryAsync()
        {
            foreach (var merchant in TestContext.Transaction)
            {
                yield return merchant;
            }
        }

        public MerchantInformation ReadDefaultMerchantsFromRepositoryAsync()
        {
            return TestContext.DefaultMerchant.Clone();
        }

        public async IAsyncEnumerable<MerchantInformation> ReadMerchantsFromRepositoryAsync()
        {
            foreach (var merchant in TestContext.Merchants)
            {
                yield return merchant;
            }
        }
        private class Fee
        {
            public string FeeAmount { get; set; }
        }
    }
}