using System;
using System.Collections.Generic;
using System.Text;
using Domain.Fees;
using Repository;
using Xunit;

namespace Domain.UnitTests
{
    public class TransactionPercentageFee_Should
    {
        [Theory]
        [InlineData(10, 10, 1)]
        [InlineData(10, 5, 0.5)]
        [InlineData(0, 1, 0)]
        [InlineData(100, 0.5, 0.5)]
        [InlineData(10, 0.5, 0.05)]
        public void Calculate_TransactionPercentageFee_Correctly(decimal amount, decimal feeAmount, decimal expectedResult)
        {
            //setup
            var fees = new TransactionPercentageFee();
            var merchantInformation = new MerchantInformation {TransactionPercentageFee = feeAmount};
            var transaction = new Transaction {Amount = amount};



            //act
            var response = fees.Calculate(transaction, merchantInformation);

            //Assert
            Assert.Equal( expectedResult,transaction.TransactionPercentageFeeAmount);
        }
    }
}
