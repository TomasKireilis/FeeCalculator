using Domain.Interfaces;
using Repository;

namespace Domain.Fees
{
    internal class BasicFeeDiscount : IFee
    {
        public bool NeedToCalculate(Transaction transaction, MerchantInformation merchantInformation)
        {
            return true;
        }

        public Transaction Calculate(Transaction transaction, MerchantInformation merchantInformation)
        {
            transaction.BasicFeeAmount -=
                  (transaction.BasicFeeAmount / 100 * merchantInformation.BasicFeeDiscount);
            return transaction;
        }
    }
}