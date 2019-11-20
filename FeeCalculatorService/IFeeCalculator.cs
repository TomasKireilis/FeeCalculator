using Repository;
using System.Threading.Tasks;

namespace FeeCalculatorService
{
    public interface IFeeCalculator
    {
        Task<Transaction> Calculate(Transaction transaction);
    }
}