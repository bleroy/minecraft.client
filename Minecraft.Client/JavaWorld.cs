using System;
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
    }
}
