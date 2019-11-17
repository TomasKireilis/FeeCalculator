using System;
using Xunit;

namespace FeeCalculator_Should
{
    public class Fees_Should
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
            var fees = new Fees();

            //act
            var response = fees.TransactionPercentageFee(amount, feeAmount);

            //Assert
            Assert.Equal(expectedResult, response);
        }

        [Theory]
        [InlineData(1, 10, 0.9)]
        [InlineData(10, 50, 5)]
        [InlineData(0, 1, 0)]
        [InlineData(12.5, 20, 10)]
        public void Calculate_TransactionPercentageFeeDiscount_Correctly(decimal amount, decimal feeAmount, decimal expectedResult)
        {
            //setup
            var fees = new Fees();

            //act
            var response = fees.TransactionPercentageFeeDiscount(amount, feeAmount);

            //Assert
            Assert.Equal(expectedResult, response);
        }

        [Theory]
        [InlineData(10)]
        [InlineData(1)]
        [InlineData(0.000000000000001)]
        [InlineData(0.5)]
        [InlineData(100000000000000000)]
        public void Calculate_TransactionFixedFee_Correctly(decimal feeAmount)
        {
            //setup
            var fees = new Fees();

            //act
            var response = fees.TransactionFixedFee(feeAmount);

            //Assert
            Assert.Equal(feeAmount, response);
        }

        [Theory]
        [InlineData(-0.101022)]
        [InlineData(0)]
        [InlineData(-45461456)]
        public void ThrowError_When_TransactionFixedFee_FeeAmountIsWrong(decimal feeAmount)
        {
            //setup
            var fees = new Fees.Fees();

            //act & Assert
            Assert.Throws<ArgumentException>(() => fees.TransactionFixedFee(feeAmount));
        }

        [Theory]
        [InlineData(-0.101022)]
        [InlineData(0)]
        [InlineData(-45461456)]
        public void ThrowError_When_TransactionPercentageFeeDiscount_FeeAmountIsWrong(decimal feeAmount)
        {
            //setup
            var fees = new Fees.Fees();

            //act & Assert
            Assert.Throws<ArgumentException>(() => fees.TransactionPercentageFeeDiscount(10, feeAmount));
        }

        [Theory]
        [InlineData(-0.101022)]
        [InlineData(0)]
        [InlineData(-45461456)]
        public void ThrowError_When_TransactionPercentageFee_FeeAmountIsWrong(decimal feeAmount)
        {
            //setup
            var fees = new Fees();

            //act & Assert
            Assert.Throws<ArgumentException>(() => fees.TransactionPercentageFee(10, feeAmount));
        }
    }
}