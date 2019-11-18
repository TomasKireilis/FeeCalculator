using Domain.Interfaces;
using Repository;

namespace Domain.Fees
{
    internal class BasicFee : IFee
    {
        public bool NeedToCalculate(Transaction transaction, MerchantInformation merchantInformation)
        {
            return true;
        }

        public Transaction Calculate(Transaction transaction, MerchantInformation merchantInformation)
        {
            transaction.BasicFeeAmount = transaction.Amount * (merchantInformation.BasicFee / 100);
            return transaction;
        }
    }
}