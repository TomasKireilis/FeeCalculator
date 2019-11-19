using Repository;
using System.Collections.Generic;

namespace AcceptanceTests.Constants
{
    internal static class TestContext
    {
        public static List<Transaction> Transaction { get; set; }
        public static List<decimal> TransactionFees { get; set; }
        public static List<MerchantInformation> Merchants { get; set; }
        public static MerchantInformation DefaultMerchant { get; set; }
    }
}