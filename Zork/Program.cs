using System;
using System.Collections.Generic;

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

            Room previousLocationName = null;
            Commands command = Commands.UNKNOWN;

            InitializeRoomDescriptions();

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

        private static readonly Room[,] Rooms = {
            {new Room("Rocky Trail"),   new Room("South of House"),  new Room("Canyon View") },
            {new Room("Forest"),        new Room("West of House"),   new Room("Behind House") },
            {new Room("Dense Woods"),   new Room("North of House"),  new Room("Clearing") },
        };

        private static void InitializeRoomDescriptions()
        {
            var roomMap = new Dictionary<string, Room>();
            foreach (Room room in Rooms)
            {
                roomMap[room.Name] = room;
            }

            roomMap["Rocky Trail"].Description = "You are on a rock-strewn trail.";                                                                            
            roomMap["South of House"].Description = "You are facing the south side of a white house. There is no door here, and all the windows are barred.";  
            roomMap["Canyon View"].Description = "You are at the top of the Great Canyon on its south wall.";                                                  
            roomMap["Forest"].Description = "This is a forest, with trees in all directions around you.";                                                 
            roomMap["West of House"].Description = "This is an open field west of a white house, with a boarded front door.";                                  
            roomMap["Behind House"].Description = "You are behind the white house. In one corner of the house there is a small window which is slightly ajar.";
            roomMap["Dense Woods"].Description = "This is a dimly lit forest, with large trees all around. To the east, there appears to be sunlight.";        
            roomMap["North of House"].Description = "You are facing the north side of a white house. There is no door here, and all the windows are barred.";  
            roomMap["Clearing"].Description = "You are in a clearing with a forest surrounding you on the west and south.";                                 
        }
    }
}
