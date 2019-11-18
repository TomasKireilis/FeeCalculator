using Domain.Interfaces;
using Repository;
using System;

namespace Domain.Fees
{
    internal class MonthlyFee : IFee
    {
        public DateTimeOffset LastMonthlyFee;

        public bool NeedToCalculate(Transaction transaction, MerchantInformation merchantInformation)
        {
            if (CheckIfDateIsNewerForMonthlyFee(transaction.Date))
            {
                return true;
            }

            return false;
        }

        public Transaction Calculate(Transaction transaction, MerchantInformation merchantInformation)
        {
            transaction.MonthlyFeeAmount = merchantInformation.MonthlyFee;
            LastMonthlyFee = transaction.Date;
            return transaction;
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