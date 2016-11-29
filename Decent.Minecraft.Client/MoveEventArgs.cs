using System;
using System.Numerics;

namespace Decent.Minecraft.Client
{
    /// <summary>
    /// Information about an entity move: what entity moved,
    /// the previous position of the entity, and its new position.
    /// </summary>
    public class MoveEventArgs : EventArgs
    {
        public MoveEventArgs(Vector3 previousPosition, Vector3 newPosition, int? entityId = null) : base()
        {
            EntityId = entityId;
            PreviousPosition = previousPosition;
            NewPosition = newPosition;
        }

        /// <summary>
        /// The id of the entity that moved.
        /// </summary>
        public int? EntityId { get; }
        
        /// <summary>
        /// The position of the entity before it moved.
        /// </summary>
        public Vector3 PreviousPosition { get; }

        /// <summary>
        /// The position of the entity after it moved.
        /// </summary>
        public Vector3 NewPosition { get; }
    }
}
