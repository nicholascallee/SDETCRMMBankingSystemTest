using Newtonsoft.Json;
using System.Text;
using TechTalk.SpecFlow;
using RadancyTest.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using RestSharp;
namespace BankingSystemTest
{ 

    [Binding]
    public class BankAccountSteps
    {
        private Account _returnedAccount;
        private TransactionRequest _transactionRequest;
        private RestClient _client;
        private string currentAccountNumber;
        private static Mock<IWebHostEnvironment> _mockEnvironment;
        private static ServiceCollection _serviceCollection;
        private static ApplicationRunner _testingApplication;

        [SetUp]


        [BeforeFeature]
        public static void Setup()
        {
            _serviceCollection = new ServiceCollection();

            // Create mock of IWebHostEnvironment
            _mockEnvironment = new Mock<IWebHostEnvironment>();
            _mockEnvironment.Setup(m => m.ContentRootPath).Returns("C:\\Development\\visualStudioProjects\\RadancyTest\\RadancyTest");

            // Create startup instance
            _testingApplication = new ApplicationRunner(_mockEnvironment.Object);
            _serviceCollection = (ServiceCollection)_testingApplication.ConfigureServices(_serviceCollection);

        }




        [Given("I have an account number '(.*)'")]
        public void GivenIHaveAnAccountNumber(string accountNumber)
        {
            _client = new RestClient("http://localhost:7275");
            currentAccountNumber = accountNumber;
        }

        [When("I deposit the amount (.*) into that account")]
        public void WhenIDepositTheAmount(string amount)
        {
            Decimal decimalAmount = Decimal.Parse(amount);


            _transactionRequest = new TransactionRequest { AccountNumber = currentAccountNumber, Amount = decimalAmount };

            var request = new RestRequest("/BankAccount/deposit", Method.Post);
            request.AddJsonBody(_transactionRequest);
            var response = _client.Execute<Account>(request);
            _returnedAccount = response.Data;
        }

        [When("I withdraw the amount (.*) into that account")]
        public void WhenIWithdrawTheAmount()
        {
            var request = new RestRequest("/BankAccount/withdraw", Method.Post);
            request.AddJsonBody(_transactionRequest);
            var response = _client.Execute<Account>(request);
            _returnedAccount = response.Data;
        }

        [Then("that accounts new balance should be (.*)")]
        public void ThenMyNewBalanceShouldBe(decimal expectedBalance)
        {
            Assert.AreEqual(expectedBalance, _returnedAccount.Balance);
        }
    }




















    [Binding]
    public class BankingSystemAPITests
    {
        private readonly HttpClient _client;
        private string _accountNumber;
        private Account _returnedAccount;
        private static Mock<IWebHostEnvironment> _mockEnvironment;
        private static ServiceCollection _serviceCollection;
        private static ApplicationRunner _testingApplication;


        [BeforeFeature]
        public static void Setup()
        {
            _serviceCollection = new ServiceCollection();

            // Create mock of IWebHostEnvironment
            _mockEnvironment = new Mock<IWebHostEnvironment>();
            _mockEnvironment.Setup(m => m.ContentRootPath).Returns("C:\\Development\\visualStudioProjects\\RadancyTest\\RadancyTest");

            // Create startup instance
            _testingApplication = new ApplicationRunner(_mockEnvironment.Object);
            _serviceCollection = (ServiceCollection)_testingApplication.ConfigureServices(_serviceCollection);


        }


        [Given(@"I have a bank account number ""(.*)""")]
        public void GivenIHaveABankAccountNumber(string accountNumber)
        {
            _accountNumber = accountNumber;
        }

        [When(@"I deposit an amount of (.*)")]
        public async Task WhenIDepositAnAmountOf(decimal amount)
        {
            var request = new TransactionRequest
            {
                AccountNumber = _accountNumber,
                Amount = amount
            };

            var content = new StringContent(
                JsonConvert.SerializeObject(request),
                Encoding.UTF8,
                "application/json");

            var response = await _client.PostAsync("/BankAccount/deposit", content);
            _returnedAccount = JsonConvert.DeserializeObject<Account>(
                await response.Content.ReadAsStringAsync());
        }

        [When(@"I withdraw an amount of (.*)")]
        public async Task WhenIWithdrawAnAmountOf(decimal amount)
        {
            // Similar to deposit step, but for the withdraw endpoint
        }

        [Then(@"the balance of the account should be (.*)")]
        public void ThenTheBalanceOfTheAccountShouldBe(decimal expectedBalance)
        {
            Assert.AreEqual(expectedBalance, _returnedAccount.Balance);
        }
    }

}