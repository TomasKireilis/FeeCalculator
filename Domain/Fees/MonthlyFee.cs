using Domain.Interfaces;
using Repository;
using System;

namespace Domain.Fees
{
    internal class MonthlyFee : IFee
    {
        public DateTimeOffset LastMonthlyFee;

        public Transaction Calculate(Transaction transaction, MerchantInformation merchantInformation)
        {
            if (!NeedToCalculate(transaction))
            {
                return transaction;
            }
            transaction.MonthlyFeeAmount += merchantInformation.MonthlyFee;
            LastMonthlyFee = transaction.Date;
            return transaction;
        }

        private bool NeedToCalculate(Transaction transaction)
        {
            if (CheckIfDateIsNewerForMonthlyFee(transaction.Date) && transaction.BasicFeeAmount > 0)
            {
                return true;
            }

            return false;
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
    }
}