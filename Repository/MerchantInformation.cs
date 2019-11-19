namespace Repository
{
    public class MerchantInformation
    {
        public string MerchantName { get; set; }
        public string Status { get; set; }
        public decimal BasicFee { get; set; }
        public decimal BasicFeeDiscount { get; set; }
        public decimal MonthlyFee { get; set; }
        public decimal WeeklyFee { get; set; }
        public MerchantInformation Clone()
        {
            return (MerchantInformation) MemberwiseClone();
        }
    }
}