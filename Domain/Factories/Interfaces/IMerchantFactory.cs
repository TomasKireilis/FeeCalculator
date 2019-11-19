using System.Threading.Tasks;
using Repository;

namespace Domain.Factories.Interfaces
{
    public interface IMerchantFactory
    {
        Task<Merchants.Merchant> CreateMerchant(Transaction transaction, MerchantInformation merchantInformation);
    }
}