using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BulkAPITest
{
    public class Account
    {   
        public int AccountId { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public double Balance { get; set; }
    }

    public class Accounts
    {
        [JsonProperty("Accounts")]
        public List<Account> accounts { get; set; }
        public Accounts()
        {
            accounts = new List<Account>();
        }
    }
}
