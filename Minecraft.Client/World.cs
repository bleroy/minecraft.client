using System;
using System.Threading.Tasks;
using static Decent.Minecraft.Client.Block;

namespace Decent.Minecraft.Client
{
    public class World : IDisposable
    {
        internal World(Connection connection)
        {
            Connection = connection;
            Player = new Entity(Entity.EntityType.ThePlayer, connection, "player");
        }

        private Connection Connection { get; }
        public Entity Player { get; }

        public static World Connect(string address = "localhost", int port = 4711)
        {
            var connection = new Connection(address, port);
            var world = new World(connection);
            connection.Open();
            return world;
        }

        public async Task<BlockType> GetBlockTypeAsync(int x, int y, int z)
        {
            return (BlockType)int.Parse(await Connection.SendAndReceiveAsync("world.getBlock", x, y, z));
        }

        public BlockType GetBlockType(int x, int y, int z)
        {
            return GetBlockTypeAsync(x, y, z).Result;
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
