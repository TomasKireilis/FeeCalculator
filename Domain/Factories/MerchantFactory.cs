using Domain.Enum;
using Domain.Factories.Interfaces;
using Domain.MerchantTypeRules;
using Repository;
using System.Threading.Tasks;

namespace Domain.Factories
{
    public class MerchantFactory : IMerchantFactory
    {
        private readonly BigMerchantValidation _bigMerchantValidation = new BigMerchantValidation();
        private readonly IReadingFromFile _readingFromFile;
        private readonly IFeeFactory _merchantFeeFactory;

        public MerchantFactory(IReadingFromFile readingFromFile, IFeeFactory merchantFeeFactory)
        {
            _readingFromFile = readingFromFile;
            _merchantFeeFactory = merchantFeeFactory;
        }

        public async Task<Merchants.Merchant> CreateMerchant(Transaction transaction)
        {
            var merchantInfo = await GetMerchantInfo(transaction);
            if (_bigMerchantValidation.ItIsBigMerchant(merchantInfo))
            {
                return new Merchants.Merchant(_merchantFeeFactory.CreateMerchantFeeFactory(MerchantType.Big), merchantInfo);
            }

            return new Merchants.Merchant(_merchantFeeFactory.CreateMerchantFeeFactory(MerchantType.Default), merchantInfo);
        }

        private async Task<MerchantInformation> GetMerchantInfo(Transaction transaction)
        {
            await foreach (var merchant in _readingFromFile.ReadMerchantsFromRepositoryAsync())
            {
                if (merchant.MerchantName == transaction.MerchantName)
                {
                    return merchant;
                }
            }

            return _readingFromFile.GetMerchantDefaultValues(transaction.MerchantName);
        }
    }
}