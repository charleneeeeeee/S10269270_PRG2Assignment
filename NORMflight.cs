//==========================================================
// Student Number	: S10269270K
// Student Name	: Charlene Soh 
//==========================================================
//////////////////////////////NORMflight class////////////////////////////////////////
namespace FlightInfoSystem
{
    public class NORMFlight : Flight
    {
        public NORMFlight(string flightNumber, string origin, string destination, DateTime expectedTime, string status)
            : base(flightNumber, origin, destination, expectedTime, status)
        {
        }

        public override double CalculateFees()
        {
            return 100; // Example fixed fee for normal flights
        }
    }
}
