using Domain.Factories.Interfaces;
using Domain.Fees;
using Domain.Interfaces;
using Repository;
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

        public List<IFee> AddFee(Transaction transaction, MerchantInformation merchantInformation)
        {
            var preparedFees = new List<IFee>();
            foreach (var registeredFee in RegisteredFees)
            {
                if (registeredFee.NeedToCalculate(transaction, merchantInformation))
                {
                    preparedFees.Add(registeredFee);
                }
            }

            return preparedFees;
        }
    }
}