using Models.Merchants;

namespace Models
{
    public interface IMerchantFactory
    {
        Merchant CreateMerchant(Transaction transaction);
    }
}