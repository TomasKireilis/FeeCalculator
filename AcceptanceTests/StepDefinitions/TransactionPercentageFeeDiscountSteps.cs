using Repository;
using System.Collections.Generic;
using System.Globalization;
using TechTalk.SpecFlow;

namespace AcceptanceTests.StepDefinitions
{
    [Binding]
    public class TransactionPercentageFeeDiscountSteps : Steps
    {
        private readonly CultureInfo _culture = new CultureInfo("en-US");

        private readonly List<MerchantInformation> _merchantInformation = new List<MerchantInformation>()
        {
            new MerchantInformation()
            {
                MerchantName = "TELIA",
                Status = "BIG",
                BasicFee = 1,
                BasicFeeDiscount = 10,
                MonthlyFee = 29
            },
            new MerchantInformation()
            {
                MerchantName = "CIRCLE_K",
                Status = "BIG",
                BasicFee = 1,
                BasicFeeDiscount = 20,
                MonthlyFee = 29
            }
        };

        private readonly MerchantInformation _defaultMerchantInformation = new MerchantInformation()
        {
            MerchantName = "DEFAULT",
            Status = "DEFAULT",
            BasicFee = 1,
            BasicFeeDiscount = 0,
            MonthlyFee = 29
        };

        [Given(@"Merchant repository is populated with Transaction Percentage Fee Discount business logic")]
        public void MerchantRepositoryIsPopulateWithTransactionPercentageFeeBusinessLogic()

        {
            ScenarioContext.Add("DefaultMerchantInformation", _defaultMerchantInformation);
            ScenarioContext.Add("MerchantInformation", _merchantInformation);
        }
    }
}