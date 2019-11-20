using Repository;
using System.Collections.Generic;
using TechTalk.SpecFlow;

namespace AcceptanceTests.StepDefinitions
{
    [Binding]
    public class TransactionPercentageFeeDiscountSteps : Steps
    {
        private readonly List<MerchantInformation> _merchantInformation = new List<MerchantInformation>
        {
            new MerchantInformation
            {
                MerchantName = "TELIA",
                Status = "BIG",
                TransactionPercentageFee = 1,
                TransactionPercentageDiscountFee = 10,
                InvoiceFixedFee = 29
            },
            new MerchantInformation
            {
                MerchantName = "CIRCLE_K",
                Status = "BIG",
                TransactionPercentageFee = 1,
                TransactionPercentageDiscountFee = 20,
                InvoiceFixedFee = 29
            }
        };

        private readonly MerchantInformation _defaultMerchantInformation = new MerchantInformation
        {
            MerchantName = "DEFAULT",
            Status = "DEFAULT",
            TransactionPercentageFee = 1,
            TransactionPercentageDiscountFee = 0,
            InvoiceFixedFee = 29
        };

        [Given(@"Merchant repository is populated with Transaction Percentage Fee Discount business logic")]
        public void MerchantRepositoryIsPopulateWithTransactionPercentageDiscountFeeBusinessLogic()

        {
            ScenarioContext.Add("DefaultMerchantInformation", _defaultMerchantInformation);
            ScenarioContext.Add("MerchantInformation", _merchantInformation);
        }
    }
}