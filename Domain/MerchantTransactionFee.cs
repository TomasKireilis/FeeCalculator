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

        protected MerchantTransactionFee()
        {
        }

        public decimal DefaultFeeForTransactionValue(string transactionMerchantName = "")
        {
            if (TryGetValueFromSettings($"{DefaultFeeForTransaction}.{transactionMerchantName}", out decimal value))
            {
                return value;
            }
            if (TryGetValueFromSettings($"{DefaultFeeForTransaction}.", out decimal defaultValue))
            {
                return defaultValue;
            }
            throw new ArgumentException($"{transactionMerchantName} was not found in default fees");
        }

        public decimal MonthlyFeeForTransactionValue()
        {
            if (TryGetValueFromSettings(MonthlyFeeForTransaction, out decimal defaultValue))
            {
                return defaultValue;
            }
            throw new ArgumentException($"{MonthlyFeeForTransaction} was not found in monthly fees");
        }

        protected bool TryGetValueFromSettings(string key, out decimal decimalValue)
        {
            if (DefaultFees.TryGetValue(key, out decimal value))
            {
                decimalValue = value;
                return true;
            }
            decimalValue = 0;
            return false;
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