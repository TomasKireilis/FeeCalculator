using Homework_Tomas_Kireilis;
using Homework_Tomas_Kireilis.Interfaces;
using Models;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace FeeCalculator_Should
{
    public class FeeCalculator_Should
    {
        [Theory]
        [InlineData("2019-11-11", "CircleK", 10)]
        [InlineData("2019-11-11", "Telia", 0)]
        [InlineData("2019-10-11", "Maxima", 20)]
        [InlineData("2019-01-11", "CircleK", 80)]
        [InlineData("2019-12-11", "CircleK", 999999999999)]
        public void Calculate_Basic_And_Monthly_Fee(string dateString, string merchantName, decimal amount)
        {
            //setup
            var transaction = CreateTransaction(dateString, merchantName, amount);
            var fees = new Mock<IFees>();
            var feeCalculator = new FeeCalculator(fees.Object, 1, new List<Merchant>(), 20);
            fees.Setup(x => x.TransactionFixedFee(20)).Returns(20);
            fees.Setup(x => x.TransactionPercentageFee(transaction.Amount, 1)).Returns((decimal)15.00786454);

            //act
            var response = feeCalculator.Calculate(transaction);

            //Assert
            Assert.Equal((decimal)15.01, response.BasicFeeAmount);
            Assert.Equal(20, response.MonthlyFeeAmount);
            Assert.Equal(transaction.MerchantName, response.MerchantName);
            Assert.Equal(transaction.Date, response.Date);
            Assert.Equal(transaction.Amount, response.Amount);
        }

        [Theory]
        [InlineData("2019-11-11", "2019-10-11", "CircleK", 10)]
        [InlineData("2019-11-11", "2019-01-11", "Telia", 0)]
        [InlineData("2019-11-11", "2018-10-11", "Maxima", 20)]
        [InlineData("2019-02-11", "2009-01-11", "CircleK", 80)]
        [InlineData("2019-11-11", "2018-11-11", "CircleK", 999999999999)]
        public void Calculate_Basic_And_Monthly_Fee_WhenThereIsTimeDifferences(string dateString, string preTransactionDateString, string merchantName, decimal amount)
        {
            //setup
            var preTransaction = CreateTransaction(preTransactionDateString, merchantName, amount);
            var transaction = CreateTransaction(dateString, merchantName, amount);
            var fees = new Mock<IFees>();
            var feeCalculator = new FeeCalculator(fees.Object, 1, new List<Merchant>(), 20);
            fees.Setup(x => x.TransactionFixedFee(20)).Returns(20);
            fees.Setup(x => x.TransactionPercentageFee(transaction.Amount, 1)).Returns((decimal)15.00786454);

            //act
            feeCalculator.Calculate(preTransaction);
            var response = feeCalculator.Calculate(transaction);

            //Assert
            Assert.Equal((decimal)15.01, response.BasicFeeAmount);
            Assert.Equal(20, response.MonthlyFeeAmount);
            Assert.Equal(transaction.MerchantName, response.MerchantName);
            Assert.Equal(transaction.Date, response.Date);
            Assert.Equal(transaction.Amount, response.Amount);
        }

        [Theory]
        [InlineData("2019-10-11", "2019-11-11", "CircleK", 10)]
        [InlineData("2019-11-11", "2019-11-11", "Telia", 0)]
        [InlineData("2019-01-11", "2020-12-11", "Maxima", 20)]
        [InlineData("2019-11-11", "2019-11-09", "CircleK", 80)]
        [InlineData("2018-11-11", "2019-10-11", "CircleK", 999999999999)]
        public void Calculate_Only_Basic_Fee(string dateString, string preTransactionDateString, string merchantName, decimal amount)
        {
            //setup
            var preTransaction = CreateTransaction(preTransactionDateString, merchantName, amount);
            var transaction = CreateTransaction(dateString, merchantName, amount);
            var fees = new Mock<IFees>();
            var feeCalculator = new FeeCalculator(fees.Object, 1, new List<Merchant>(), 20);
            fees.Setup(x => x.TransactionFixedFee(20)).Returns(20);
            fees.Setup(x => x.TransactionPercentageFee(transaction.Amount, 1)).Returns((decimal)15.00786454);

            //act
            feeCalculator.Calculate(preTransaction);
            var response = feeCalculator.Calculate(transaction);

            //Assert
            Assert.Equal((decimal)15.01, response.BasicFeeAmount);
            Assert.Equal(0, response.MonthlyFeeAmount);
            Assert.Equal(transaction.MerchantName, response.MerchantName);
            Assert.Equal(transaction.Date, response.Date);
            Assert.Equal(transaction.Amount, response.Amount);
        }

        [Theory]
        [InlineData("2019-11-11", "2019-10-11", "CircleK", 10)]
        [InlineData("2019-11-11", "2019-11-13", "Telia", 0)]
        [InlineData("2019-10-11", "2018-09-11", "Maxima", 20)]
        [InlineData("2019-02-11", "2009-01-11", "CircleK", 80)]
        [InlineData("2019-12-11", "2018-12-11", "CircleK", 999999999999)]
        public void Calculate_Basic_Fee_With_Discount(string dateString, string preTransactionDateString, string merchantName, decimal amount)
        {
            //setup
            var preTransaction = CreateTransaction(preTransactionDateString, merchantName, amount);
            var transaction = CreateTransaction(dateString, merchantName, amount);
            var merchant = new Merchant
            {
                Name = merchantName,
                FeeDiscount = 10
            };

            var fees = new Mock<IFees>();
            var feeCalculator = new FeeCalculator(fees.Object, 1, new List<Merchant> { merchant }, 20);
            fees.Setup(x => x.TransactionFixedFee(20)).Returns(20);
            fees.Setup(x => x.TransactionPercentageFeeDiscount((decimal)15.00786454, 10)).Returns((decimal)45.215);
            fees.Setup(x => x.TransactionPercentageFee(transaction.Amount, 1)).Returns((decimal)15.00786454);

            //act
            feeCalculator.Calculate(preTransaction);
            var response = feeCalculator.Calculate(transaction);

            //Assert
            Assert.Equal((decimal)45.22, response.BasicFeeAmount);
            Assert.Equal(20, response.MonthlyFeeAmount);
            Assert.Equal(transaction.MerchantName, response.MerchantName);
            Assert.Equal(transaction.Date, response.Date);
            Assert.Equal(transaction.Amount, response.Amount);
        }

        private Transaction CreateTransaction(string dateString, string merchantName, decimal amount)
        {
            DateTimeOffset date = DateTimeOffset.Parse(dateString);
            return new Transaction()
            {
                Date = date,
                Amount = amount,
                MerchantName = merchantName
            };
        }
    }
}