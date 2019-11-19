using Domain.Factories.Interfaces;
using Domain.Fees;
using Domain.Interfaces;
using System.Collections.Generic;

namespace Domain.Factories
{
    public class MerchantFeeFactory : IMerchantFeeFactory
    {
        protected List<IFee> RegisteredFees = new List<IFee>()
        {
            new BasicFee(),
            new MonthlyFee()
        };

        public List<IFee> AddFee()
        {
            return RegisteredFees;
        }
    }
}