using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Flight
    {
        public int FlightNo { get; set; }
        public int FlightDateDay { get; set; }
        public string Departure { get; set; }
        public string Arrival { get; set; }
        public IEnumerable<KeyValuePair<string, Order>> Shipments { get; set; }
    }
}
