using Models;

namespace Homework_Tomas_Kireilis.Interfaces
{
    public interface IFeeCalculator
    {
        Transaction Calculate(Transaction transaction);
    }
}