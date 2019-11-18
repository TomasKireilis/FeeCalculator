using System.Collections.Generic;
using Domain.Interfaces;
using Repository;

namespace Domain.Factories.Interfaces
{
    public interface IMerchantFeeFactory
    {
        List<IFee> AddFee(Transaction transaction, MerchantInformation merchantInformation);
    }
}