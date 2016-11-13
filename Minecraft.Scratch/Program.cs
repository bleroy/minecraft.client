using Decent.Minecraft.Client;
using Decent.Minecraft.Client.Blocks;
using System;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;

namespace Minecraft.Scratch
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                MainAsync(args).Wait();
            }
            catch (AggregateException ae)
            {
                Console.WriteLine("Oooops! Something went exceptionally bad...");
                Console.WriteLine(ae.InnerExceptions?.FirstOrDefault()?.Message);
            }
        }

        public static async Task MainAsync(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine(@"Minecraft.Scratch - a console app using Minecraft.Client

Modify the source if you want to make it do anything useful.

usage:

minecraft.client <raspberry pi ip>");
                return;
            }

            try
            {
                using (var world = JavaWorld.Connect(args[0]))
                {
                    var player = world.Player;

                    world.PostToChat("Hello from C# and .NET Core!");
                    world.Player.SetPosition(new Vector3(0, 0, 0));
                    var playerPosition = await player.GetPositionAsync();
                    world.PostToChat($"Player is at {playerPosition}");
                    var blockUnderPlayer = await world.GetBlockAsync(playerPosition.Downwards());
                    world.PostToChat($"Block under player is {blockUnderPlayer.Type}.");

                    while (true)
                    {
                        Console.WriteLine("");
                        Console.WriteLine("Available commands:");
                        Console.WriteLine("P = Get current position");
                        Console.WriteLine("T = Transport to a given position");
                        Console.WriteLine("M = Move towards north/south/east/west");
                        Console.WriteLine("C = Place chest towards north/south/east/west");
                        Console.WriteLine("H = Height under the player");
                        Console.WriteLine("S = Add snow");
                        //Console.WriteLine("F = Move forward");
                        Console.WriteLine("Press ESC to stop");

                        var cmd = Console.ReadKey();
                        if (cmd.Key == ConsoleKey.Escape)
                            break;

                        Console.WriteLine();
                        switch (cmd.Key)
                        {
                            case ConsoleKey.P:
                                playerPosition = await player.GetPositionAsync();
                                world.PostToChat($"Player is at {playerPosition}");
                                break;
                            case ConsoleKey.T:
                                Console.WriteLine("Where do you want to go? Enter X,Y,Z coordinates and press Enter:");
                                var destCoord = Console.ReadLine().ParseCoordinates();
                                playerPosition = await player.SetPositionAsync(destCoord);
                                world.PostToChat($"Player is now at {playerPosition}");
                                break;
                            case ConsoleKey.F:
                                world.PostToChat($"Moving player forward");
                                playerPosition = player.MoveForward();
                                world.PostToChat($"Player is at {playerPosition}");
                                break;
                            case ConsoleKey.M:
                                {
                                    var direction = GetDirection();
                                    if (direction.HasValue)
                                    {
                                        playerPosition = await player.MoveAsync(direction.Value);
                                        Console.WriteLine($"Player moved {direction} to {playerPosition}");
                                    }
                                }
                                break;
                            case ConsoleKey.C:
                                {
                                    var direction = GetDirection();
                                    if (direction.HasValue)
                                    {
                                        var chest = new Chest(direction.Value);
                                        world.SetBlock(chest, playerPosition.Towards(direction.Value));
                                    }
                                    break;
                                }
                            case ConsoleKey.H:
                                {
                                    var height = world.GetHeight(playerPosition);
                                    world.PostToChat($"The height of the world under the player is {height}.");
                                }
                                break;
                            case ConsoleKey.S:
                                {
                                    // Create grass and put a snowy layer on top of it
                                    // to create a snowy grass block.
                                    var grass = new Grass();
                                    world.SetBlock(grass, playerPosition + new Vector3(0, 0, 3));
                                    var snowLayer = new SnowLayer();
                                    world.SetBlock(snowLayer, playerPosition + new Vector3(0, 1, 3));
                                    Console.WriteLine("Snow added");
                                }
                                break;
                            default:
                                Console.WriteLine("Unknown command");
                                break;
                        }
                    }
                }
            }
            catch (FailedToConnectToMineCraftEngine e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private static Direction? GetDirection()
        {
            ConsoleKeyInfo cmd;
            Console.WriteLine("Which direction? (N,S,E,W)");
            cmd = Console.ReadKey();
            Direction? direction = null;
            switch (cmd.Key)
            {
                case ConsoleKey.N:
                    direction = Direction.North;
                    break;
                case ConsoleKey.S:
                    direction = Direction.South;
                    break;
                case ConsoleKey.E:
                    direction = Direction.East;
                    break;
                case ConsoleKey.W:
                    direction = Direction.West;
                    break;
                default:
                    Console.WriteLine("Unknown direction...");
                    break;
            }
            return direction;
        }
    }
}
