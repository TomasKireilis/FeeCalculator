using Domain.Fees;
using Repository;
using System;
using Xunit;

namespace Domain.UnitTests
{
    public class InvoiceFixedFee_Should
    {
        [Theory]
        [InlineData(10)]
        [InlineData(1)]
        [InlineData(0.01)]
        [InlineData(0.5)]
        [InlineData(100000000000000000)]
        public void Calculate_TransactionFixedFee_Correctly(decimal feeAmount)
        {
            //setup
            var fees = new InvoiceFixedFee();
            var merchantInformation = new MerchantInformation { InvoiceFixedFee = feeAmount };
            var transaction = new Transaction { Date = DateTimeOffset.Now, TransactionPercentageFeeAmount = 10 };

            //act
            var response = fees.Calculate(transaction, merchantInformation);

            //Assert
            Assert.Equal(feeAmount, response.InvoiceFixedFeeAmount);
        }

        [Theory]
        [InlineData("2019-11-11", "2019-10-11", "CircleK", 10)]
        [InlineData("2019-11-11", "2019-01-11", "Telia", 0)]
        [InlineData("2019-11-11", "2018-10-11", "Maxima", 20)]
        [InlineData("2019-02-11", "2009-01-11", "CircleK", 80)]
        [InlineData("2019-11-11", "2018-11-11", "CircleK", 999999999999)]
        public void NotCalculate_TransactionFixedFee_WhenWrongDate(string dateString, string lastDateString,
            string merchantName, decimal amount)
        {
            //setup
            var fees = new InvoiceFixedFee { LastInvoiceFixedFeeDate = DateTimeOffset.Parse(lastDateString) };
            var merchantInformation = new MerchantInformation { InvoiceFixedFee = 10 };
            var transaction = CreateTransaction(dateString, merchantName, amount);

            //act
            var response = fees.Calculate(transaction, merchantInformation);

            //Assert
            Assert.Equal(0, response.InvoiceFixedFeeAmount);
        }

        [Fact]
        public void NotCalculate_TransactionFixedFee_WhenTransactionPercentageFeeIsZero()
        {
            //setup
            var fees = new InvoiceFixedFee();
            var merchantInformation = new MerchantInformation { InvoiceFixedFee = 10 };
            var transaction = new Transaction { TransactionPercentageFeeAmount = 0, Date = DateTimeOffset.Now };

            //act
            var response = fees.Calculate(transaction, merchantInformation);

            //Assert
            Assert.Equal(0, response.InvoiceFixedFeeAmount);
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