using Fees.Models;
using Models;

namespace DtoMapping
{
    public interface IMapper
    {
        Transaction MapTransactionFeeToTransaction(TransactionFee transactionFees);

        TransactionFee MapTransactionToTransactionFee(Transaction transaction, decimal basicFee = 0, decimal monthlyFee = 0);
    }
}