using System.Linq;
using System.Numerics;
using System.Threading.Tasks;

namespace Decent.Minecraft.Client
{
    public partial class Entity
    {
        internal Entity(EntityType type, Connection connection, string prefix = "entity")
        {
            Type = type;
            Connection = connection;
            Prefix = prefix;
        }

        public EntityType Type { get; }
        protected Connection Connection { get; }
        protected string Prefix { get; }

        public async Task<Vector3> GetPositionAsync()
        {
            var response = await Connection.SendAndReceiveAsync(Prefix + ".getPos");
            var coordinates = response.Split(',').Select(float.Parse).ToList();
            return new Vector3(coordinates[0], coordinates[1], coordinates[2]);
        }

        public Vector3 GetPosition()
        {
            return GetPositionAsync().Result;
        }
    }
}
