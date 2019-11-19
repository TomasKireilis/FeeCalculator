using Domain.Interfaces;
using Repository;

namespace Domain.Fees
{
    internal class BasicFeeDiscount : IFee
    {
        public Transaction Calculate(Transaction transaction, MerchantInformation merchantInformation)
        {
            transaction.BasicFeeAmount -=
                  (transaction.BasicFeeAmount / 100 * merchantInformation.BasicFeeDiscount);
            return transaction;
        }
    }
}