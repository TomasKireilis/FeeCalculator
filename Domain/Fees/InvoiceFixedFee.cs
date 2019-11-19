using Domain.Interfaces;
using Repository;
using System;

namespace Domain.Fees
{
    public class InvoiceFixedFee : IFee
    {
        public DateTimeOffset LastInvoiceFixedFeeDate;

        public Transaction Calculate(Transaction transaction, MerchantInformation merchantInformation)
        {
            if (!NeedToCalculate(transaction))
            {
                return transaction;
            }
            transaction.InvoiceFixedFeeAmount += merchantInformation.InvoiceFixedFee;
            LastInvoiceFixedFeeDate = transaction.Date;
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
            if (LastInvoiceFixedFeeDate.Year == transactionDate.Year && LastInvoiceFixedFeeDate.Month < transactionDate.Month)
            {
                return true;
            }

            if (LastInvoiceFixedFeeDate.Year < transactionDate.Year)
            {
                return true;
            }

            return false;
        }
    }
}