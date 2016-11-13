using Decent.Minecraft.Client;
using Decent.Minecraft.Client.Blocks;
using System;
using System.Numerics;

namespace Minecraft.Scratch
{
    public class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine(@"Minecraft.Scratch - a console app using Minecraft.Client

Modify the source if you want to make it do anything useful.

usage:

minecraft.client <raspberry pi ip>");
                return;
            }
            using (var world = JavaWorld.Connect(args[0]))
            {
                world.PostToChat("Hello from C# and .NET Core!");
                while (true)
                {
                    Console.WriteLine(@"
Please pick an option:

[1] Build a castle
[2] Surround with chests
[3] Create some snow

[Q] Quit
");

                    var command = Console.ReadKey().KeyChar.ToString().ToUpperInvariant();
                    Console.WriteLine();

                    var playerPosition = world.Player.GetPosition();
                    world.PostToChat($"Player is at {playerPosition}");
                    var blockUnderPlayer = world.GetBlock(playerPosition - new Vector3(0, 1, 0));
                    world.PostToChat($"Block under player is {blockUnderPlayer.Type}.");
                    var height = world.GetHeight(playerPosition);
                    world.PostToChat($"The height of the world under the player is {height}.");

                    switch (command)
                    {
                        case "Q":
                            return;
                        case "1":
                            new Castle(world, playerPosition, 21).Build();
                            break;
                        case "2":
                            var chest = new Chest(Direction.East);
                            world.SetBlock(chest, playerPosition + new Vector3(-1, 0, 0));
                            chest = new Chest(Direction.North);
                            world.SetBlock(chest, playerPosition + new Vector3(0, 0, 1));
                            chest = new Chest(Direction.South);
                            world.SetBlock(chest, playerPosition + new Vector3(0, 0, -1));
                            chest = new Chest(Direction.West);
                            world.SetBlock(chest, playerPosition + new Vector3(1, 0, 0));
                            break;
                        case "3":
                            // Create grass and put a snowy layer on top of it
                            // to create a snowy grass block.
                            var grass = new Grass();
                            world.SetBlock(grass, playerPosition + new Vector3(0, 0, 3));
                            var snowLayer = new SnowLayer();
                            world.SetBlock(snowLayer, playerPosition + new Vector3(0, 1, 3));
                            break;
                    }
                }
            }
        }
    }
}
