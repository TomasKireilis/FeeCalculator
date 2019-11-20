using Domain.Enums;

namespace Domain.Factories.Interfaces
{
    public interface IFeeFactory
    {
        IMerchantFeeFactory CreateMerchantFeeFactory(MerchantStatus merchantStatus);
    }
}