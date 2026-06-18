@regression
Feature: Login feature
In order to perform successful login
As a User
I have to enter correct username and password

@login
Scenario Outline: Successful login with valid credentials
	Given I have navigated to the login page
	When I enter a username "<username>" and password "<password>"
	And I tap Login
	Then I should see home page

	Examples: 
		| username					| password     |
		| standard_user				| secret_sauce |
		| problem_user				| secret_sauce |
		| visual_user				| secret_sauce |
		| performance_glitch_user	| secret_sauce |

@login
@smoke
Scenario Outline: Unsuccessful login with invalid credentials
	Given I have navigated to the login page
	When I enter a username "<username>" and password "<password>"
	And I tap Login
	Then I should see an error message "<error>" indicating invalid credentials

	Examples:
		| username        | password     | error									|
		| locked_out_user | secret_sauce | Sorry, this user has been locked out.	|
		|                 |              | Username is required						|
		| standard_user   |              | Password is required						|
