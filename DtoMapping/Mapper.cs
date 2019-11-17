using Fees.Models;
using Models;

namespace DtoMapping
{
    public class Mapper : IMapper
    {
        public TransactionFee MapTransactionToTransationFee(Transaction transaction, decimal basicFee = 0, decimal monthlyFee = 0)
        {
            return new TransactionFee()
            {
                Date = transaction.Date,
                MerchantName = transaction.MerchantName,
                Amount = transaction.Amount,
                BasicFeeAmount = transaction.BasicFeeAmount,
                MonthlyFeeAmount = transaction.MonthlyFeeAmount,
                BasicFee = basicFee,
                MonthlyFee = monthlyFee
            };
        }

        public Transaction MapTransactionFeeToTransaction(TransactionFee transactionFees)
        {
            return new Transaction()
            {
                Date = transactionFees.Date,
                MerchantName = transactionFees.MerchantName,
                Amount = transactionFees.Amount,
                BasicFeeAmount = transactionFees.BasicFeeAmount,
                MonthlyFeeAmount = transactionFees.MonthlyFeeAmount
            };
        }
    }
}