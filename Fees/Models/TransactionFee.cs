using System;

namespace Fees.Models
{
    public class TransactionFee
    {
        public DateTimeOffset Date { get; set; }
        public string MerchantName { get; set; }
        public decimal Amount { get; set; }
        public decimal BasicFee { get; set; }
        public decimal MonthlyFee { get; set; }
        public decimal BasicFeeAmount { get; set; }
        public decimal MonthlyFeeAmount { get; set; }
    }
}