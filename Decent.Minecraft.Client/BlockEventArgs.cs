using System;
using System.Numerics;

namespace Decent.Minecraft.Client
{
    /// <summary>
    /// Information about a block hit: what entity hit the block,
    /// the position of the block, and the direction the hit came from.
    /// </summary>
    public class BlockEventArgs : EventArgs
    {
        public BlockEventArgs(int entityId, Vector3 position, Direction3 facing) : base()
        {
            EntityId = entityId;
            Position = position;
            Facing = facing;
        }

        /// <summary>
        /// The id of the entity that hit the block.
        /// </summary>
        public int EntityId { get; }
        
        /// <summary>
        /// The position of the block that's been hit.
        /// </summary>
        public Vector3 Position { get; }

        /// <summary>
        /// The direction the hit came from, as seen from the perspective of the block.
        /// For example, if the player hits the block he's standing on, the direction
        /// will be `Facing.Up`.
        /// </summary>
        public Direction3 Facing { get; }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            var args = obj as BlockEventArgs;
            if (args == null) return false;
            return (args.EntityId == EntityId)
                && (args.Facing == Facing)
                && (args.Position == Position);
        }

        public override int GetHashCode()
        {
            return new { EntityId, Facing, Position }.GetHashCode();
        }
    }
}
