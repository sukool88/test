using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class OrderManager
    {
        public Dictionary<string, Order> Orders;
        public OrderManager()
        {
            LoadOrders();
        }
        private void LoadOrders()
        {
            JsonSerializer js = JsonSerializer.Create();
            string file = "/coding-assigment-orders[2062].json";
            var reader = new StreamReader(System.Environment.CurrentDirectory + file);
            string json = reader.ReadToEnd();
            Orders = JsonConvert.DeserializeObject<Dictionary<string, Order>>(json);
            //var list = js.Deserialize<Orders>(new JsonTextReader(reader));
        }
    }
}
