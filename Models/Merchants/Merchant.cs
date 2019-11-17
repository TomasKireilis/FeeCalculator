using System;
using Domain;
using Fees;

namespace Models.Merchants
{
    public class Merchant
    {
        public Transaction LastTransactionWithMonthlyFee { get; set; }
        public string Name { get; set; }
        public IMerchantTransactionFee TransactionFee;
        public IFees Fees;

        public Merchant(IMerchantTransactionFee transactionFee,IFees fees)
        {
            TransactionFee = transactionFee;
            Fees = fees;
        }
    }
}