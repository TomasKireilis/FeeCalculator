Feature: TransactionPercentageDiscountFee		
		As a MobilePay accountant I want (TELIA 10% dicount for transaction, CIRCLE_K 20% dicount for transaction) to get Transaction Percentage Fee Discount,
		so that MobilePay would be more attractive to big merchants

Scenario Outline: CalculateTransactionPercentageFeeDiscountForCircleK
Given Merchant repository is populated with Transaction Percentage Fee Discount business logic
Given that transactions where made
| Date       | MerchantName | Amount |
| 2018-09-02 | CIRCLE_K     | 120    |
| 2018-09-04 | CIRCLE_K     | 200    |
| 2018-10-22 | CIRCLE_K     | 300    |
| 2018-10-29 | CIRCLE_K     | 150    |
When fees calculation app is executed
Then the output for Transaction Percentage Fee is
| FeeAmount |
| 0.96      |
| 1.60      |
| 2.40      |
| 1.20      |

Scenario Outline: CalculateTransactionPercentageFeeDiscountForTelia
Given Merchant repository is populated with Transaction Percentage Fee Discount business logic
Given that transactions where made
| Date       | MerchantName | Amount |
| 2018-09-02 | TELIA        | 120    |
| 2018-09-04 | TELIA        | 200    |
| 2018-10-22 | TELIA        | 300    |
| 2018-10-29 | TELIA        | 150    |
When fees calculation app is executed
Then the output for Transaction Percentage Fee is
| FeeAmount |
| 1.08      |
| 1.80      |
| 2.70      |
| 1.35      |