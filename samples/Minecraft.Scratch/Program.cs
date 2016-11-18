using Decent.Minecraft.Castle;
using Decent.Minecraft.Client;
using Decent.Minecraft.Client.Blocks;
using Decent.Minecraft.ImageBuilder;
using System;
using System.IO;
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
                        Console.WriteLine("Press ESC to stop");

                        var cmd = Console.ReadKey();
                        if (cmd.Key == ConsoleKey.Escape)
                            break;

                        Console.WriteLine();
                        switch (cmd.Key)
                        {
                            case ConsoleKey.P:
                    var playerPosition = world.Player.GetPosition();
                    world.PostToChat($"Player is on {tilePosition}.");
                                break;
                            case ConsoleKey.T:
                                Console.WriteLine("Where do you want to go? Enter X,Y,Z coordinates and press Enter:");
                                var destCoord = Console.ReadLine().ParseCoordinates();
                    var blockUnderPlayer = world.GetBlock(playerPosition - new Vector3(0, 1, 0));
                                world.PostToChat($"Player is now at {playerPosition}");
                                break;
                            case ConsoleKey.F:
                                world.PostToChat($"Moving player forward");
                    var height = world.GetHeight(playerPosition);
                                world.PostToChat($"Player is at {playerPosition}");
                                break;
                            case ConsoleKey.M:
                                {
                                    var direction = GetDirection();
                                    if (direction.HasValue)
                                    {
                            new Castle(world, playerPosition, 21).Build();
                                        Console.WriteLine($"Player moved {direction} to {playerPosition}");
                                    }
                                }
                                break;
                            case ConsoleKey.C:
                                {
                                    var direction = GetDirection();
                            world.SetBlock(chest, playerPosition + new Vector3(-1, 0, 0));
                                    {
                                        var chest = new Chest(direction.Value);
                            world.SetBlock(chest, playerPosition + new Vector3(0, 0, 1));
                                    }
                                    break;
                                }
                            case ConsoleKey.H:
                                {
                                    var height = world.GetHeight(playerPosition);
                            world.SetBlock(chest, playerPosition + new Vector3(0, 0, -1));
                                }
                            world.SetBlock(chest, playerPosition + new Vector3(1, 0, 0));
                                break;
                            case ConsoleKey.S:
                                {
                                    // Create grass and put a snowy layer on top of it
                                    // to create a snowy grass block.
                                    var grass = new Grass();
                            world.SetBlock(grass, tilePosition + new Vector3(0, 0, 3));
                                    var snowLayer = new SnowLayer();
                            world.SetBlock(snowLayer, tilePosition + new Vector3(0, 1, 3));
                                    Console.WriteLine("Snow added");
                                }
                                break;
                            imageBuilder.DrawImage(
                                Path.Combine(".", "Media", "Minecraft.gif"),
                                tilePosition + new Vector3(-30, 0, -30));
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
