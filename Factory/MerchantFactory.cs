
using Domain;
using Models.Merchants;

namespace Models
{
    public class MerchantFactory
    {
        public Merchant CreateMerchant(Transaction transaction)
        {
            if (SpecialMerchantTransactionFee.GetInstance()._specialMerchants.Contains(transaction.MerchantName))
            { 
                return new Merchant(SpecialMerchantTransactionFee.GetInstance());
            }
            else
            {
              return new Merchant(MerchantTransactionFee.GetInstance());
            }
        }
    }
}
