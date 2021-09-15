using System;

namespace Zork
{
    class Program
    {
        private static string LocationName
        {
            get
            {
                return Rooms[LocationCoords.Row, LocationCoords.Column];
            }
        }
        
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Zork!");

            Commands command = Commands.UNKNOWN;
            while (command != Commands.QUIT)
            {
                Console.Write($"{LocationName}\n> ");
                command = ToCommand(Console.ReadLine().Trim());

                string outputString;
                switch (command)
                {
                    case Commands.QUIT:
                        outputString = "Thanks for playing!";
                        break;

                    case Commands.LOOK:
                        outputString = "This is an open field west of a white house, with a boarded front door.\nA rubber mat saying 'Welcome to Zork!' lies by the door.";
                        break;

                    case Commands.NORTH:
                    case Commands.SOUTH:
                    case Commands.EAST:
                    case Commands.WEST:
                        outputString = Move(command) ? $"You moved {command}." : "The way is shut!";
                        break;

                    default:
                        outputString = "Unknown command.";
                        break;
                }

                Console.WriteLine(outputString);
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

        private static readonly string[,] Rooms = {
            { "Rocky Trail", "South of House", "Canyon View" },
            { "Forest", "West of House", "Behind House" },
            { "Dense Woods", "North of House", "Clearing" },
        };
    }
}
