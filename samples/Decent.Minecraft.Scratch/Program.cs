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
                    await world.PostToChatAsync("Hello from C# and .NET Core!");
                    var player = world.Player;

                    while (true)
                    {
                        Console.WriteLine(@"
Available commands:

P = Get current position
T = Transport to a given position
M = Move towards north/south/east/west
C = Build a castle
I = Render a picture
H = Height under the player
S = Add snow

Press ESC to quit

");

                        Vector3 playerPosition = new Vector3();
                        Direction? direction;

                        var cmd = Console.ReadKey();
                        Console.WriteLine();
                        switch (cmd.Key)
                        {
                            case ConsoleKey.Escape:
                                return;
                            case ConsoleKey.P:
                                playerPosition = await player.GetTilePositionAsync();
                                Console.WriteLine($"Player is on {playerPosition}.");
                                break;
                            case ConsoleKey.T:
                                Console.WriteLine("Where do you want to go? Enter X,Y,Z coordinates and press Enter:");
                                var destCoord = Console.ReadLine().ParseCoordinates();
                                await player.SetPositionAsync(destCoord);
                                playerPosition = await player.GetTilePositionAsync();
                                Console.WriteLine($"Player is now at {playerPosition}");
                                break;
                            case ConsoleKey.M:
                                direction = GetDirection();
                                if (direction.HasValue)
                                {
                                    playerPosition = await player.GetTilePositionAsync();
                                    await player.SetPositionAsync(playerPosition.Towards(direction.Value));
                                    playerPosition = await player.GetTilePositionAsync();
                                    Console.WriteLine($"Player moved {direction} to {playerPosition}");
                                }
                                break;
                            case ConsoleKey.C:
                                direction = GetDirection();
                                if (direction.HasValue)
                                {
                                    playerPosition = await player.GetTilePositionAsync();
                                    new Castle(world, playerPosition.Towards(direction.Value, 30)).Build();
                                }
                                break;
                            case ConsoleKey.I:
                                direction = GetDirection();
                                if (direction.HasValue)
                                {
                                    playerPosition = await player.GetTilePositionAsync();
                                    var imageBuilder = new ImageBuilder(world);
                                    imageBuilder.DrawImage(
                                        Path.Combine(".", "Media", "Minecraft.gif"),
                                        playerPosition.Towards(direction.Value, 20),
                                        maxSize: 100);
                                }
                                break;
                            case ConsoleKey.H:
                                var height = world.GetHeight(playerPosition);
                                Console.WriteLine($"Height under the player is {height}");
                                break;
                            case ConsoleKey.S:
                                playerPosition = await player.GetTilePositionAsync();
                                // Create grass and put a snowy layer on top of it
                                // to create a snowy grass block.
                                var grass = new Grass();
                                world.SetBlock(grass, playerPosition + new Vector3(0, 0, 3));
                                var snowLayer = new Snow();
                                world.SetBlock(snowLayer, playerPosition + new Vector3(0, 1, 3));
                                Console.WriteLine("Snow added");
                                break;
                        }
                    }
                }
            }
            catch (FailedToConnectToMinecraftEngine e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private static Direction? GetDirection()
        {
            ConsoleKeyInfo cmd;
            Console.WriteLine("Which direction? (N,S,E,W)");
            cmd = Console.ReadKey();
            switch (cmd.Key)
            {
                case ConsoleKey.N:
                    return Direction.North;
                case ConsoleKey.S:
                    return Direction.South;
                case ConsoleKey.E:
                    return Direction.East;
                case ConsoleKey.W:
                    return Direction.West;
                default:
                    Console.WriteLine("Unknown direction...");
                    return null;
            }
        }
    }
}
