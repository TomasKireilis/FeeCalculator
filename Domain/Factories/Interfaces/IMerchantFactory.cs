using Domain.Merchants;
using Repository;

namespace Domain.Factories.Interfaces
{
    public interface IMerchantFactory
    {
        Merchant CreateMerchant(Transaction transaction, MerchantInformation merchantInformation);
    }
}