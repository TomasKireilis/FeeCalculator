using Domain.Enums;
using Repository;

namespace Domain.MerchantTypeRules
{
    public class BigMerchantValidation
    {
        public bool ItIsBigMerchant(MerchantInformation merchantInformation)
        {
            return ByDefaultMerchantStatus(merchantInformation);
        }

        private bool ByDefaultMerchantStatus(MerchantInformation merchantInformation)
        {
            if (merchantInformation.Status == MerchantStatus.Big.ToString().ToUpper())
            {
                return true;
            }

            return false;
        }
    }
}