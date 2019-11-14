using Homework_Tomas_Kireilis.Interfaces;
using System;

namespace Homework_Tomas_Kireilis
{
    public class Fees : IFees
    {
        public decimal TransactionPercentageFee(decimal amount, decimal feeAmount)
        {
            if (feeAmount > 0)
            {
                return amount * (feeAmount / 100);
            }
            throw new ArgumentException($"Wrong Fee value: {feeAmount}");
        }

        public decimal TransactionPercentageFeeDiscount(decimal amount, decimal feeAmount)
        {
            if (feeAmount > 0)
            {
                return amount * ((100 - feeAmount) / 100);
            }
            throw new ArgumentException($"Wrong Fee value: {feeAmount}");
        }

        public decimal TransactionFixedFee(decimal feeAmount)
        {
            if (feeAmount > 0)
            {
                return feeAmount;
            }
            throw new ArgumentException($"Wrong Fee value: {feeAmount}");
        }
    }
}