using System.Threading.Tasks;
using Repository;

namespace FeeCalculator
{
    public interface IFeeCalculator
    {
        Task<Transaction> Calculate(Transaction transaction);
    }
}