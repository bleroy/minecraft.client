using Decent.Minecraft.BlocksToBombs;
using Decent.Minecraft.Shapes;
using Decent.Minecraft.Client;
using Decent.Minecraft.Client.Blocks;
using Decent.Minecraft.Client.Java;
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

minecraft.client <Minecraft instance ip>");
                return;
            }

            try
            {
                using (var world = JavaWorld.Connect(args[0]))
                {
                    await world.PostToChatAsync("Hello from C# and .NET Core!");
                    var player = world.Player;
                    Bridge bridge = null;

                    DisplayAvailableCommands();

                    while (true)
                    {
                        Vector3 playerPosition = new Vector3();
                        Direction? direction;

                        var cmd = Console.ReadKey(true);
                        Console.WriteLine();
                        var shift = cmd.Modifiers.HasFlag(ConsoleModifiers.Shift);
                        var ctrl = cmd.Modifiers.HasFlag(ConsoleModifiers.Control);
                        var alt = cmd.Modifiers.HasFlag(ConsoleModifiers.Alt);
                        switch (cmd.Key)
                        {
                            case ConsoleKey.Escape:
                                return;
                            case ConsoleKey.P:
                                if (ctrl)
                                {
                                    player.Moved -= _onPlayerMoved;
                                    Console.WriteLine("Stopped detecting player movement.");
                                }
                                else
                                {
                                    player.Moved += _onPlayerMoved;
                                    Console.WriteLine("Started detecting player movement.");
                                }
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
                            case ConsoleKey.D:
                                Console.WriteLine(@"

Please choose what to draw:
    * C is for castle
    * I is for image
    * S is for snowy block
    * B is for Borromean rings

");
                                var whatToDraw = Console.ReadKey(true);
                                switch (whatToDraw.Key)
                                {
                                    case ConsoleKey.C:
                                        direction = GetDirection();
                                        if (direction.HasValue)
                                        {
                                            playerPosition = await player.GetTilePositionAsync();
                                            var castlePosition = playerPosition.Towards(direction.Value, 70);
                                            Console.WriteLine($"Building a castle at {castlePosition}.");
                                            new Castle(world, castlePosition).Build();
                                        }
                                        break;
                                    case ConsoleKey.I:
                                        direction = GetDirection();
                                        if (direction.HasValue)
                                        {
                                            RenderIcon(world, direction);
                                        }
                                        break;
                                    case ConsoleKey.S:
                                        direction = GetDirection();
                                        if (direction.HasValue)
                                        {
                                            Console.WriteLine(@"

It's starting to snow on this particular block...");
                                            Snow(world, direction);
                                        }
                                        break;
                                    case ConsoleKey.B:
                                        playerPosition = await player.GetTilePositionAsync();
                                        new Borromean(world, playerPosition + new Vector3(0, 35, 0)).Build();
                                        break;
                                }
                                break;
                            case ConsoleKey.H:
                                var height = world.GetHeight(playerPosition);
                                Console.WriteLine($"Height under the player is {height}");
                                break;
                            case ConsoleKey.E:
                                if (ctrl)
                                {
                                    world.PostedToChat -= _onChatMessage;
                                    Console.WriteLine("Stopped listening to chat messages.");
                                }
                                else
                                {
                                    world.PostedToChat += _onChatMessage;
                                    Console.WriteLine(@"Started listening to chat messages.

Available commands:

    * `Explode` to explode the tile under the player
    * `Snow` to create a block north of the player that will get snowed on.
    * `Castle` to build a castle north of the player
    * `Image` to render an image north of the player
");
                                }
                                break;
                            case ConsoleKey.X:
                                if (ctrl)
                                {
                                    world.BlockHit -= _onBlockHitExplode;
                                    Console.WriteLine("Stopped exploding blocks when hit.");
                                }
                                else
                                {
                                    world.BlockHit += _onBlockHitExplode;
                                    Console.WriteLine("Started exploding blocks when hit.");
                                }
                                break;
                            case ConsoleKey.B:
                                if (ctrl)
                                {
                                    if (bridge != null)
                                    {
                                        bridge.Dispose();
                                        bridge = null;
                                        Console.WriteLine("Stopped building a bridge under the player.");
                                    }
                                }
                                else if (bridge == null)
                                {
                                    bridge = new Bridge(world, player);
                                    Console.WriteLine("Started building a bridge under the player.");
                                }
                                break;
                            case ConsoleKey.I:
                                if (ctrl)
                                {
                                    world.BlockHit -= _onBlockHitIdentify;
                                    Console.WriteLine("Stopped block identification mode.");
                                }
                                else
                                {
                                    world.BlockHit += _onBlockHitIdentify;
                                    Console.WriteLine("Started blocks identification mode.");
                                }
                                break;
                            default:
                                DisplayAvailableCommands();
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

        private static void DisplayAvailableCommands()
        {
            Console.WriteLine(@"
Available commands:

P = Monitor current position (CTRL+P to cancel).
H = Height under the player.
T = Teleport to a given position.
M = Move towards north/south/east/west.
D = Draw something.
B = Create a bridge under the player as he walks (CTRL+B to stop).
E = Eavesdrop on chat (CTRL+E to cancel) and take commands from there.
X = Explode blocks when hit / right-clicked (CTRL+X to cancel).
I = Identify a block type (CTRL+I to cancel).

Press ESC to quit.

");
        }

        private static async void Snow(IWorld world, Direction? direction)
        {
            // Create grass and put a snowy layer on top of it
            // to create a snowy grass block.
            var playerPosition = await world.Player.GetTilePositionAsync();
            var awayFromPlayer = playerPosition.Towards(direction.Value, 3);
            await world.SetBlockAsync<Grass>(awayFromPlayer);
            for (var thickness = 0; thickness <= 8; thickness++)
            {
                await Task.Delay(1000);
                var snowLayer = new Snow(thickness);
                await world.SetBlockAsync(snowLayer, awayFromPlayer + new Vector3(0, 1, 0));
            }
        }

        private static async void RenderIcon(IWorld world, Direction? direction)
        {
            var playerPosition = await world.Player.GetTilePositionAsync();
            var imageBuilder = new ImageBuilder(world);
            imageBuilder.DrawImage(
                Path.Combine(".", "Media", "Minecraft.gif"),
                playerPosition.Towards(direction.Value, 20),
                maxSize: 100);
        }

        private static EventHandler<MoveEventArgs> _onPlayerMoved = (object sender, MoveEventArgs args) =>
        {
            Console.WriteLine($"Player moved from {args.PreviousPosition} to {args.NewPosition}.");
        };

        private static EventHandler<ChatEventArgs> _onChatMessage = (object sender, ChatEventArgs args) =>
        {
            var message = args.Message;

            var world = (IWorld)sender;
            var position = world.Player.GetTilePosition();
            switch (message.ToLower())
            {
                case "explode":
                    new ExplodingBlock(world, position, 0, 5).Explode();
                    break;
                case "snow":
                    Snow(world, Direction.North);
                    break;
                case "castle":
                    new Castle(world, position.North(60), 31).Build();
                    break;
                case "image":
                    RenderIcon(world, Direction.North);
                    break;
                default:
                    Console.WriteLine($"Somebody said: {message}");
                    break;
            }
        };

        private static EventHandler<BlockEventArgs> _onBlockHitExplode = (object sender, BlockEventArgs args) =>
        {
            new ExplodingBlock((IWorld)sender, args.Position, 5, 10).Explode();
        };

        private static EventHandler<BlockEventArgs> _onBlockHitIdentify = (object sender, BlockEventArgs args) =>
        {
            IBlock block = ((IWorld)sender).GetBlock(args.Position);
            try
            {
                Type t = block.GetType();
                int id = JavaBlockTypes.GetTypeId(t);
                ((IWorld)sender).PostToChatAsync(String.Format("{0} : {1}", t.Name, JavaBlockTypes.GetTypeId(t)));
            }
            catch (Exception)
            {
                ((IWorld)sender).PostToChatAsync("Unknown : Unknown");
            }
        };

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
