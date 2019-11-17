using System;
using System.Collections.Generic;
namespace Domain
{
    public class MerchantTransactionFee : IMerchantTransactionFee
    {
        private static MerchantTransactionFee _instance;
        private static object syncLock = new object();

        private const string DefaultFeeForTransaction = "DefaultFeeForTransaction";
        private const string MonthlyFeeForTransaction = "MonthlyFeeForTransaction";
        protected Dictionary<string, decimal> DefaultFees { get; set; } = new Dictionary<string, decimal>
        {
            { $"{DefaultFeeForTransaction}." , 1 },
            { $"{MonthlyFeeForTransaction}", 29}
        };

        public decimal DefaultFeeForTransactionValue(string transactionMerchantName="")
        {
            return GetValueFromSettings($"{DefaultFeeForTransaction}.{transactionMerchantName}");
        }
        public decimal MonthlyFeeForTransactionValue()
        {
            return GetValueFromSettings(MonthlyFeeForTransaction);
        }
        protected decimal GetValueFromSettings(string key)
        {
            if (DefaultFees.TryGetValue(key, out decimal value))
            {
                return value;
            }
            throw new ArgumentException($"{key} was not found in default fees");
        }
        public static MerchantTransactionFee GetInstance()
        {
            if (_instance == null)
            {
                lock (syncLock)
                {
                    if (_instance == null)
                    {
                        _instance = new MerchantTransactionFee();
                    }
                }
            }

            return _instance;
        }
    }
}