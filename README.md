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


# Nick's Personal Notes During the test
   - I started by creating the api. I decided I am currently most familiar with C# and dot net right now so thats what i went with.
   - I created the api with a BankAccountController containing 2 methods. 1 being Deposit and the other as Withdraw.
	- I took careful note of the above business requirements as not to miss any.
	- Ended up making a validate transaction Method thatt has a good amount of if statements in it. I am confident with my logic but will have to make sure with my upcoming test cases. The things I wanted to ensure were: the user cant deposit more than 10k in one transaction.. so 9999.99 is ok but 10k is not. The user cant withdraw more than 90% of what their account balance at once. and they cant have less than 100 bucks at any time.
- Total time taken to create and upload this to github - 35 minutes


## part 2 notes

   - I looked through the given document again and noticed something i may have missed last time. It says "A user can deposit and withdraw money from account X" which leads me to believe that any user can add or remove any amount of money (within the given limitations) to anyone elses account.
       - In a real case scenario I would double check with the business person to ensure this is what they meant.
    
- Reworking the Business Logic now. --- The current logic takes a transaction request from anyone and executes it. From my perspective it seems no linking between the user and a particular bank account is made here but since the additional notes mention to not worry about login and logout I will assume this is good enough.

   - Now that im finished:
   - I did not get a test to pass, but i did come very close.
   - I had trouble getting the application to run and make its api calls available before the test runs.
   - My tests consisted of a test for withdraw and a test for deposit.
   - 	As the api returns the user that had the item changed, thats what i looked at to validate. I think with more time i would have also ensured that the underlying data structure behind it was successfully changed.
   - 	I was able to format my tests in gherkin with specflow.
   - 	Future testing would also include: more test values. can the user deposit negitive money?? (based on the business requirements its ok), can money be added to a fake account? Availablity testing? is it working at all times? load testing? how many requests can it handle?


As for setting up the test to run, i think another way i could have got around this is if my testing was completely seperated from my program. I could have ran the program and then the test seperately. I am assuming in a banking scenario, it would be available 24/7
