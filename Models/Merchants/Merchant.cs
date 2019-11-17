using System;
using Domain;

namespace Models.Merchants
{
    public class Merchant
    {
        public Transaction LastTransactionWithMonthlyFee { get; set; }
        public string Name { get; set; }
        public MerchantTransactionFee TransactionFee;

        public Merchant(MerchantTransactionFee transactionFee)
        {
            TransactionFee = transactionFee;
        }
    }
}