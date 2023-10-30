using Microsoft.AspNetCore.Mvc;
using RadancyTest.Models;

namespace RadancyTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BankAccountController : ControllerBase
    {

        // Dummy data store for the sake of this example.
        private static List<Account> accounts = new List<Account>
            {
                new Account { Id = 1, AccountNumber = "12345678", Balance = 1000 },
                new Account { Id = 2, AccountNumber = "87654321", Balance = 500 }
            };

        [HttpPost("deposit")]
        public ActionResult Deposit([FromBody] TransactionRequest request)
        {
            var account = accounts.FirstOrDefault(a => a.AccountNumber == request.AccountNumber);

            if (account == null)
            {
                return NotFound("Account not found.");
            }
            if (validateTransaction(account, request))
            {

                account.Balance += request.Amount;
                return Ok(account);
            }
            else
            {
                return BadRequest("The provided transaction details are not valid.");
            }
        }

        [HttpPost("withdraw")]
        public ActionResult Withdraw([FromBody] TransactionRequest request)
        {
            var account = accounts.FirstOrDefault(a => a.AccountNumber == request.AccountNumber);
            if (account == null)
            {
                return NotFound("Account not found.");
            }
            if (validateTransaction(account, request))
            {
                if (account.Balance < request.Amount)
                {
                    return BadRequest("Insufficient funds.");
                }
                account.Balance -= request.Amount;
                return Ok(account);
            }
            else {
                return BadRequest("The provided transaction details are not valid.");
            }

        }

        private Boolean validateTransaction(Account account, TransactionRequest request)
        {
            if (request.Amount > 0)
            {
                if (request.Amount > 10000)
                {
                    return false;
                }
            }
            else
            {
                if (account.Balance + request.Amount < 100)
                {
                    return false;
                }
                if (account.Balance * (decimal).9 < request.Amount)
                {
                    return false;
                }
            }
            return true;
        }

    }
}