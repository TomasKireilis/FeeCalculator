Feature: InvoiceFixedFee		
		As a MobilePay accountant I want charge merchants Invoice Fixed Fee (29 DKK) every month,
		so that we could cover our expenses for sending physical invoices to merchants

Scenario Outline: CalculateInvoiceFixedFee
Given Merchant repository is populated with Invoice Fixed Fee business logic
Given that transactions where made
| Date       | MerchantName | Amount |
| 2018-09-02 | 7-ELEVEN     | 120    |
| 2018-09-04 | NETTO        | 200    |
| 2018-10-22 | 7-ELEVEN     | 300    |
| 2018-10-29 | 7-ELEVEN     | 150    |
When fees calculation app is executed
Then the output for Invoice Fixed Fee is
| FeeAmount |
| 30.20     |
| 31.00     |
| 32.00     |
| 1.50      |