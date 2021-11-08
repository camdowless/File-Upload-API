using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace BulkAPITest
{
    public class DataLoader
    {
        public List<Account> ReadFromJson()
        {
            using (StreamReader r = new StreamReader("Accounts.json"))
            {
                string json = r.ReadToEnd();
                List<Account> items = JsonConvert.DeserializeObject<List<Account>>(json);
                return items;
            }
           
        }

        public void WriteToJson(List<Account> Accounts)
        {
            var json = System.Text.Json.JsonSerializer.Serialize(Accounts);
            File.WriteAllText("Accounts.json", json);
        }

    }
}
