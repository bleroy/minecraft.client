using Decent.Minecraft.Client;
using System;
using System.Threading.Tasks;

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
            using (var connection = new Connection(args[0]))
            {
                connection.Open();
                var world = new World(connection);
                world.PostToChat("Hello from C# and .NET Core!");
                var originBlock = world.GetBlockType(0, 0, 0);
                world.PostToChat($"Origin block is {originBlock}.");
            }
        }
    }
}
