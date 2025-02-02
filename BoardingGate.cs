//==========================================================
// Student Number	: S10269270K
// Student Name	: Charlene Soh 
//==========================================================
/////////////////////////////////boarding gate class///////////////////////////////////
namespace FlightInfoSystem
{
    public class BoardingGate
    {
        public string GateName { get; set; }
        public bool SupportsCFFT { get; set; }
        public bool SupportsDDJB { get; set; }
        public bool SupportsLWTT { get; set; }
        public Flight Flight { get; set; }

        public BoardingGate(string gateName, bool supportsCFFT, bool supportsDDJB, bool supportsLWTT)
        {
            GateName = gateName;
            SupportsCFFT = supportsCFFT;
            SupportsDDJB = supportsDDJB;
            SupportsLWTT = supportsLWTT;
        }

        public double CalculateFees()
        {
            return Flight != null ? Flight.CalculateFees() : 0;
        }

        public override string ToString()
        {
            return $"Gate: {GateName}, SupportsCFFT: {SupportsCFFT}, SupportsDDJB: {SupportsDDJB}, SupportsLWTT: {SupportsLWTT}";
        }
    }
}
