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
        private readonly IFeeFactory _merchantFeeFactory;

        public MerchantFactory(IFeeFactory merchantFeeFactory)
        {
            _merchantFeeFactory = merchantFeeFactory;
        }

        public async Task<Merchants.Merchant> CreateMerchant(Transaction transaction, MerchantInformation merchantInformation)
        {
            if (_bigMerchantValidation.ItIsBigMerchant(merchantInformation))
            {
                return new Merchants.Merchant(_merchantFeeFactory.CreateMerchantFeeFactory(MerchantType.Big), merchantInformation);
            }

            return new Merchants.Merchant(_merchantFeeFactory.CreateMerchantFeeFactory(MerchantType.Default), merchantInformation);
        }
    }
}