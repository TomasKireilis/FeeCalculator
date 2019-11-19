using Repository;
using System.Threading.Tasks;

namespace Domain.Factories.Interfaces
{
    public interface IMerchantFactory
    {
        Task<Merchants.Merchant> CreateMerchant(Transaction transaction, MerchantInformation merchantInformation);
    }
}