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
            var block = JavaBlockSerializer.Create(
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

        public async Task PostToChatAsync(string message)
        {
            await Connection.SendAsync("chat.post", message);
        }

        public void PostToChat(string message)
        {
            PostToChatAsync(message).Wait();
        }

        public void Dispose()
        {
            Connection.Close();
        }
    }
}
