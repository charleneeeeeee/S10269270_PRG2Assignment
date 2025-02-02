//==========================================================
// Student Number	: S10269270K
// Student Name	: Charlene Soh 
//==========================================================
/////////////////////////////airline class///////////////////////////////////

namespace FlightInfoSystem
{
    public class Airline
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public Dictionary<string, Flight> Flights { get; set; }

        public Airline(string name, string code)
        {
            Name = name;
            Code = code;
            Flights = new Dictionary<string, Flight>();
        }

        public bool AddFlight(Flight flight)
        {
            if (!Flights.ContainsKey(flight.FlightNumber))
            {
                Flights.Add(flight.FlightNumber, flight);
                return true;
            }
            return false;
        }

        public bool RemoveFlight(Flight flight)
        {
            return Flights.Remove(flight.FlightNumber);
        }

        public double CalculateFees()
        {
            double totalFees = 0;
            foreach (var flight in Flights.Values)
            {
                totalFees += flight.CalculateFees();
            }
            return totalFees;
        }

        public override string ToString()
        {
            return $"Airline: {Name}, Code: {Code}";
        }
    }
}
