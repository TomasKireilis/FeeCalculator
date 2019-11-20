using Domain.Fees;
using Repository;
using Xunit;

namespace Domain.UnitTests
{
    public class TransactionPercentageDiscountFee_Should
    {
        [Theory]
        [InlineData(1, 10, 0.9)]
        [InlineData(10, 50, 5)]
        [InlineData(0, 1, 0)]
        [InlineData(12.5, 20, 10)]
        public void Calculate_TransactionPercentageFeeDiscount_Correctly(decimal amount, decimal feeAmount, decimal expectedResult)
        {
            //setup
            var fees = new TransactionPercentageDiscountFee();
            var merchantInformation = new MerchantInformation { TransactionPercentageDiscountFee = feeAmount };
            var transaction = new Transaction { TransactionPercentageFeeAmount = amount };

            //act
            var response = fees.Calculate(transaction, merchantInformation);

            //Assert
            Assert.Equal(expectedResult, response.TransactionPercentageFeeAmount);
        }
    }
}