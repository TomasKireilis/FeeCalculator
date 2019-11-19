using Domain.Interfaces;
using Repository;

namespace Domain.Fees
{
    internal class BasicFee : IFee
    {
        public Transaction Calculate(Transaction transaction, MerchantInformation merchantInformation)
        {
            transaction.BasicFeeAmount = transaction.Amount * (merchantInformation.BasicFee / 100);
            return transaction;
        }
    }
}