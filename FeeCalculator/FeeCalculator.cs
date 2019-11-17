using Domain;
using DtoMapping;
using Homework_Tomas_Kireilis.Interfaces;
using Models;
using Models.Merchants;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Homework_Tomas_Kireilis
{
    public class FeeCalculator : IFeeCalculator
    {
        private readonly List<Merchant> _merchants = new List<Merchant>();
        private readonly IMerchantFactory merchantFactory;
        public readonly IMapper mapper;
        public FeeCalculator(IMapper Mapper, IMerchantFactory merchantFactory)
        {
            mapper = Mapper;
            this.merchantFactory = merchantFactory;
        }

        public Transaction Calculate(Transaction transaction)
        {
            var merchant = CheckMerchantValidity(transaction);
            if (merchant != null)
            {
                var calculatedFee = merchant.Fees.CalculateFee(
                    mapper.MapTransactionToTransationFee(
                        transaction,
                        merchant.TransactionFee.DefaultFeeForTransactionValue(merchant.Name),
                        merchant.TransactionFee.MonthlyFeeForTransactionValue()));

                transaction = mapper.MapTransactionFeeToTransaction(calculatedFee);
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
                var merchant = merchantFactory.CreateMerchant(transaction);
                _merchants.Add(merchant);
                return merchant;
            }

            return merchants[0];
        }
    }
}