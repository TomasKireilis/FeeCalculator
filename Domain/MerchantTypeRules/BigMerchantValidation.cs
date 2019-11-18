using Repository;

namespace Domain.MerchantTypeRules
{
    public class BigMerchantValidation
    {
        public const string BigMerchantStatus = "Big";

        public bool ItIsBigMerchant(MerchantInformation merchantInformation)
        {
            return ByDefaultMerchantStatus(merchantInformation);
        }

        private bool ByDefaultMerchantStatus(MerchantInformation merchantInformation)
        {
            if (merchantInformation.Status == BigMerchantStatus)
            {
                return true;
            }

            return false;
        }
    }
}