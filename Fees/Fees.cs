using Fees.Models;
using System;

namespace Fees
{
    public class Fees : IFees
    {
        public DateTimeOffset LastMonthlyFee;

        public TransactionFee CalculateFee(TransactionFee transactionFee)
        {
            var convertedTransactionFee = new TransactionFee
            {
                Amount = transactionFee.Amount,
                Date = transactionFee.Date,
                MerchantName = transactionFee.MerchantName,
                BasicFee = transactionFee.BasicFee,
                MonthlyFee = transactionFee.MonthlyFee,
                BasicFeeAmount = TransactionPercentageFee(transactionFee.Amount, transactionFee.BasicFee),
            };

            if (CheckIfDateIsNewerForMonthlyFee(transactionFee.Date))
            {
                convertedTransactionFee.MonthlyFeeAmount = TransactionFixedFee(transactionFee.MonthlyFee);
                LastMonthlyFee = transactionFee.Date;
            }

            return convertedTransactionFee;
        }

        private bool CheckIfDateIsNewerForMonthlyFee(DateTimeOffset transactionDate)
        {
            if (LastMonthlyFee.Year == transactionDate.Year && LastMonthlyFee.Month < transactionDate.Month)
            {
                return true;
            }

            if (LastMonthlyFee.Year < transactionDate.Year)
            {
                return true;
            }

            return false;
        }

        private decimal TransactionPercentageFee(decimal amount, decimal feeAmount)
        {
            if (feeAmount > 0)
            {
                return amount * (feeAmount / 100);
            }
            throw new ArgumentException($"Wrong Fee value: {feeAmount}");
        }

        private decimal TransactionFixedFee(decimal feeAmount)
        {
            if (feeAmount > 0)
            {
                return feeAmount;
            }
            throw new ArgumentException($"Wrong Fee value: {feeAmount}");
        }
    }
}