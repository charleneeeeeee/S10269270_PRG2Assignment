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
