using Domain.Factories.Interfaces;
using Domain.Merchants;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeeCalculator
{
    public class FeeCalculator : IFeeCalculator
    {
        private readonly List<Merchant> _merchants = new List<Merchant>();
        private readonly IMerchantFactory _merchantFactory;

        public FeeCalculator(IMerchantFactory merchantFactory)
        {
            _merchantFactory = merchantFactory;
        }

        public async Task<Transaction> Calculate(Transaction transaction)
        {
            var merchant = await CreateMerchantIfNotExist(transaction);
            if (merchant != null)
            {
                var calculatedTransactionWithFee = merchant.CalculateFee(transaction);
                return calculatedTransactionWithFee;
            }

            throw new Exception("Could not calculate fee");
        }

        private async Task<Merchant> CreateMerchantIfNotExist(Transaction transaction)
        {
            var merchants = _merchants.FindAll(x => x.MerchantInformation.MerchantName == transaction.MerchantName).ToList();

            if (merchants.Count > 1)
            {
                //    throw new Exception(
                //       $"Found merchants with duplicated names. Could not calculate fee for merchant name :{transaction.MerchantName}");
            }

            if (merchants.Count == 0)
            {
                var merchant = await _merchantFactory.CreateMerchant(transaction);
                _merchants.Add(merchant);
                return merchant;
            }

            return merchants[0];
        }
    }
}