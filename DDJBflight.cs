//==========================================================
// Student Number	: S10269270K
// Student Name	: Charlene Soh 
//==========================================================
////////////////////////////////////////ddjb flight/////////////////////////////////////////////
namespace FlightInfoSystem
{
    public class DDJBFlight : Flight
    {
        public double RequestFee { get; set; }

        public DDJBFlight(string flightNumber, string origin, string destination, DateTime expectedTime, string status, double requestFee)
            : base(flightNumber, origin, destination, expectedTime, status)
        {
            RequestFee = requestFee;
        }

        public override double CalculateFees()
        {
            return RequestFee * 1.2; // Example fee calculation
        }
    }
}
