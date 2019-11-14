using Homework_Tomas_Kireilis.Interfaces;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Homework_Tomas_Kireilis
{
    public class FeeCalculator : IFeeCalculator
    {
        private readonly IFees _fees;
        private readonly decimal _basicTransactionPercentageFee;
        private readonly decimal _monthlyTransactionPercentageFee;
        private readonly List<Merchant> _merchants;

        public FeeCalculator(IFees fees, decimal transactionPercentageFee, List<Merchant> merchants, decimal monthlyTransactionPercentageFee)
        {
            _fees = fees;
            _basicTransactionPercentageFee = transactionPercentageFee;
            _merchants = merchants;
            _monthlyTransactionPercentageFee = monthlyTransactionPercentageFee;
        }

        public Transaction Calculate(Transaction transaction)
        {
            var merchant = CheckMerchantValidity(transaction);
            transaction.BasicFeeAmount =
                    _fees.TransactionPercentageFee(transaction.Amount, _basicTransactionPercentageFee);

            if (CheckIfNeedToAddMonthlyFeeToTransaction(transaction))
            {
                transaction.MonthlyFeeAmount =
                    _fees.TransactionFixedFee(_monthlyTransactionPercentageFee);
            }
            if (merchant != null)
            {
                if (merchant.FeeDiscount != 0)
                {
                    transaction.BasicFeeAmount =
                        _fees.TransactionPercentageFeeDiscount(transaction.BasicFeeAmount, merchant.FeeDiscount);
                }

                transaction.BasicFeeAmount = decimal.Round(transaction.BasicFeeAmount, 2);
                ChangeLastMonthlyTransactionToMerchant(transaction);
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
                return AddNewMerchant(transaction);
            }

            return merchants[0];
        }

        private Merchant AddNewMerchant(Transaction transaction)
        {
            var merchant = new Merchant
            {
                Name = transaction.MerchantName
            };
            _merchants.Add(
                merchant
            );
            return merchant;
        }

        private void ChangeLastMonthlyTransactionToMerchant(Transaction transaction)
        {
            var merchantIndex = _merchants.FindIndex(x => x.Name == transaction.MerchantName);
            _merchants[merchantIndex].LastTransactionWithMonthlyFee = transaction;
        }

        private bool CheckIfNeedToAddMonthlyFeeToTransaction(Transaction transaction)
        {
            var merchantIndex = _merchants.FindIndex(x => x.Name == transaction.MerchantName);
            var lastMonthlyTransaction = _merchants[merchantIndex].LastTransactionWithMonthlyFee;
            if (lastMonthlyTransaction == null || CheckIfDateIsNewerForMonthlyFee(lastMonthlyTransaction.Date, transaction.Date))
            {
                return true;
            }

            return false;
        }

        private bool CheckIfDateIsNewerForMonthlyFee(DateTimeOffset lastFeeDate, DateTimeOffset transactionDate)
        {
            if (lastFeeDate.Year == transactionDate.Year && lastFeeDate.Month < transactionDate.Month)
            {
                return true;
            }

            if (lastFeeDate.Year < transactionDate.Year)
            {
                return true;
            }

            return false;
        }
    }
}