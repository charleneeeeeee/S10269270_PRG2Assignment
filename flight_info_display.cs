// See https://aka.ms/new-console-template for more information

//==========================================================
// Student Number	: S10269270K
// Student Name	: Charlene Soh 
//==========================================================

//////////////////////////////terminal class///////////////////////////////////
using FlightInfoSystem;
using System;
using System.Collections.Generic;
using System.IO;

namespace FlightInfoSystem
{
    public class Terminal
    {
        public string TerminalName { get; set; }
        public Dictionary<string, Airline> Airlines { get; set; }
        public Dictionary<string, Flight> Flights { get; set; }
        public Dictionary<string, BoardingGate> BoardingGates { get; set; }
        public Dictionary<string, double> GateFees { get; set; }

        public Terminal(string terminalName)
        {
            TerminalName = terminalName;
            Airlines = new Dictionary<string, Airline>();
            Flights = new Dictionary<string, Flight>();
            BoardingGates = new Dictionary<string, BoardingGate>();
            GateFees = new Dictionary<string, double>();
        }

        public bool AddAirline(Airline airline)
        {
            if (!Airlines.ContainsKey(airline.Name))
            {
                Airlines.Add(airline.Name, airline);
                return true;
            }
            return false;
        }

        public bool AddBoardingGate(BoardingGate boardingGate)
        {
            if (!BoardingGates.ContainsKey(boardingGate.GateName))
            {
                BoardingGates.Add(boardingGate.GateName, boardingGate);
                return true;
            }
            return false;
        }

        public Airline GetAirlineFromFlight(Flight flight)
        {
            foreach (var airline in Airlines.Values)
            {
                if (airline.Flights.ContainsKey(flight.FlightNumber))
                {
                    return airline;
                }
            }
            return null;
        }

        public void PrintAirlineFees()
        {
            foreach (var fee in GateFees)
            {
                Console.WriteLine($"Airline: {fee.Key}, Fee: {fee.Value}");
            }
        }

        public override string ToString()
        {
            return $"Terminal: {TerminalName}";
        }
    }
}

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

/////////////////////////////////////////////////////  BASIC FEATURES ////////////////////////////////////////////////////////////


class Program
{
    static Dictionary<string, Airline> airlines = new Dictionary<string, Airline>();
    static Dictionary<string, BoardingGate> boardingGates = new Dictionary<string, BoardingGate>();
    static Dictionary<string, Flight> flights = new Dictionary<string, Flight>();

    static void Main()
    {
        ////////////////////////////////////////// load airlines, boarding gates, flights files /////////////////////////////////////////////
        LoadAirlines("C:\\Users\\sohch\\OneDrive - Ngee Ann Polytechnic\\Desktop\\prg2 assignment\\airlines.csv");
        LoadBoardingGates("C:\\Users\\sohch\\OneDrive - Ngee Ann Polytechnic\\Desktop\\prg2 assignment\\boardinggates.csv");
        LoadFlights("C:\\Users\\sohch\\OneDrive - Ngee Ann Polytechnic\\Desktop\\prg2 assignment\\flights.csv");
        ShowMenu();

        static void LoadAirlines(string filePath)
        {
            if (!File.Exists(filePath)) return;
            string[] lines = File.ReadAllLines(filePath);
            foreach (var line in lines.Skip(1))
            {
                string[] parts = line.Split(',');
                if (parts.Length < 2) continue;
                airlines[parts[1]] = new Airline(parts[0], parts[1]);
            }
            Console.WriteLine("Loading Airlines...");
            Console.WriteLine($"{airlines.Count} Airlines Loaded!");
        }

        static void LoadBoardingGates(string filePath)
        {
            if (!File.Exists(filePath)) return;
            string[] lines = File.ReadAllLines(filePath);
            foreach (var line in lines.Skip(1))
            {
                string[] parts = line.Split(',');
                if (parts.Length < 2) continue;
                boardingGates[parts[0]] = new BoardingGate(parts[0], Convert.ToBoolean(parts[1]), Convert.ToBoolean(parts[2]), Convert.ToBoolean(parts[3]));
            }
            Console.WriteLine("Loading Boarding Gates...");
            Console.WriteLine($"{boardingGates.Count} Boarding Gates Loaded!");
        }


        static void LoadFlights(string filePath)
        {
            if (!File.Exists(filePath)) return;
            string[] lines = File.ReadAllLines(filePath);
            foreach (var line in lines.Skip(1))
            {
                string[] parts = line.Split(',');
                if (parts.Length < 5) continue;

                string specialRequestCode = parts[4];

                if (specialRequestCode == "DDJB")
                {
                    flights[parts[0]] = new DDJBFlight(parts[0], parts[1], parts[2], DateTime.Parse(parts[3]), parts[4], 0.0);
                }
                else if (specialRequestCode == "CCFT")
                {
                    flights[parts[0]] = new CFFTFlight(parts[0], parts[1], parts[2], DateTime.Parse(parts[3]), parts[4], 0.0);
                }
                else if (specialRequestCode == "DDJB")
                {
                    flights[parts[0]] = new LWTTFlight(parts[0], parts[1], parts[2], DateTime.Parse(parts[3]), parts[4], 0.0);
                }
                else
                {
                    flights[parts[0]] = new NORMFlight(parts[0], parts[1], parts[2], DateTime.Parse(parts[3]), parts[4]);
                }

            }
            Console.WriteLine("Loading flights...");
            Console.WriteLine($"{flights.Count} Flights Loaded!");
        }

        static void ShowMenu()
        {
            while (true)
            {
                Console.WriteLine("\n=============================================");
                Console.WriteLine("Welcome to Changi Airport Terminal 5");
                Console.WriteLine("=============================================");
                Console.WriteLine("1. List all flights");
                Console.WriteLine("2. List boarding gates");
                Console.WriteLine("4. Create flight");
                Console.WriteLine("5. Display airline flights");
                Console.WriteLine("10b. Display the total fee per airline for the day");
                Console.WriteLine("0. Exit");
                Console.Write("\nPlease select your option: ");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1": ListAllFlights(); break;
                    case "2": ListBoardingGates(); break;
                    case "4": CreateNewFlight(); break;
                    case "5": DisplayFlightDetails(); break;
                    case "10b": DisplayTotalFeePerAirline(); break;
                    case "0": return;
                    default: Console.WriteLine("Invalid choice, try again."); break;
                }
            }
        }
        ////////////////////////////////////////// 3) List all flights with their basic information /////////////////////////////////////////////
        static void ListAllFlights()
        {
            Console.WriteLine("\n=============================================");
            Console.WriteLine("List of Flights for Changi Airport Terminal 5");
            Console.WriteLine("=============================================");
            Console.WriteLine($"{"Flight Number",-15}{"Airline Name",-25}{"Origin",-25}{"Destination",-25}Expected Departure/Arrival Time");
            foreach (var flight in flights.Values)
            {
                Console.WriteLine($"{flight.FlightNumber,-15}{airlines[flight.FlightNumber.Substring(0, 2)].Name,-25}{flight.Origin,-25}{flight.Destination,-25}{flight.ExpectedTime}");
            }
        }

        /////////////////////////////////////////////////////// 4) List all boarding gates //////////////////////////////////////////////////////
        static void ListBoardingGates()
        {
            Console.WriteLine("\n====================================================");
            Console.WriteLine("List of Boarding Gates for Changi Airport Terminal 5");
            Console.WriteLine("====================================================\n");

            // Print headers with proper spacing
            Console.WriteLine("{0,-10} {1,-10} {2,-10} {3,-10}", "Gate Name", "DDJB", "CFFT", "LWTT");
            Console.WriteLine("----------------------------------------------------");

            foreach (var gate in boardingGates.Values)
            {
                Console.WriteLine("{0,-10} {1,-10} {2,-10} {3,-10}", gate.GateName, gate.SupportsDDJB, gate.SupportsCFFT, gate.SupportsLWTT);
            }
        }

        //////////////////////////////////////////////////////// 6) Create a new flight ////////////////////////////////////////////////////////
        static void CreateNewFlight()
        {
            bool addAnother = true;

            while (addAnother)
            {
                Console.Write("Enter Flight Number: ");
                string flightNumber = Console.ReadLine();
                Console.Write("Enter Origin: ");
                string origin = Console.ReadLine();
                Console.Write("Enter Destination: ");
                string destination = Console.ReadLine();
                Console.Write("Enter Expected Departure Time: ");
                string departureTime = Console.ReadLine();
                Console.Write("Enter Special Request Code (CFFT/DDJB/LWTT/None): ");
                string specialRequestCode = Console.ReadLine();

                Flight flight = new NORMFlight(flightNumber, origin, destination, DateTime.Parse(departureTime), "Scheduled");
                flights[flightNumber] = flight;

                Console.WriteLine($"Flight {flightNumber} has been added!");

                // Ask the user if they want to add another flight
                Console.Write("Would you like to add another flight? (Y/N): ");
                string userResponse = Console.ReadLine().ToUpper();

                // Repeat or stop based on user's choice
                if (userResponse == "N")
                {
                    addAnother = false;
                }
            }
        }


        //////////////////////////////////////////////// 7) Display full flight details from an airline /////////////////////////////////////////////

        static void DisplayFlightDetails()
        {
            Console.WriteLine("=============================================");
            Console.WriteLine("List of Airlines for Changi Airport Terminal 5");
            Console.WriteLine("=============================================");
            Console.WriteLine("Airline Code    Airline Name");
            foreach (var airline in airlines)
            {
                Console.WriteLine($"{airline.Key,-15} {airline.Value.Name}");
            }
            Console.Write("Enter Airline Code: ");
            string airlineCode = Console.ReadLine();
            if (!airlines.ContainsKey(airlineCode))
            {
                Console.WriteLine("Invalid airline code.");
                return;
            }

            Airline selectedAirline = airlines[airlineCode];

            Console.WriteLine("\n=============================================");
            Console.WriteLine($"List of Flights for {selectedAirline.Name}");
            Console.WriteLine("=============================================");
            Console.WriteLine($"{"Flight Number",-15}{"Airline Name",-25}{"Origin",-25}{"Destination",-25}{"Expected Departure/Arrival Time"}");

            var airlineFlights = flights.Values
                .Where(f => f.FlightNumber.StartsWith(airlineCode))
                .OrderBy(f => f.ExpectedTime)
                .ToList();

            if (airlineFlights.Count == 0)
            {
                Console.WriteLine("No flights available for this airline.");
                return;
            }

            foreach (var flight in airlineFlights)
            {
                Console.WriteLine($"{flight.FlightNumber,-15}{selectedAirline.Name,-25}{flight.Origin,-25}{flight.Destination,-25}{flight.ExpectedTime.ToString("dd/M/yyyy h:mm:ss tt")}");
            }


        }

        //////////////////////////////////////////////// Advanced feature (b) /////////////////////////////////////////////
        static void DisplayTotalFeePerAirline()
        {
            Console.WriteLine("\n=============================================");
            Console.WriteLine("Total Fee Per Airline for the Day");
            Console.WriteLine("=============================================");

            // check that all flights have assigned boarding gates
            //var unassignedFlights = flights.Values.Where(f => !boardingGates.Values.Any(g => g.Flight == f)).ToList();
            //if (unassignedFlights.Count > 0)
            //{
            //    Console.WriteLine("Warning: Some flights do not have assigned boarding gates.");
            //    Console.WriteLine("Please assign boarding gates to all flights before running this feature again.");
            //    return;
            //}

            double totalFeesCollected = 0;
            double totalDiscountsApplied = 0;

            // looking through each airline
            foreach (var airline in airlines.Values)
            {
                double airlineSubtotal = 0;
                double airlineDiscounts = 0;
                int flightCount = 0;
                int specialRequestCount = 0;
                int earlyLateFlightCount = 0;
                int eligibleOriginCount = 0;

                var airlineFlights = flights.Values.Where(f => f.FlightNumber.StartsWith(airline.Code)).ToList();

                if (airlineFlights.Count == 0) continue;

                Console.WriteLine($"\n=============================================");
                Console.WriteLine($"Fees Calculation for {airline.Name}");
                Console.WriteLine("=============================================");

                // calc fees for each flight
                foreach (var flight in airlineFlights)
                {
                    double flightFee = 300;
                    flightCount++;

                    if (flight.Destination == "SIN") flightFee += 500;

                    if (flight.Origin == "SIN") flightFee += 800;

                    if (flight is CFFTFlight cfftFlight)
                    {
                        flightFee += 150;
                        specialRequestCount++;
                    }
                    else if (flight is LWTTFlight lwttFlight)
                    {
                        flightFee += 500;
                        specialRequestCount++;
                    }
                    else if (flight is DDJBFlight ddjbFlight)
                    {
                        flightFee += 300;
                        specialRequestCount++;
                    }

                    if (flight.ExpectedTime.Hour < 11 || flight.ExpectedTime.Hour >= 21)
                        earlyLateFlightCount++;

                    if (flight.Origin == "DXB" || flight.Origin == "BKK" || flight.Origin == "NRT")
                        eligibleOriginCount++;

                    airlineSubtotal += flightFee;
                }

                // applying promotional discounts
                airlineDiscounts += (flightCount / 3) * 350;
                airlineDiscounts += earlyLateFlightCount * 110;
                airlineDiscounts += eligibleOriginCount * 25;
                airlineDiscounts += (flightCount - specialRequestCount) * 50;

                if (flightCount > 5)
                    airlineDiscounts += airlineSubtotal * 0.03;

                // display breakdown for each airline
                double finalAirlineFee = airlineSubtotal - airlineDiscounts;
                totalFeesCollected += finalAirlineFee;
                totalDiscountsApplied += airlineDiscounts;

                Console.WriteLine($"Subtotal Fees: ${airlineSubtotal:F2}");
                Console.WriteLine($"Total Discounts: ${airlineDiscounts:F2}");
                Console.WriteLine($"Final Total Fees: ${finalAirlineFee:F2}");
            }

            // final display
            Console.WriteLine("\n=============================================");
            Console.WriteLine("Final Summary for Terminal 5");
            Console.WriteLine("=============================================");
            Console.WriteLine($"Total Fees Collected: ${totalFeesCollected:F2}");
            Console.WriteLine($"Total Discounts Applied: ${totalDiscountsApplied:F2}");

            double discountPercentage = totalDiscountsApplied / totalFeesCollected * 100;
            Console.WriteLine($"Discount Percentage over Final Fees: {discountPercentage:F2}%");
        }




    }
}
