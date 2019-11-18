using Domain.Enum;
using Domain.Factories.Interfaces;
using System;

namespace Domain.Factories
{
    public class FeeFactory : IFeeFactory
    {
        public IMerchantFeeFactory CreateMerchantFeeFactory(MerchantType merchantType)
        {
            switch (merchantType)
            {
                case MerchantType.Default:
                    return new MerchantFeeFactory();

                case MerchantType.Big:
                    return new BigMerchantFeeFactory();

                default:
                    throw new ArgumentOutOfRangeException(nameof(merchantType), merchantType, null);
            }
        }
    }
}