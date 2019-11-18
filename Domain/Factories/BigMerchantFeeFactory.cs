using System.Collections.Generic;
using Domain.Fees;
using Domain.Interfaces;

namespace Domain.Factories
{
    internal class BigMerchantFeeFactory : MerchantFeeFactory
    {
        public BigMerchantFeeFactory()
        {
            base.RegisteredFees.AddRange(RegisteredFees);
        }
        protected new List<IFee> RegisteredFees = new List<IFee>()
        {
            new BasicFeeDiscount(),
        };
    }
}