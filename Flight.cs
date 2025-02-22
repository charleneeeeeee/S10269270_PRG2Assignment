//==========================================================
// Student Number	: S10269270K
// Student Name	: Charlene Soh 
//==========================================================
////////////////////////////////flight class/////////////////////////////////////
namespace FlightInfoSystem
{
    public abstract class Flight
    {
        public string FlightNumber { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public DateTime ExpectedTime { get; set; }
        public string Status { get; set; }
        public Airline Airline { get; set; }

        protected Flight(string flightNumber, string origin, string destination, DateTime expectedTime, string status)
        {
            FlightNumber = flightNumber;
            Origin = origin;
            Destination = destination;
            ExpectedTime = expectedTime;
            Status = status;
        }

        public abstract double CalculateFees();

        public override string ToString()
        {
            return $"Flight: {FlightNumber}, Origin: {Origin}, Destination: {Destination}, Status: {Status}";
        }
    }
}
