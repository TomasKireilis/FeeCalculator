using Domain.MerchantTypeRules;
using Repository;
using Xunit;

namespace Domain.UnitTests
{
    public class BigMerchantValidation_Should
    {
        [Fact]
        public void ReturnTrue_When_MerchantStateIsBig()
        {
            //setup
            var validation = new BigMerchantValidation();
            var merchantInformation = new MerchantInformation { Status = "BIG" };

            //act
            var response = validation.ItIsBigMerchant(merchantInformation);

            //Assert
            Assert.True(response);
        }

        [Fact]
        public void ReturnFalse_When_MerchantStateIsNotBig()
        {
            //setup
            var validation = new BigMerchantValidation();
            var merchantInformation = new MerchantInformation { Status = "DEFAULT" };

            //act
            var response = validation.ItIsBigMerchant(merchantInformation);

            //Assert
            Assert.False(response);
        }
    }
}