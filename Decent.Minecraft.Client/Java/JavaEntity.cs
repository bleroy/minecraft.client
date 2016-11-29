using System;
using System.Numerics;
using System.Threading;
using System.Threading.Tasks;

namespace Decent.Minecraft.Client.Java
{
    public partial class JavaEntity : IEntity
    {
        internal JavaEntity(EntityType type, IConnection connection, string prefix = "entity", int? id = null)
        {
            Id = id;
            Type = type;
            Connection = connection;
            Prefix = prefix;
        }

        public int? Id { get; }
        public EntityType Type { get; }
        protected IConnection Connection { get; }
        protected string Prefix { get; }

        public async Task<Vector3> GetPositionAsync()
        {
            var response = await Connection.SendAndReceiveAsync(Prefix + ".getPos", Id);
            return response.ParseCoordinates();
        }

        public Vector3 GetPosition()
        {
            return GetPositionAsync().Result;
        }

        public async Task<Vector3> GetTilePositionAsync()
        {
            var response = await Connection.SendAndReceiveAsync(Prefix + ".getTile", Id);
            return Util.ParseCoordinates(response);
        }

        public Vector3 GetTilePosition()
        {
            return GetTilePositionAsync().Result;
        }

        public async Task<Vector3> GetDirectionAsync()
        {
            var response = await Connection.SendAndReceiveAsync(Prefix + ".getDirection", Id);
            return response.ParseCoordinates();
        }

        public Vector3 GetDirection()
        {
            return GetDirectionAsync().Result;
        }

        public async Task<Vector3> SetPositionAsync(Vector3 to)
        {
            await Connection.SendAsync(Prefix + ".setPos",
                Id,
                (int)Math.Floor(to.X),
                (int)Math.Floor(to.Y),
                (int)Math.Floor(to.Z)
                );
            return await GetPositionAsync();
        }
        public Vector3 SetPosition(Vector3 to)
        {
            return SetPositionAsync(to).Result;
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

        public Vector3 Move(Direction towards, int steps = 1)
        {
            return MoveAsync(towards, steps).Result;
        }

        private EventHandler<MoveEventArgs> _moved;
        private CancellationTokenSource _movedCancellationTokenSource;
        public int MovePollingInterval { get; set; } = 100;
        private Vector3 _previousPosition;

        public event EventHandler<MoveEventArgs> Moved
        {
            add
            {
                if (_moved == null)
                {
                    _movedCancellationTokenSource = new CancellationTokenSource();

                    StartListeningToMoves(_movedCancellationTokenSource.Token);
                }
                _moved += value;
            }
            remove
            {
                _moved -= value;
                if (_moved == null)
                {
                    _movedCancellationTokenSource.Cancel();
                }
            }
        }

        private async void StartListeningToMoves(CancellationToken cancellationToken)
        {
            _previousPosition = await GetTilePositionAsync();
            // Then start polling
            while (!cancellationToken.IsCancellationRequested)
            {
                var newPosition = await GetTilePositionAsync();
                if (_previousPosition != newPosition)
                {
                    _moved?.Invoke(this, new MoveEventArgs(_previousPosition, newPosition, Id));
                    _previousPosition = newPosition;
                }
                await Task.Delay(MovePollingInterval);
            }
        }
    }
}
