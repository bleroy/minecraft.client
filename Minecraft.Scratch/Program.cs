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
                var wood = new Wood(Wood.Species.Oak, Orientation.UpDown);
                world.SetBlock(wood, playerPosition + new Vector3(0, 0, 1));
            }
        }
    }
}
