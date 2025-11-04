Feature: UI Tests

Background: 
	Given The user logs in
	And The user creates a new employer	

Scenario: Basic UI Test	
	And The user adds new employees as follows:
		| Title | First Name | Last Name | DOB        | Start Date | NI Number | Salary |
		| Mr    | Test       | Employee  | 1980-04-06 | 2025-04-06 | AB123456C |   2000 |
		| Ms    | Sample     | Worker    | 1990-05-15 | 2025-04-15 | BA789012A |   1000 |
	And The user creates a new pay schedule
	And The user starts the next pay run
	Then The payrun should contain the following:
		| Employee         | Monthly Pay | PAYE Tax    | National Insurance Contribution | Take Home Pay |
		| Mr Test Employee |    1,636.36 |      117.60 |                           47.07 |     £1,471.69 |
		| Ms Sample Worker |      545.45 |           - |                               - |       £545.45 |

Scenario: Importing CSV Files
	And The user imports the "5employees.csv" file	
	And The user creates a new pay schedule
	And The user starts the next pay run
	Then the payrun data should match the "PayRunData.csv" file

