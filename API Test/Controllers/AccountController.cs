using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BulkAPITest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly ILogger<AccountController> _logger;

        public AccountController(ILogger<AccountController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public List<Account> Get()
        {
            DataLoader dl = new DataLoader();
            return dl.ReadFromJson();
        }
        [Route("{accountid:int}")]
        [HttpGet]
        public Account Get([FromRoute] int accountid)
        {
            DataLoader dl = new DataLoader();
            foreach (Account a in dl.ReadFromJson())
            {
                if (a.AccountId == accountid)
                {
                    return a;
                }
            }
            return new Account();
        }

        [Route("BulkUpdate")]
        [HttpPut]
        public IActionResult BulkUpdate([FromBody] List<Account> body)
        {
            DataLoader dl = new DataLoader();
            List<Account> Accounts = dl.ReadFromJson();
            List<Account> NewAccounts = new List<Account>();
            try
            {
                foreach (Account a in body)
                {
                    try
                    {
                        Account c = Accounts.Find(acc => acc.AccountId == a.AccountId);
                        c = a;
                        NewAccounts.Add(c);
                    }
                    catch (ArgumentNullException)
                    {
                        NewAccounts.Add(a);
                    }
                }
                dl.WriteToJson(NewAccounts);
                return Ok();
            } catch (Exception e) 
            {
                return BadRequest(new { message = "Error" });
            }
        }
    }
}
