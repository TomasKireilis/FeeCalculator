namespace Repository
{
    public class MerchantInformation
    {
        public string MerchantName { get; set; }
        public string Status { get; set; }
        public decimal TransactionPercentageFee { get; set; }
        public decimal TransactionPercentageDiscountFee { get; set; }
        public decimal InvoiceFixedFee { get; set; }

        public MerchantInformation Clone()
        {
            return (MerchantInformation)MemberwiseClone();
        }
    }
}