# SDETCRMMBankingSystemTest
### Notes:
  * We would like you to spend no more than a couple hours of your time on this. 
  * We will focus our review on coding style, organization, testability and test coverage.
  * Don't worry about a real database. Feel free to fake it with in-memory data structures.
  * We would like to see test case creation, test data creation, and see how you trigger the execution of tests with anticipated results.
  * The completed work can be returned to us in a zipped/compressed package that we can extract, build and run; or through a public repository such as GitHub.  
  * Please setup the project so that we can run the application locally in a container via Docker Compose.


### Banking System Test:
  * Assumption: A user has an open account with a Bank. 
  * Objective: Create an API to facilitate banking operations, no need to develop a GUI (no need to browser test, etc). Write test cases for the API.

  * Business Rules:
	- A user can deposit and withdraw money from account X
	- An account cannot have less than $100 at any time in an account
	- A user cannot withdraw more than 90% of their total balance from an account in a single transaction.
	- A user cannot deposit more than $10,000 in a single transaction.

Addl Notes: Do not worry about other banking system functionalities such as login/logout/security etc. for the purpose of this exercise.
