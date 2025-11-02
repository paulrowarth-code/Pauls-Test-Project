Feature: Basic UI Test

Scenario: Basic Login
	Given The user logs in
	And The user creates a new employer	
	And the user adds new employees as follows:
		| Title | First Name | Last Name | DOB        | Start Date | NI Number | Salary |
		| Mr    | Test       | Employee  | 1980-04-06 | 2025-04-06 | AB123456C |   2000 |
		| Ms    | Sample     | Worker    | 1990-05-15 | 2025-04-15 | BA789012A |   1000 |
	And The user creates a new pay schedule
	And The user starts the next pay run
