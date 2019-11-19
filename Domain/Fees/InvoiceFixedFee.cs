using Domain.Interfaces;
using Repository;
using System;

namespace Domain.Fees
{
    internal class InvoiceFixedFee : IFee
    {
        public DateTimeOffset InvoiceFixedFeeDate;

        public Transaction Calculate(Transaction transaction, MerchantInformation merchantInformation)
        {
            if (!NeedToCalculate(transaction))
            {
                return transaction;
            }
            transaction.InvoiceFixedFeeAmount += merchantInformation.InvoiceFixedFee;
            InvoiceFixedFeeDate = transaction.Date;
            return transaction;
        }

        private bool NeedToCalculate(Transaction transaction)
        {
            if (CheckIfDateIsNewerForMonthlyFee(transaction.Date) && transaction.TransactionPercentageFeeAmount > 0)
            {
                return true;
            }

            return false;
        }

        private bool CheckIfDateIsNewerForMonthlyFee(DateTimeOffset transactionDate)
        {
            if (InvoiceFixedFeeDate.Year == transactionDate.Year && InvoiceFixedFeeDate.Month < transactionDate.Month)
            {
                return true;
            }

            if (InvoiceFixedFeeDate.Year < transactionDate.Year)
            {
                return true;
            }

            return false;
        }
    }
}