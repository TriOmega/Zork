using System;

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
        
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Zork!");

            Commands command = Commands.UNKNOWN;

            InitializeRoomDescription();

            while (command != Commands.QUIT)
            {
                Console.Write($"{LocationName}\n> ");
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

        private static readonly Room[,] Rooms = {
            {new Room("Rocky Trail"),   new Room("South of House"),  new Room("Canyon View") },
            {new Room("Forest"),        new Room("West of House"),   new Room("Behind House") },
            {new Room("Dense Woods"),   new Room("North of House"),  new Room("Clearing") },
        };

        private static void InitializeRoomDescription()
        {
            Rooms[0, 0].Description = "You are on a rock-strewn trail.";                                                                            // Rocky Trail
            Rooms[0, 1].Description = "You are facing the south side of a white house. There is no door here, and all the windows are barred.";     // South of House
            Rooms[0, 2].Description = "You are at the top of the Great Canyon on its south wall.";                                                  // Canyon View

            Rooms[1, 0].Description = "This is a forest, with trees in all directions around you.";                                                 // Forest
            Rooms[1, 1].Description = "This is an open field west of a white house, with a boarded front door.";                                    // West of House
            Rooms[1, 2].Description = "You are behind the white house. In one corner of the house there is a small window which is slightly ajar."; // Behind House

            Rooms[2, 0].Description = "This is a dimly lit forest, with large trees all around. To the east, there appears to be sunlight.";        // Dense Woods
            Rooms[2, 1].Description = "You are facing the north side of a white house. There is no door here, and all the windows are barred.";     // North of House
            Rooms[2, 2].Description = "You are in a clearing with a forest surrounding you on the west and south.";                                 // Clearing
        }
    }
}
