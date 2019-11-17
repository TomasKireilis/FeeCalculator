using System;
using Domain;
using Fees;

namespace Models.Merchants
{
    public class Merchant
    {
        public string Name { get; set; }
        public IMerchantTransactionFee TransactionFee;
        public IFees Fees;

        public Merchant(IMerchantTransactionFee transactionFee,IFees fees, string name)
        {
            TransactionFee = transactionFee;
            Fees = fees;
            Name = name;
        }
    }
}