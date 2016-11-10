using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;

namespace Decent.Minecraft.Client
{
    public class JavaWorld : IWorld
    {
        internal JavaWorld(IConnection connection)
        {
            Connection = connection;
            Player = new Entity(EntityType.ThePlayer, connection, "player");
        }

        private IConnection Connection { get; }
        public Entity Player { get; }

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
            var splitResponse = response.Split(',');
            var block = JavaBlock.Create(
                (BlockType)int.Parse(splitResponse[0]),
                byte.Parse(splitResponse[1])) as T;
            return block;
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
                (byte)javaBlock.Type, javaBlock.Data & 0xF);
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

        public async Task<IEnumerable<ChatMessage>> WaitForChatMessagesAsync()
        {
            var response = await Connection.SendAndReceiveAsync("events.chat.posts");
            return response
                .Split('|')
                .Select(Util.FixPipe)
                .Select(msg =>
                {
                    var comma = msg.IndexOf(',');
                    if (comma == -1) throw new FormatException("Bad message format");
                    return new ChatMessage(
                        int.Parse(msg.Substring(0, comma)),
                        msg.Substring(comma + 1));
                });
        }

        public IEnumerable<ChatMessage> WaitForChatMessages()
        {
            return WaitForChatMessagesAsync().Result;
        }

        public void Dispose()
        {
            Connection.Close();
        }
    }
}
