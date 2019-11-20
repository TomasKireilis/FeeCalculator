using AutoFixture.Xunit2;
using FeeCalculatorService.UnitTests.TestHelpers;
using Moq;
using Repository;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace FeeCalculatorService.UnitTests
{
    public class Program_Should
    {
        [Theory]
        [AutoMoqData]
        public async Task CalculateFees_When_ProgramIsExecuted(
            [Frozen] Mock<IReadingFromFile> readingFromFile,
            [Frozen] Mock<IFeeCalculator> feeCalculator,
            Transaction transaction,
            [Frozen] MerchantInformation merchantInformation)
        {
            //Setup
            transaction.MerchantName = merchantInformation.MerchantName;
            readingFromFile.Setup(x => x.ReadMerchantsFromRepositoryAsync()).Returns(ReadMerchantsFromRepositoryAsync);
            readingFromFile.Setup(x => x.GetMerchantDefaultValues(It.IsAny<string>())).Returns(merchantInformation);
            readingFromFile.Setup(x => x.ReadTransactionsFromRepositoryAsync()).Returns(ReadTransactionsFromRepositoryAsync);
            feeCalculator.Setup(x => x.Calculate(It.IsAny<Transaction>())).ReturnsAsync(transaction);
            //Act
            await Program.CalculateFee(readingFromFile.Object, feeCalculator.Object);
            //Assert
            feeCalculator.Verify(x => x.Calculate(It.IsAny<Transaction>()), Times.Exactly(2));
        }

        public async IAsyncEnumerable<MerchantInformation> ReadMerchantsFromRepositoryAsync()
        {
            var merchantList = new List<MerchantInformation> { new MerchantInformation() };
            foreach (var merchant in merchantList)
            {
                yield return merchant;
            }
        }

        public async IAsyncEnumerable<Transaction> ReadTransactionsFromRepositoryAsync()
        {
            var transactions = new List<Transaction> { new Transaction(), new Transaction() };
            foreach (var transaction in transactions)
            {
                yield return transaction;
            }
        }
    }
}