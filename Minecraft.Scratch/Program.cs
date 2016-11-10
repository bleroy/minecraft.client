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
                var playerPosition = world.Player.GetPosition();
                world.PostToChat($"Player is at {playerPosition}");
                var blockUnderPlayer = world.GetBlock(playerPosition - new Vector3(0, 1, 0));
                world.PostToChat($"Block under player is {blockUnderPlayer.Type}.");
                var chest = new Chest(Direction.East);
                world.SetBlock(chest, playerPosition + new Vector3(0, 0, 1));
                chest = new Chest(Direction.North);
                world.SetBlock(chest, playerPosition + new Vector3(3, 0, 1));
                chest = new Chest(Direction.South);
                world.SetBlock(chest, playerPosition + new Vector3(5, 0, 1));
                chest = new Chest(Direction.West);
                world.SetBlock(chest, playerPosition + new Vector3(7, 0, 1));
                var height = world.GetHeight(playerPosition);
                world.PostToChat($"The height of the world under the player is {height}.");
            }
        }
    }
}
