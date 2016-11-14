using System.Numerics;
using System.Threading.Tasks;

namespace Decent.Minecraft.Client
{
    // TODO: have an abstraction for this, as this really is JavaEntity at the moment.
    public partial class Entity
    {
        internal Entity(EntityType type, IConnection connection, string prefix = "entity")
        {
            Type = type;
            Connection = connection;
            Prefix = prefix;
        }

        public EntityType Type { get; }
        protected IConnection Connection { get; }
        protected string Prefix { get; }

        public async Task<Vector3> GetPositionAsync()
        {
            var response = await Connection.SendAndReceiveAsync(Prefix + ".getPos");
            return Util.ParseCoordinates(response);
        }

        public Vector3 GetPosition()
        {
            return GetPositionAsync().Result;
        }

        public async Task<Vector3> GetTilePositionAsync()
        {
            var response = await Connection.SendAndReceiveAsync(Prefix + ".getTile");
            return Util.ParseCoordinates(response);
        }

        public Vector3 GetTilePosition()
        {
            return GetTilePositionAsync().Result;
        }
    }
}
