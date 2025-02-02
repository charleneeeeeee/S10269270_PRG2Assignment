//==========================================================
// Student Number	: S10269270K
// Student Name	: Charlene Soh 
//==========================================================
//////////////////////////////////////////lwttflight/////////////////////////////////////////
namespace FlightInfoSystem
{
    public class LWTTFlight : Flight
    {
        public double RequestFee { get; set; }

        public LWTTFlight(string flightNumber, string origin, string destination, DateTime expectedTime, string status, double requestFee)
            : base(flightNumber, origin, destination, expectedTime, status)
        {
            RequestFee = requestFee;
        }

        public override double CalculateFees()
        {
            return RequestFee * 1.1; // Example fee calculation
        }
    }
}
