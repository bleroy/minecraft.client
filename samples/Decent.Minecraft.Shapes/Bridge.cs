using Decent.Minecraft.Client;
using Decent.Minecraft.Client.Blocks;
using System;
using System.Collections.Generic;
using System.Numerics;

namespace Decent.Minecraft.Shapes
{
    /// <summary>
    /// Maintains a bridge under an entity.
    /// </summary>
    public class Bridge : IDisposable
    {
        private Queue<Tuple<Vector3, IBlock>> _bridge;

        /// <summary>
        /// The length of the bridge.
        /// </summary>
        public int Length { get; }
        /// <summary>
        /// The entity to follow.
        /// </summary>
        public IEntity Entity { get; }
        /// <summary>
        /// The world.
        /// </summary>
        public IWorld World { get; }

        public Bridge(IWorld world, IEntity entity, int length = 10)
        {
            World = world;
            Entity = entity;
            Length = length;
            _bridge = new Queue<Tuple<Vector3, IBlock>>(Length);
            _moved = (object sender, MoveEventArgs args) =>
            {
                var position = args.NewPosition.Downwards();
                var belowBlock = World.GetBlock(position);
                if (belowBlock is Air || belowBlock is Water)
                {
                    _bridge.Enqueue(new Tuple<Vector3, IBlock>(position, belowBlock));
                    World.SetBlock(new StainedGlass(Color.Blue), position);
                    if (_bridge.Count > Length)
                    {
                        var posAndBlock = _bridge.Dequeue();
                        if (!_bridge.Contains(posAndBlock))
                        {
                            World.SetBlock(posAndBlock.Item2, posAndBlock.Item1);
                        }
                    }
                }
            };
            Entity.Moved += _moved;
        }

        public void Dispose()
        {
            Entity.Moved -= _moved;
        }

        private EventHandler<MoveEventArgs> _moved;
    }
}
