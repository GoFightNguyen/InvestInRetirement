Feature: How much to invest
	Given my annual salary
	When I want to invest a percentage of it into retirement
	Then I should be told the dollar amount to invest

Scenario Outline: Dollar amount to invest
	Given my annual salary is <annualSalary>
	When I want to invest <percentage>% into retirement
	Then I am told to invest <moniesToInvest>
	Examples:
		| annualSalary | percentage | moniesToInvest |
		| $106,156     | 15         | $15,923.40     |
		| $55,800      | 15         | $8,370         |
		| $24,136.25   | 4.3        | $1,037.86      |
		| $55,800      | 7.7        | $4,296.60      |