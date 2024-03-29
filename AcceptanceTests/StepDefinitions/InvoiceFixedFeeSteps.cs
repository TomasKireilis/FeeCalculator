﻿using AcceptanceTests.Classes;
using Repository;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using Xunit;

namespace AcceptanceTests.StepDefinitions
{
    [Binding]
    public class InvoiceFixedFeeSteps : Steps
    {
        private readonly CultureInfo _culture = new CultureInfo("en-US");

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

        [Given(@"Merchant repository is populated with Invoice Fixed Fee business logic")]
        public void MerchantRepositoryIsPopulateWithInvoiceFixedFeeBusinessLogic()

        {
            ScenarioContext.Add("DefaultMerchantInformation", _defaultMerchantInformation);
            ScenarioContext.Add("MerchantInformation", _merchantInformation);
        }

        [Then(@"the output for Invoice Fixed Fee is")]
        public void TheOutputIs(Table table)
        {
            var feeTable = table.CreateSet<Fee>();
            var fees = feeTable.Select(x => decimal.Parse(x.FeeAmount, _culture)).ToList();
            var counter = 0;
            foreach (var fee in (List<Transaction>)ScenarioContext["CalculatedFees"])
            {
                Assert.Equal(fees[counter], fee.TransactionPercentageFeeAmount + fee.InvoiceFixedFeeAmount);
                counter++;
            }
        }
    }
}