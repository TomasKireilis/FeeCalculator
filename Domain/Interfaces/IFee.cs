using Repository;

namespace Domain.Interfaces
{
    public interface IFee
    {
        bool NeedToCalculate(Transaction transaction, MerchantInformation merchantInformation);

        Transaction Calculate(Transaction transaction, MerchantInformation merchantInformation);
    }
}