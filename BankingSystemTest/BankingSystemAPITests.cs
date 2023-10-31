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
        private HttpClient _client;
        private string currentAccountNumber;
        private static Mock<IWebHostEnvironment> _mockEnvironment;
        private static ServiceCollection _serviceCollection;
        private static ApplicationRunner _testingApplication;
        private ApplicationRunner myApp;


        [BeforeFeature]
        public static void Setup()
        {
            _serviceCollection = new ServiceCollection();

            // Create mock of IWebHostEnvironment
            _mockEnvironment = new Mock<IWebHostEnvironment>();
            _mockEnvironment.Setup(m => m.ContentRootPath).Returns("C:\\Development\\visualStudioProjects\\RadancyTest\\RadancyTest");

            // Create startup instance
            ApplicationRunner myApp = new ApplicationRunner();
        }




        [Given("I have an account number '(.*)'")]
        public void GivenIHaveAnAccountNumber(string accountNumber)
        {
            _client = myApp.client;
            

            currentAccountNumber = accountNumber;
        }







        [When("I deposit the amount (.*) into that account")]
        public async Task WhenIDepositTheAmount(string amount)
        {
            Decimal decimalAmount = Decimal.Parse(amount);

            _transactionRequest = new TransactionRequest { AccountNumber = currentAccountNumber, Amount = decimalAmount };

            var content = new StringContent(
                JsonConvert.SerializeObject(_transactionRequest),
                Encoding.UTF8,
                "application/json");

            var response = await _client.PostAsync("/BankAccount/deposit", content);
            var responseContent = await response.Content.ReadAsStringAsync();
            _returnedAccount = JsonConvert.DeserializeObject<Account>(responseContent);
        }

        [When("I withdraw the amount (.*) into that account")]
        public async Task WhenIWithdrawTheAmount(string amount)
        {
            Decimal decimalAmount = Decimal.Parse(amount);

            _transactionRequest = new TransactionRequest { AccountNumber = currentAccountNumber, Amount = decimalAmount };

            var content = new StringContent(
                JsonConvert.SerializeObject(_transactionRequest),
                Encoding.UTF8,
                "application/json");

            var response = await _client.PostAsync("/BankAccount/withdraw", content);
            var responseContent = await response.Content.ReadAsStringAsync();
            _returnedAccount = JsonConvert.DeserializeObject<Account>(responseContent);
        }














        //[When("I deposit the amount (.*) into that account")]
        //public void WhenIDepositTheAmount(string amount)
        //{
        //    Decimal decimalAmount = Decimal.Parse(amount);


        //    _transactionRequest = new TransactionRequest { AccountNumber = currentAccountNumber, Amount = decimalAmount };

        //    var request = new RestRequest("/BankAccount/deposit", Method.Post);
        //    request.AddJsonBody(_transactionRequest);
        //    var response = _client.Execute<Account>(request);
        //    _returnedAccount = response.Data;
        //}

        //[When("I withdraw the amount (.*) into that account")]
        //public void WhenIWithdrawTheAmount()
        //{
        //    var request = new RestRequest("/BankAccount/withdraw", Method.Post);
        //    request.AddJsonBody(_transactionRequest);
        //    var response = _client.Execute<Account>(request);
        //    _returnedAccount = response.Data;
        //}

        [Then("that accounts new balance should be (.*)")]
        public void ThenMyNewBalanceShouldBe(decimal expectedBalance)
        {
            Assert.AreEqual(expectedBalance, _returnedAccount.Balance);
        }
    }
}