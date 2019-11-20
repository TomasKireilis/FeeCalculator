Feature: TransactionPercentageFee		
		As a MobilePay accountant I want merchants to be charged Transaction Percentage Fee (1% of transaction amount),
		so that MobilePay would still be cheapest solution in the market and we could earn enough money
		to cover our expenses

Scenario Outline: CalculateTransactionPercentageFee
Given Merchant repository is populated with Transaction Percentage Fee business logic
Given that transactions where made
| Date       | MerchantName | Amount | 
| 2018-09-02 | CIRCLE_K     | 120    | 
| 2018-09-04 | TELIA        | 200    | 
| 2018-10-22 | CIRCLE_K     | 300    | 
| 2018-10-29 | CIRCLE_K     | 150    | 
When fees calculation app is executed
Then the output for Transaction Percentage Fee is
| FeeAmount |
| 1.20      |
| 2.00      |
| 3.00      |
| 1.50      |