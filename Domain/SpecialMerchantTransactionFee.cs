

using System.Collections.Generic;

namespace Domain
{
    public class SpecialMerchantTransactionFee : MerchantTransactionFee
    {
        private static SpecialMerchantTransactionFee _instance;
        private static object syncLock = new object();
        public decimal FeeDiscount { get; set; }
        private const string DefaultFeeForTransaction = "DefaultFeeForTransaction";
        private const string MonthlyFeeForTransaction = "MonthlyFeeForTransaction";
        private const string TeliaName = "TELIA";
        private const string CircleKName = "CIRCLE_K";
        private readonly Dictionary<string, decimal> _defaultFees = new Dictionary<string, decimal>
        {
            { $"{DefaultFeeForTransaction}.{TeliaName}" ,(decimal) 0.9 },
            { $"{DefaultFeeForTransaction}.{CircleKName}" ,(decimal) 0.8 },
            { $"{MonthlyFeeForTransaction}", 29}
        };

        public readonly List<string> _specialMerchants = new List<string>
        {
            { TeliaName},
            { CircleKName}
        };
        public SpecialMerchantTransactionFee()
        {
          
        }
        private void ChangeSettingsDictionary()
        {
            foreach (var fee in _defaultFees)
            {
                if (DefaultFees.ContainsKey(fee.Key))
                {
                    DefaultFees[fee.Key] = fee.Value;
                }
                else
                {
                    DefaultFees.Add(fee.Key, fee.Value);
                }
            }
        }
        public new static SpecialMerchantTransactionFee GetInstance()
        {
            if (_instance == null)
            {
                lock (syncLock)
                {
                    if (_instance == null)
                    {
                        _instance = new SpecialMerchantTransactionFee();
                        _instance.ChangeSettingsDictionary();
                    }
                }
            }
            
            return _instance;
        }
    }
}