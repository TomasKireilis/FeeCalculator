using Domain.Factories.Interfaces;
using Repository;

namespace Domain.Merchants
{
    public class Merchant
    {
        private readonly IMerchantFeeFactory _feeFactory;
        public MerchantInformation MerchantInformation { get; }

        public Merchant(IMerchantFeeFactory feeFactory, MerchantInformation merchantInformation)
        {
            _feeFactory = feeFactory;
            MerchantInformation = merchantInformation;
        }

        public Transaction CalculateFee(Transaction transaction)
        {
            var fees = _feeFactory.AddFee(transaction, MerchantInformation);
            foreach (var fee in fees)
            {
                transaction = fee.Calculate(transaction, MerchantInformation);
            }
            transaction.BasicFeeAmount = decimal.Round(transaction.BasicFeeAmount, 2);
            transaction.MonthlyFeeAmount = decimal.Round(transaction.MonthlyFeeAmount, 2);
            return transaction;
        }
    }
}