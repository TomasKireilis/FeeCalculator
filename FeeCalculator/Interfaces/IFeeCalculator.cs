using Models;

namespace FeeCalculator.Interfaces
{
    public interface IFeeCalculator
    {
        Transaction Calculate(Transaction transaction);
    }
}