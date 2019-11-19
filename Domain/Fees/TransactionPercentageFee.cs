using Domain.Interfaces;
using Repository;

namespace Domain.Fees
{
    public class TransactionPercentageFee : IFee
    {
        public Transaction Calculate(Transaction transaction, MerchantInformation merchantInformation)
        {
            transaction.TransactionPercentageFeeAmount = transaction.Amount * (merchantInformation.TransactionPercentageFee / 100);
            return transaction;
        }
    }
}