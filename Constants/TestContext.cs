using System.Collections.Generic;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Models;

namespace AcceptanceTests.Constants
{
    internal static class TestContext
    {
        public static List<Transaction> Transaction { get; set; }
        public static List<Merchant> SpecialMerchants { get; set; }
    }
}