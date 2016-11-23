using System;
using System.Numerics;

namespace Decent.Minecraft.Client
{
    public class BlockEventArgs : EventArgs
    {
        public BlockEventArgs(int entityId, Vector3 position, Facing facing) : base()
        {
            EntityId = entityId;
            Position = position;
            Facing = facing;
        }

        public int EntityId { get; }
        public Vector3 Position { get; }
        public Facing Facing { get; }

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
