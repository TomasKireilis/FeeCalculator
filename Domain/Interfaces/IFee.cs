using Repository;

namespace Domain.Interfaces
{
    public interface IFee
    {
        Transaction Calculate(Transaction transaction, MerchantInformation merchantInformation);
    }
}