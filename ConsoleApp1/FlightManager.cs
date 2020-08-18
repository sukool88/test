using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class FlightManager
    {
        List<Flight> Flights;

        public Dictionary<string, Order> UnScheduledOrders = new Dictionary<string, Order>();
        public void FlightLoader()
        {
            bool exit = false;
            Flights = new List<Flight>();
            while (!exit)
            {
                Flight flight = new Flight();
                for (int i = 0; i < 4; i++)
                {
                    switch (i)
                    {
                        case 0:
                            Console.Write("Enter The FlightDateDay:");
                            int day;
                            if (int.TryParse(Console.ReadLine(), out day))
                            {
                                flight.FlightDateDay = day;
                            }
                            break;
                        case 1:
                            Console.Write("Enter The FlightNo:");
                            int flightno;
                            if (int.TryParse(Console.ReadLine(), out flightno))
                            {
                                flight.FlightNo = flightno;
                            }
                            break;
                        case 2:
                            Console.Write("Enter The Arrival:");
                            flight.Arrival = Console.ReadLine();
                            break;
                        case 3:
                            Console.Write("Enter The Departure:");
                            flight.Departure = Console.ReadLine();
                            break;
                    }
                }
                Flights.Add(flight);
                Console.Write("Continue adding?(Y/N):");
                string exitResponse = Console.ReadLine();
                if (exitResponse == "n")
                {
                    exit = true;
                }
            }
        }
        public void Display()
        {
            foreach (var flight in Flights)
            {
                Console.WriteLine(string.Format("Flight: {0}, Departure: {1}, arrival: {2}, day: {3}", flight.FlightNo, flight.Departure, flight.Arrival, flight.FlightDateDay));
            }
            Console.WriteLine();
        }
        public void DisplayShipments()
        {
            foreach (var flight in Flights)
            {
                foreach (var item in flight.Shipments)
                {

                    Console.WriteLine(string.Format("Order:{4},Flight: {0}, Departure: {1}, arrival: {2}, day: {3}", flight.FlightNo, flight.Departure, flight.Arrival, flight.FlightDateDay, item.Key));
                }
            }
            Console.WriteLine();
        }
        public void DisplayUnscheduledShipments()
        {
            foreach (var item in UnScheduledOrders)
            {
                Console.WriteLine(string.Format("order: {0}, flightNumber: not scheduled", item.Key));
            }
        }
        public void AddOrders(Dictionary<string, Order> orders)
        {
            var groupedOrders = orders.GroupBy(x => x.Value.Destination);
            var groupedFlights = Flights.GroupBy(x => x.Departure);

            var ordersWithNoFlghts = groupedOrders.Select(x => x).Where(z => !groupedFlights.Select(y => y.Key.Substring(y.Key.IndexOf('(') + 1, 3)).Contains(z.Key));
            //adds unscheduled orders
            foreach (var items in ordersWithNoFlghts)
            {
                foreach (var item in items)
                {
                    UnScheduledOrders.Add(item.Key, item.Value);
                }
            }

            var joined = groupedFlights.GroupJoin(groupedOrders,
                                                    flight => flight.Key.Substring(flight.Key.IndexOf('(') + 1, 3),
                                                    order => order.Key,
                                                    (flight, _orders) => new { flight.Key, _orders });

            foreach (var flight in Flights.GroupBy(x => x.Departure))
            {

                //gets matching flight key from grouped
                //var matchedKeyFlight = joined.Where(y => y.Key == flight.Key).Select(x => new { x._orders });
                //Dictionary<string,Order> filteredOrders = matchedKeyFlight.Select(x => x._orders.First()).First().Select(x=>new { x.Key, x.Value }).ToDictionary(x=>x.Key,x=>x.Value);

                Dictionary<string, Order> filteredOrders = joined.Where(y => y.Key == flight.Key).Select(x => new { x._orders }).Single()._orders.Single().Select(x => new { x.Key, x.Value }).ToDictionary(x => x.Key, x => x.Value);
                int count = 0;
                foreach (var item in flight)
                {
                    if (count >= 20)
                    {
                        item.Shipments = filteredOrders.Skip(count).Take(20);
                    }
                    else
                    {
                        item.Shipments = filteredOrders.Take(20);
                    }
                    count += 20;
                }
                // add overflow orders to unscheduled
                if (filteredOrders.Skip(count).Any())
                {
                    foreach (var item in filteredOrders.Skip(count))
                    {
                        UnScheduledOrders.Add(item.Key, item.Value);
                    }
                }
            }

        }
        public void LoadDummy()
        {
            Flights = new List<Flight>()
            {
                new Flight()
                {
                     FlightNo = 1,
                     FlightDateDay = 1,
                     Arrival = "Montreal airport (YUL)",
                     Departure = "Toronto (YYZ)"
                },
                new Flight()
                {
                     FlightNo = 2,
                     FlightDateDay = 1,
                     Arrival = "Montreal (YUL)",
                     Departure = "Calgary (YYC)"
                },
                new Flight()
                {
                     FlightNo = 3,
                     FlightDateDay = 1,
                     Arrival = "Montreal (YUL)",
                     Departure = "Vancouver (YVR)"
                },
                new Flight()
                {
                     FlightNo = 4,
                     FlightDateDay = 2,
                     Arrival = "Montreal airport (YUL)",
                     Departure = "Toronto (YYZ)"
                },
                new Flight()
                {
                     FlightNo = 5,
                     FlightDateDay = 2,
                     Arrival = "Montreal (YUL)",
                     Departure = "Calgary (YYC)"
                },
                new Flight()
                {
                     FlightNo = 6,
                     FlightDateDay = 2,
                     Arrival = "Montreal (YUL)",
                     Departure = "Vancouver (YVR)"
                }
            };
        }
    }
}
