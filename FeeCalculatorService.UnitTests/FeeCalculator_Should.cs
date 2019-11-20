using AutoFixture.Xunit2;
using Domain.Factories.Interfaces;
using Domain.Merchants;
using FeeCalculatorService.UnitTests.TestHelpers;
using Moq;
using Repository;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace FeeCalculatorService.UnitTests
{
    public class FeeCalculator_Should
    {
        [Theory]
        [AutoMoqData]
        public async Task Create_NewMerchant_IfMerchantDoesNotExist(
            [Frozen] Mock<IReadingFromFile> readingFromFileMock,
            [Frozen] Mock<IMerchantFactory> merchantFactory,
            [Frozen] Mock<IMerchantFeeFactory> merchantFeeFactory,
            Transaction transaction,
            MerchantInformation merchantInformation,
            Merchant merchant,
            FeeCalculator sut)
        {
            //Setup
            readingFromFileMock.Setup(x => x.ReadMerchantsFromRepositoryAsync()).Returns(ReadMerchantsFromRepositoryAsync);
            readingFromFileMock.Setup(x => x.GetMerchantDefaultValues(It.IsAny<string>())).Returns(merchantInformation);
            merchantFactory.Setup(x => x.CreateMerchant(It.Is<Transaction>(y => y.Equals(transaction)),
                It.Is<MerchantInformation>(y => y.Equals(merchantInformation)))).Returns(merchant);
            await sut.Calculate(transaction);
            merchantFactory.Verify(x => x.CreateMerchant(transaction, It.Is<MerchantInformation>(y => y.Equals(merchantInformation))), Times.Once);
            merchantFeeFactory.Verify(x => x.AddFee(), Times.Once);
        }

        [Theory]
        [AutoMoqData]
        public async Task DoNotCreate_NewMerchant_IfMerchantExist(
            [Frozen] Mock<IReadingFromFile> readingFromFileMock,
            [Frozen] Mock<IMerchantFactory> merchantFactory,
            [Frozen] Mock<IMerchantFeeFactory> merchantFeeFactory,
            Transaction transaction,
            [Frozen] MerchantInformation merchantInformation,
            Merchant merchant,
            FeeCalculator sut)
        {
            //Setup
            transaction.MerchantName = merchantInformation.MerchantName;
            readingFromFileMock.Setup(x => x.ReadMerchantsFromRepositoryAsync()).Returns(ReadMerchantsFromRepositoryAsync);
            readingFromFileMock.Setup(x => x.GetMerchantDefaultValues(It.IsAny<string>())).Returns(merchantInformation);
            merchantFactory.Setup(x => x.CreateMerchant(It.Is<Transaction>(y => y.Equals(transaction)),
                It.Is<MerchantInformation>(y => y.Equals(merchantInformation)))).Returns(merchant);
            //Act
            await sut.Calculate(transaction);
            await sut.Calculate(transaction);
            //Assert
            merchantFactory.Verify(x => x.CreateMerchant(transaction, It.Is<MerchantInformation>(y => y.Equals(merchantInformation))), Times.Once);
            merchantFeeFactory.Verify(x => x.AddFee(), Times.Exactly(2));
        }

        public async IAsyncEnumerable<MerchantInformation> ReadMerchantsFromRepositoryAsync()
        {
            var merchantList = new List<MerchantInformation> { new MerchantInformation() };
            foreach (var merchant in merchantList)
            {
                yield return merchant;
            }
        }
    }
}