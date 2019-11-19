Feature: TransactionPercentageFee
		As a MobilePay accountant I want merchants to be charged Transaction Percentage Fee (1% of transaction 
		amount), so that MobilePay would still be cheapest solution in the market and we could earn enough money
		to cover our expenses

Scenario: CalculateFees
Given that <Amount> DKK transaction is made to <MerchantName> on <date>
Given that <Amount> DKK transaction is made to <MerchantName> on <date>
Given that <Amount> DKK transaction is made to <MerchantName> on <date>
Given that <Amount> DKK transaction is made to <MerchantName> on <date>
When fees calculation app is executed
Then the output is
| Date       | MerchantName | Amount | FeeAmount |
| 2018-09-02 | CIRCLE_K     | 120    | 1.20      |
| 2018-09-04 | TELIA        | 200    | 2.00      |
| 2018-10-22 | CIRCLE_K     | 300    | 3.00      |
| 2018-10-29 | CIRCLE_K     | 150    | 1.50      |