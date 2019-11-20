using Domain.Enums;
using Domain.Factories.Interfaces;
using Domain.Merchants;
using Domain.MerchantTypeRules;
using Repository;

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

        public Merchant CreateMerchant(Transaction transaction, MerchantInformation merchantInformation)
        {
            if (_bigMerchantValidation.ItIsBigMerchant(merchantInformation))
            {
                return new Merchant(_merchantFeeFactory.CreateMerchantFeeFactory(MerchantStatus.Big), merchantInformation);
            }

            return new Merchant(_merchantFeeFactory.CreateMerchantFeeFactory(MerchantStatus.Default), merchantInformation);
        }
    }
}