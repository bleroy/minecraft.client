using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading;
using System.Threading.Tasks;

namespace Decent.Minecraft.Client
{
    public class JavaWorld : IWorld
    {
        internal JavaWorld(IConnection connection)
        {
            Connection = connection;
            Player = new JavaPlayer(connection);
        }

        private IConnection Connection { get; }
        public IPlayer Player { get; }

        public static JavaWorld Connect(string address = "localhost", int port = 4711)
        {
            var connection = new JavaConnection(address: address, port: port);
            var world = new JavaWorld(connection);
            connection.Open();
            return world;
        }

        public async Task<BlockType> GetBlockTypeAsync(float x, float y, float z)
        {
            return (BlockType)int.Parse(await Connection.SendAndReceiveAsync(
                "world.getBlock",
                (int)Math.Floor(x), (int)Math.Floor(y), (int)Math.Floor(z)));
        }

        public BlockType GetBlockType(float x, float y, float z)
        {
            return GetBlockTypeAsync(x, y, z).Result;
        }

        public async Task<T> GetBlockAsync<T>(float x, float y, float z) where T : Block
        {
            var response = await Connection.SendAndReceiveAsync(
                "world.getBlockWithData",
                (int)Math.Floor(x), (int)Math.Floor(y), (int)Math.Floor(z));
            return DeserializeBlock(response) as T;
        }

        private static Block DeserializeBlock(string response)
        {
            var splitResponse = response.Split(',');
            return  JavaBlock.Create(
                (BlockType)int.Parse(splitResponse[0]),
                byte.Parse(splitResponse[1]));
        }

        public async Task<T> GetBlockAsync<T>(Vector3 coordinates) where T : Block
        {
            return await GetBlockAsync<T>(coordinates.X, coordinates.Y, coordinates.Z);
        }

        public async Task<Block> GetBlockAsync(float x, float y, float z)
        {
            return await GetBlockAsync<Block>(x, y, z);
        }

        public async Task<Block> GetBlockAsync(Vector3 coordinates)
        {
            return await GetBlockAsync<Block>(coordinates);
        }

        public T GetBlock<T>(float x, float y, float z) where T : Block
        {
            return GetBlockAsync<T>(x, y, z).Result;
        }

        public T GetBlock<T>(Vector3 coordinates) where T : Block
        {
            return GetBlockAsync<T>(coordinates).Result;
        }

        public Block GetBlock(float x, float y, float z)
        {
            return GetBlock<Block>(x, y, z);
        }

        public Block GetBlock(Vector3 coordinates)
        {
            return GetBlock<Block>(coordinates);
        }

        public IWorld SetBlock(Block block, float x, float y, float z)
        {
            SetBlockAsync(block, x, y, z).Wait();
            return this;
        }

        public IWorld SetBlock(Block block, Vector3 coordinates)
        {
            SetBlockAsync(block, coordinates).Wait();
            return this;
        }

        public async Task<IWorld> SetBlockAsync(Block block, float x, float y, float z)
        {
            var javaBlock = JavaBlock.From(block);
            await Connection.SendAsync(
                "world.setBlock",
                (int)Math.Floor(x), (int)Math.Floor(y), (int)Math.Floor(z),
                (byte)javaBlock.Type, javaBlock.Data & 0xF,
                "{}"); // Total hack: pass an empty NBT block to force the java mod to go through a codepath that doesn't have commented out code for data.
            return this;
        }

        public async Task<IWorld> SetBlockAsync(Block block, Vector3 coordinates)
        {
            await SetBlockAsync(block, coordinates.X, coordinates.Y, coordinates.Z);
            return this;
        }

        public async Task<IWorld> PostToChatAsync(string message)
        {
            Console.WriteLine(message);
            await Connection.SendAsync("chat.post", message);
            return this;
        }

        public IWorld PostToChat(string message)
        {
            PostToChatAsync(message).Wait();
            return this;
        }

        public async Task ClearEventsAsync()
        {
            await Connection.SendAsync("events.clear");
        }

        public void ClearEvents()
        {
            ClearEventsAsync().Wait();
        }

        private EventHandler<ChatEventArgs> _postedToChat;
        private CancellationTokenSource _listeningToChatCancellationTokenSource;
        public int ChatPollingInterval { get; set; } = 100;

        public event EventHandler<ChatEventArgs> PostedToChat
        {
            add
            {
                if (_postedToChat == null)
                {
                    _listeningToChatCancellationTokenSource = new CancellationTokenSource();

                    StartListeningToChat(_listeningToChatCancellationTokenSource.Token);
                }
                _postedToChat += value;
            }
            remove
            {
                _postedToChat -= value;
                if (_postedToChat == null)
                {
                    _listeningToChatCancellationTokenSource.Cancel();
                }
            }
        }

        private async void StartListeningToChat(CancellationToken cancellationToken)
        {
            // Get current chats and throw them away to clear the mod-side buffer.
            await Connection.SendAndReceiveAsync("events.chat.posts");
            // Then start polling
            while (!cancellationToken.IsCancellationRequested)
            {
                var chat = await GetChatMessagesAsync();
                if (chat.Count != 0)
                {
                    foreach (var message in chat)
                    {
                        _postedToChat?.Invoke(this, message);
                    }
                }
                await Task.Delay(ChatPollingInterval);
            }
        }

        private async Task<IList<ChatEventArgs>> GetChatMessagesAsync()
        {
            var response = await Connection.SendAndReceiveAsync("events.chat.posts");
            if (string.IsNullOrEmpty(response))
            {
                return new ChatEventArgs[] { };
            }
            return response
                .Split('|')
                .Select(Util.FixPipe)
                .Select(msg =>
                {
                    var comma = msg.IndexOf(',');
                    if (comma == -1) throw new FormatException("Bad message format");
                    return new ChatEventArgs(
                        int.Parse(msg.Substring(0, comma)),
                        msg.Substring(comma + 1));
                })
                .ToList();
        }

        private EventHandler<BlockEventArgs> _blockHit;
        private CancellationTokenSource _blockHitCancellationTokenSource;
        public int BlockHitPollingInterval { get; set; } = 100;

        /// <summary>
        /// An event that triggers when a block is hit by the player.
        /// </summary>
        /// <remarks>
        /// Note that depending on your Minecraft client configuration, block hit events may only trigger on right-clicks and with the sword equipped.
        /// See <see cref="https://github.com/arpruss/raspberryjammod/blob/6ab289c3c3dd774577c695fe1b4e69d82abfd469/src/main/java/mobi/omegacentauri/raspberryjammod/APIHandler.java#L966"/>
        /// for the conditions that trigger the event on the Minecraft mod side.
        /// </remarks>
        public event EventHandler<BlockEventArgs> BlockHit
        {
            add
            {
                if (_blockHit == null)
                {
                    _blockHitCancellationTokenSource = new CancellationTokenSource();

                    StartListeningToBlockHits(_blockHitCancellationTokenSource.Token);
                }
                _blockHit += value;
            }
            remove
            {
                _blockHit -= value;
                if (_blockHit == null)
                {
                    _blockHitCancellationTokenSource.Cancel();
                }
            }
        }

        private async void StartListeningToBlockHits(CancellationToken cancellationToken)
        {
            // Get current block hits and throw them away to clear the mod-side buffer.
            await Connection.SendAndReceiveAsync("events.block.hits");
            // Then start polling
            while (!cancellationToken.IsCancellationRequested)
            {
                var blockHits = await GetBlockHitsAsync();
                if (blockHits.Count != 0)
                {
                    foreach (var blockHit in blockHits)
                    {
                        _blockHit?.Invoke(this, blockHit);
                    }
                }
                await Task.Delay(BlockHitPollingInterval);
            }
        }

        private async Task<IList<BlockEventArgs>> GetBlockHitsAsync()
        {
            var response = await Connection.SendAndReceiveAsync("events.block.hits");
            if (string.IsNullOrEmpty(response))
            {
                return new BlockEventArgs[] { };
            }
            return response
                .Split('|')
                .Select(Util.FixPipe)
                .Select(msg =>
                {
                    var splitMsg = msg.Split(',');
                    if (splitMsg.Length != 5) throw new FormatException("Bad message format");
                    return new BlockEventArgs(
                        int.Parse(splitMsg[4]),
                        new Vector3(int.Parse(splitMsg[0]), int.Parse(splitMsg[1]), int.Parse(splitMsg[2])),
                        (Facing)Enum.Parse(typeof(Facing), splitMsg[3]));
                })
                .Where(hit => hit.Facing != Facing.Nowhere)
                .Distinct()
                .ToList();
        }

        public void Dispose()
        {
            Connection.Close();
        }

        public async Task<int> GetHeightAsync(Vector3 coordinates)
        {
            return await GetHeightAsync(coordinates.X, coordinates.Z);
        }

        public async Task<int> GetHeightAsync(float x, float z)
        {
            var response = await Connection.SendAndReceiveAsync(
                "world.getHeight", (int)Math.Floor(x), (int)Math.Floor(z));
            return int.Parse(response);
        }

        public int GetHeight(Vector3 coordinates)
        {
            return GetHeightAsync(coordinates).Result;
        }

        public int GetHeight(float x, float z)
        {
            return GetHeightAsync(x, z).Result;
        }

        public Block[,,] GetBlocks(Vector3 corner1, Vector3 corner2)
        {
            return GetBlocksAsync(corner1, corner2).Result;
        }

        public async Task<Block[,,]> GetBlocksAsync(Vector3 corner1, Vector3 corner2)
        {
            var x1 = (int)Math.Floor(corner1.X);
            var y1 = (int)Math.Floor(corner1.Y);
            var z1 = (int)Math.Floor(corner1.Z);
            var x2 = (int)Math.Floor(corner2.X);
            var y2 = (int)Math.Floor(corner2.Y);
            var z2 = (int)Math.Floor(corner2.Z);

            var response = await Connection.SendAndReceiveAsync(
                "world.getBlocksWithData", x1, y1, z1, x2, y2, z2);
            var result = new Block[Math.Abs(x1 - x2) + 1, Math.Abs(y1 - y2) + 1, Math.Abs(z1 - z2) + 1];
            var x = 0;
            var y = 0;
            var z = 0;
            foreach(var block in response.Split('|').Select(DeserializeBlock))
            {
                z++;
                if (z > result.GetLength(2))
                {
                    z = 0;
                    x++;
                    if (x > result.GetLength(0))
                    {
                        x = 0;
                        y++;
                    }
                }
                result[x, y, z] = block;
            }
            return result;
        }

        public IWorld SetBlocks(Block block, Vector3 corner1, Vector3 corner2)
        {
            SetBlocksAsync(block, corner1, corner2).Wait();
            return this;
        }

        public async Task<IWorld> SetBlocksAsync(Block block, Vector3 corner1, Vector3 corner2)
        {
            var javaBlock = JavaBlock.From(block);
            await Connection.SendAsync(
                "world.setBlocks",
                (int)Math.Floor(corner1.X), (int)Math.Floor(corner1.Y), (int)Math.Floor(corner1.Z),
                (int)Math.Floor(corner2.X), (int)Math.Floor(corner2.Y), (int)Math.Floor(corner2.Z),
                (byte)javaBlock.Type, javaBlock.Data & 0xF);
            return this;
        }
    }
}
