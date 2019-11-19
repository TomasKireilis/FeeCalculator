using System;

namespace Repository
{
    public class Transaction
    {
        public DateTimeOffset Date { get; set; }
        public string MerchantName { get; set; }
        public decimal Amount { get; set; }
        public decimal TransactionPercentageFeeAmount { get; set; }
        public decimal InvoiceFixedFeeAmount { get; set; }
    }
}