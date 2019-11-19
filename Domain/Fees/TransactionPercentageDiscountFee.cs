using Domain.Interfaces;
using Repository;

namespace Domain.Fees
{
    public class TransactionPercentageDiscountFee : IFee
    {
        public Transaction Calculate(Transaction transaction, MerchantInformation merchantInformation)
        {
            transaction.TransactionPercentageFeeAmount -=
                  transaction.TransactionPercentageFeeAmount / 100 * merchantInformation.TransactionPercentageDiscountFee;
            return transaction;
        }
    }
}