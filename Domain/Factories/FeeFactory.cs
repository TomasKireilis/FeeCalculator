using Domain.Enums;
using Domain.Factories.Interfaces;
using System;

namespace Domain.Factories
{
    public class FeeFactory : IFeeFactory
    {
        public IMerchantFeeFactory CreateMerchantFeeFactory(MerchantStatus merchantStatus)
        {
            switch (merchantStatus)
            {
                case MerchantStatus.Default:
                    return new MerchantFeeFactory();

                case MerchantStatus.Big:
                    return new BigMerchantFeeFactory();

                default:
                    throw new ArgumentOutOfRangeException(nameof(merchantStatus), merchantStatus, null);
            }
        }
    }
}