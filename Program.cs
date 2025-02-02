//==========================================================
// Student Number	: S10269270K
// Student Name	: Charlene Soh 
//==========================================================
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
