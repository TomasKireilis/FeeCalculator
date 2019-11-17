namespace Domain
{
    public interface IMerchantTransactionFee
    {
        decimal DefaultFeeForTransactionValue(string transactionMerchantName = "");
        decimal MonthlyFeeForTransactionValue();
    }
}