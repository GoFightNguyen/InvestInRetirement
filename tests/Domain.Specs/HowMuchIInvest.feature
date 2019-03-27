Feature: How much I invest
	Given my annual salary
	When I invest a percentage of it
	Then I should be told the dollar amount I am investing

Scenario Outline: Single Investment
	Given my annual salary is <annualSalary>
	When I invest <percentage>% into an investment
	Then I am investing <moniesInvested>
	Examples:
		| annualSalary | percentage | moniesInvested |
		| $106,156     | 6          | $6,369.36      |
		| $106,156     | 5.18       | $5,498.88      |
		| $24,136.25   | 16         | $3,861.80      |

Scenario: Multiple Investments
	Given my annual salary is $106,156
	When I invest
	| percentage | investment  |
	| 6          | 401(k)      |
	| 5.18       | ROTH 401(k) |
	Then I am investing
	| moniesInvested | investment  |
	| $6,369.36      | 401(k)      |
	| $5,,498.88     | ROTH 401(k) |