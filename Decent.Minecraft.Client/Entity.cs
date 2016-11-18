using System;
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
            return response.ParseCoordinates();
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
		
        public Vector3 GetDirection()
        {
            var response = Connection.SendAndReceiveAsync(Prefix + ".getDirection").Result;
            return response.ParseCoordinates();
        }

        public async Task<Vector3> SetPositionAsync(Vector3 to)
        {
            await Connection.SendAsync(Prefix + ".setPos",
                (int)Math.Floor(to.X),
                (int)Math.Floor(to.Y),
                (int)Math.Floor(to.Z)
                );
            return await GetPositionAsync();
        }

        public Vector3 GetTilePosition()
        {
            return GetTilePositionAsync().Result;
        }
        public Vector3 SetPosition(Vector3 to)
        {
            return SetPositionAsync(to).Result;
        }

        public Vector3 Move(Direction towards, int steps = 1)
        {
            return MoveAsync(towards, steps).Result;
        }

        public async Task<Vector3> MoveAsync(Direction towards, int steps = 1)
        {
            var position = await GetPositionAsync();
            switch (towards)
            {
                case Direction.North:
                    position.Z -= steps;
                    break;
                case Direction.South:
                    position.Z += steps;
                    break;
                case Direction.West:
                    position.X -= steps;
                    break;
                case Direction.East:
                    position.X += steps;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(towards), towards, null);
            }
            return await SetPositionAsync(position);
        }

       

    }
}
