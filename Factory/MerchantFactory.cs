
using Domain;
using Models.Merchants;

namespace Models
{
    public class MerchantFactory : IMerchantFactory
    {
        public Merchant CreateMerchant(Transaction transaction)
        {
            if (SpecialMerchantTransactionFee.GetInstance()._specialMerchants.Contains(transaction.MerchantName))
            {
                return new Merchant(SpecialMerchantTransactionFee.GetInstance(), new Fees.Fees(),transaction.MerchantName);
            }
            else
            {
                return new Merchant(MerchantTransactionFee.GetInstance(), new Fees.Fees(),transaction.MerchantName);
            }
        }
    }
}
