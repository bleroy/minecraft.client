using System.Threading.Tasks;
using static Decent.Minecraft.Client.Block;

namespace Decent.Minecraft.Client
{
    public class World
    {
        public World(Connection connection)
        {
            Connection = connection;
            Player = new Entity(Entity.EntityType.ThePlayer, connection, "player");
        }

        private Connection Connection { get; }
        public Entity Player { get; }

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
    }
}
