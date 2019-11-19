using Repository;
using System.Threading.Tasks;

namespace FeeCalculator
{
    public interface IFeeCalculator
    {
        Task<Transaction> Calculate(Transaction transaction);
    }
}