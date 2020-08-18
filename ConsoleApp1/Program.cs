using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter Flights");
            var flightManager = new FlightManager();
            //flightManager.FlightLoader(); // this is UI interface to add other filghts
            flightManager.LoadDummy();
            flightManager.Display();
            var orderManager = new OrderManager();
            flightManager.AddOrders(orderManager.Orders);
            flightManager.DisplayShipments();
            flightManager.DisplayUnscheduledShipments();
        }
    }
}
