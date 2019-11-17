using Models;
using Models.Merchants;

namespace Factory
{
    public interface IMerchantFactory
    {
        Merchant CreateMerchant(Transaction transaction);
    }
}