using System;
using System.Globalization;
using System.IO;
using AcceptanceTests.Constants;
using Models;
using TechTalk.SpecFlow;

namespace AcceptanceTests.StepDefinitions
{
    [Binding]
    public class TransactionPercentageFeeSteps : Steps
    {
        [Given(@"that (.*) DKK transaction is made to (.*) on (.*)")]
        public void TransactionIsMadeTo(string amount, string merchantName, string date)
        {
            var transaction = new Transaction
                {Amount = decimal.Parse(amount), Date = DateTimeOffset.Parse(date), MerchantName = merchantName};
            TestContext.Transaction.Add(transaction);
        }

        [When(@"fees calculation app is executed")]
        public void FeesCalculationAppIsExecuted()
        {
            PrepareTextFiles();
        }
        [Then(@"the output is")]
        public void FeesCalculationAppIsExecuted(Table table)
        {
           
        }

        private void PrepareTextFiles()
        {
            using var translationsFile =
                new StreamWriter(@"transactions.txt", false);
            foreach (var transaction in TestContext.Transaction)
            {
                translationsFile.WriteLine($"{transaction.Date.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)}" +
                               $" {transaction.MerchantName}" +
                               $" {transaction.BasicFeeAmount + transaction.MonthlyFeeAmount}");
            }
            translationsFile.Close();
            using var specialMerchantsFile =
                new StreamWriter(@"transactions.txt", false);
            foreach (var merchant in TestContext.SpecialMerchants)
            {
                specialMerchantsFile.WriteLine($"{merchant.Name}" +
                                               $" {merchant.FeeDiscount}");
            }
            specialMerchantsFile.Close();
        }
    }
}
