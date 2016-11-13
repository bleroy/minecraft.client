using System;
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
