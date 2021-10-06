using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace Zork
{
    class Program
    {
        private static Room LocationName
        {
            get
            {
                return Rooms[LocationCoords.Row, LocationCoords.Column];
            }
        }

        private enum CommandLineArguments
        {
            RoomsFilename = 0
        }
        
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Zork!");

            Room previousLocationName = null;
            Commands command = Commands.UNKNOWN;

            string defaultRoomsFilename = "Rooms.json";
            string roomsFilename = (args.Length > 0 ? args[(int)CommandLineArguments.RoomsFilename] : defaultRoomsFilename);
            InitializeRooms(roomsFilename);

            while (command != Commands.QUIT)
            {
                Console.Write($"{LocationName}\n> ");
                if (previousLocationName != LocationName)
                {
                    Console.WriteLine(LocationName.Description);
                    previousLocationName = LocationName;
                }
                command = ToCommand(Console.ReadLine().Trim());

                switch (command)
                {
                    case Commands.QUIT:
                        Console.WriteLine("Thanks for playing!");
                        break;

                    case Commands.LOOK:
                        Console.WriteLine(LocationName.Description);
                        break;

                    case Commands.NORTH:
                    case Commands.SOUTH:
                    case Commands.EAST:
                    case Commands.WEST:
                        if (Move(command) == false)
                        {
                            Console.WriteLine("The way is shut!");
                        }
                        break;

                    default:
                        Console.WriteLine("Unknown command.");
                        break;
                }
            }
        }

        private static (int Row, int Column) LocationCoords = (1,1);

        private static bool Move(Commands command)
        {
            bool didMove = false;

            switch (command)
            {
                case Commands.NORTH when LocationCoords.Row < Rooms.GetLength(0) - 1:

                    ++LocationCoords.Row;
                    didMove = true;

                    break;

                case Commands.SOUTH when LocationCoords.Row > 0:

                    --LocationCoords.Row;
                    didMove = true;

                    break;

                case Commands.EAST when LocationCoords.Column < Rooms.GetLength(1) - 1:

                    ++LocationCoords.Column;
                    didMove = true;

                    break;

                case Commands.WEST when LocationCoords.Column > 0:
                    
                    --LocationCoords.Column;
                    didMove = true;
                    
                    break;
            }

            return didMove;
        }

        private static Commands ToCommand(string commandString) => Enum.TryParse<Commands>(commandString, ignoreCase: true, out Commands result) ? result : Commands.UNKNOWN;

        private static  Room[,] Rooms = {
            {new Room("Rocky Trail"),   new Room("South of House"),  new Room("Canyon View") },
            {new Room("Forest"),        new Room("West of House"),   new Room("Behind House") },
            {new Room("Dense Woods"),   new Room("North of House"),  new Room("Clearing") },
        };

        private enum Fields
        {
            Name = 0,
            Description =  1
        }
        private static void InitializeRooms(string roomsFilename) => Rooms = JsonConvert.DeserializeObject<Room[,]>(File.ReadAllText(roomsFilename));
    }
}
