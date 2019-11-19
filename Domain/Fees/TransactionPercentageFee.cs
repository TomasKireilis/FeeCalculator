using Domain.Interfaces;
using Repository;

namespace Domain.Fees
{
    internal class TransactionPercentageFee : IFee
    {
        public Transaction Calculate(Transaction transaction, MerchantInformation merchantInformation)
        {
            transaction.TransactionPercentageFeeAmount = transaction.Amount * (merchantInformation.TransactionPercentageFee / 100);
            return transaction;
        }
    }
}