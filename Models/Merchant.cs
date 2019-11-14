namespace Models
{
    public class Merchant
    {
        public Transaction LastTransactionWithMonthlyFee { get; set; }
        public string Name { get; set; }
        public decimal FeeDiscount { get; set; }
    }
}