using Fees.Models;

namespace Fees
{
    public interface IFees
    {
        TransactionFee CalculateFee(TransactionFee transactionFee);
    }
}