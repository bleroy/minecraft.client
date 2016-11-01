using Decent.Minecraft.Client;
using System;

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
                var originBlock = world.GetBlockType(0, 0, 0);
                world.PostToChat($"Origin block is {originBlock}.");
                var playerPosition = world.Player.GetPosition();
                world.PostToChat($"Player is at {playerPosition}");
                var blockUnderPlayer = world.GetBlock(playerPosition.X, playerPosition.Y - 1, playerPosition.Z);
            }
        }
    }
}
