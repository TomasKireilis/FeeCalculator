using Domain.Fees;
using Domain.Interfaces;
using System.Collections.Generic;

namespace Domain.Factories
{
    internal class BigMerchantFeeFactory : MerchantFeeFactory
    {
        public BigMerchantFeeFactory()
        {
            base.RegisteredFees = RegisteredFees;
        }

        protected new List<IFee> RegisteredFees = new List<IFee>()
        {
            new BasicFee(),
            new BasicFeeDiscount(),
            new MonthlyFee()
        };
    }
}