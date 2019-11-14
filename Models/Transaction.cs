using System;

namespace Models
{
    public class Transaction
    {
        public DateTimeOffset Date { get; set; }
        public string MerchantName { get; set; }
        public decimal Amount { get; set; }
        public decimal BasicFeeAmount { get; set; }
        public decimal MonthlyFeeAmount { get; set; }
    }
}