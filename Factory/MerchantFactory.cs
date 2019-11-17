using Domain;
using Models;
using Models.Merchants;

namespace Factory
{
    public class MerchantFactory : IMerchantFactory
    {
        public Merchant CreateMerchant(Transaction transaction)
        {
            if (SpecialMerchantTransactionFee.GetInstance().SpecialMerchants.Contains(transaction.MerchantName))
            {
                return new Merchant(SpecialMerchantTransactionFee.GetInstance(), new Fees.Fees(), transaction.MerchantName);
            }

            return new Merchant(MerchantTransactionFee.GetInstance(), new Fees.Fees(), transaction.MerchantName);
        }
    }
}