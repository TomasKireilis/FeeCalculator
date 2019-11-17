using System;
using Fees.Models;

namespace Fees
{
    public class Fees : IFees
    {
        public DateTimeOffset LastMontlyFee;

        public TransactionFee CalculateFee(TransactionFee transactionFee)
        {
            var TransactionFee = new TransactionFee
            {
                Amount = transactionFee.Amount,
                Date = transactionFee.Date,
                MerchantName = transactionFee.MerchantName,
                BasicFee = transactionFee.BasicFee,
                MonthlyFee = transactionFee.MonthlyFee,
                BasicFeeAmount = TransactionPercentageFee(transactionFee.Amount, transactionFee.BasicFee),
                MonthlyFeeAmount = TransactionFixedFee(transactionFee.MonthlyFee),
            };
            ChangeLastMonthlyTransactionToMerchant(TransactionFee);
            return TransactionFee;
        }
        private void ChangeLastMonthlyTransactionToMerchant(TransactionFee transaction)
        {
            if (CheckIfDateIsNewerForMonthlyFee(transaction.Date))
            {
                LastMontlyFee = transaction.Date;
            }
        }

        private bool CheckIfDateIsNewerForMonthlyFee(DateTimeOffset transactionDate)
        {
            if (LastMontlyFee.Year == transactionDate.Year && LastMontlyFee.Month < transactionDate.Month)
            {
                return true;
            }

            if (LastMontlyFee.Year < transactionDate.Year)
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