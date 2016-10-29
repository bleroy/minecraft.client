using System.Threading.Tasks;

namespace Decent.Minecraft.Client
{
    public class World
    {
        private Connection _connection;

        public World(Connection connection)
        {
            _connection = connection;
        }

        public async Task<BlockType> GetBlockTypeAsync(int x, int y, int z)
        {
            return (BlockType)int.Parse(await _connection.SendAndReceiveAsync("world.getBlock", x, y, z));
        }

        public BlockType GetBlockType(int x, int y, int z)
        {
            return GetBlockTypeAsync(x, y, z).Result;
        }

        public async Task PostToChatAsync(string message)
        {
            await _connection.SendAsync("chat.post", message.Replace('\r', ' ').Replace('\n', ' '));
        }

        public void PostToChat(string message)
        {
            PostToChatAsync(message).Wait();
        }
    }
}
