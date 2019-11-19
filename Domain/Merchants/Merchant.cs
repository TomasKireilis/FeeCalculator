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
            var fees = _feeFactory.AddFee();
            foreach (var fee in fees)
            {
                transaction = fee.Calculate(transaction, MerchantInformation);
            }
            transaction.TransactionPercentageFeeAmount = decimal.Round(transaction.TransactionPercentageFeeAmount, 2);
            transaction.InvoiceFixedFeeAmount = decimal.Round(transaction.InvoiceFixedFeeAmount, 2);
            return transaction;
        }
    }
}