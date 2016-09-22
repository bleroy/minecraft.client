using System.Threading.Tasks;

namespace Minecraft.Client
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
    }
}
