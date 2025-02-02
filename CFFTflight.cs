//==========================================================
// Student Number	: S10269270K
// Student Name	: Charlene Soh 
//==========================================================
/////////////////////////////////cfftflight class/////////////////////////////////
namespace FlightInfoSystem
{
    public class CFFTFlight : Flight
    {
        public double RequestFee { get; set; }

        public CFFTFlight(string flightNumber, string origin, string destination, DateTime expectedTime, string status, double requestFee)
            : base(flightNumber, origin, destination, expectedTime, status)
        {
            RequestFee = requestFee;
        }

        public override double CalculateFees()
        {
            return RequestFee;
        }
    }
}
