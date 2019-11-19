using Domain.Interfaces;
using System.Collections.Generic;

namespace Domain.Factories.Interfaces
{
    public interface IMerchantFeeFactory
    {
        List<IFee> AddFee();
    }
}