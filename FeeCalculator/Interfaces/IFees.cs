namespace Homework_Tomas_Kireilis.Interfaces
{
    public interface IFees
    {
        decimal TransactionPercentageFee(decimal amount, decimal feeAmount);

        decimal TransactionPercentageFeeDiscount(decimal amount, decimal feeAmount);

        decimal TransactionFixedFee(decimal feeAmount);
    }
}