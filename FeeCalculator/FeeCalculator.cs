using DtoMapping;
using Factory;
using FeeCalculator.Interfaces;
using Models;
using Models.Merchants;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FeeCalculator
{
    public class FeeCalculator : IFeeCalculator
    {
        private readonly List<Merchant> _merchants = new List<Merchant>();
        private readonly IMerchantFactory _merchantFactory;
        public readonly IMapper Mapper;

        public FeeCalculator(IMapper mapper, IMerchantFactory merchantFactory)
        {
            Mapper = mapper;
            _merchantFactory = merchantFactory;
        }

        public Transaction Calculate(Transaction transaction)
        {
            var merchant = CheckMerchantValidity(transaction);
            if (merchant != null)
            {
                var calculatedFee = merchant.Fees.CalculateFee(
                    Mapper.MapTransactionToTransactionFee(
                        transaction,
                        merchant.TransactionFee.DefaultFeeForTransactionValue(merchant.Name),
                        merchant.TransactionFee.MonthlyFeeForTransactionValue()));

                transaction = Mapper.MapTransactionFeeToTransaction(calculatedFee);
                transaction.BasicFeeAmount = decimal.Round(transaction.BasicFeeAmount, 2);

                return transaction;
            }

            throw new Exception("Could not calculate fee");
        }

        private Merchant CheckMerchantValidity(Transaction transaction)
        {
            var merchants = _merchants.FindAll(x => x.Name == transaction.MerchantName).ToList();

            if (merchants.Count > 1)
            {
                throw new Exception(
                    $"Found merchants with duplicated names. Could not calculate fee for merchant name :{transaction.MerchantName}");
            }

            if (merchants.Count == 0)
            {
                var merchant = _merchantFactory.CreateMerchant(transaction);
                _merchants.Add(merchant);
                return merchant;
            }

            return merchants[0];
        }
    }
}