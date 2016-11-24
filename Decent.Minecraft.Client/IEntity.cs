using System.Numerics;
using System.Threading.Tasks;

namespace Decent.Minecraft.Client
{
    /// <summary>
    /// The contract for a Minecraft entity.
    /// </summary>
    public interface IEntity
    {
        /// <summary>
        /// The Minecraft entity type.
        /// </summary>
        EntityType Type { get; }

        /// <summary>
        /// Gets the direction the entity is facing.
        /// </summary>
        /// <returns>The direction the entity is facing.</returns>
        Vector3 GetDirection();

        /// <summary>
        /// Asynchronously gets the direction the entity is facing.
        /// </summary>
        /// <returns>The direction the entity is facing.</returns>
        Vector3 GetDirectionAsync();

        /// <summary>
        /// Gets the position of the entity.
        /// </summary>
        /// <returns>The position of the entity.</returns>
        Vector3 GetPosition();

        /// <summary>
        /// Asynchronously gets the position of the entity.
        /// </summary>
        /// <returns>The position of the entity.</returns>
        Task<Vector3> GetPositionAsync();

        /// <summary>
        /// Gets the position of the tile under the entity.
        /// </summary>
        /// <returns>The position of the tile under the entity.</returns>
        Vector3 GetTilePosition();

        /// <summary>
        /// Asynchronously gets the position of the tile under the entity.
        /// </summary>
        /// <returns>The position of the tile under the entity.</returns>
        Task<Vector3> GetTilePositionAsync();

        /// <summary>
        /// Moves the entity a number of steps in a given direction.
        /// </summary>
        /// <param name="towards">The direction in which to move the entity.</param>
        /// <param name="steps">The number of steps.</param>
        /// <returns>The new position of the entity.</returns>
        Vector3 Move(Direction towards, int steps = 1);

        /// <summary>
        /// Asynchronously moves the entity a number of steps in a given direction.
        /// </summary>
        /// <param name="towards">The direction in which to move the entity.</param>
        /// <param name="steps">The number of steps.</param>
        /// <returns>The new position of the entity.</returns>
        Task<Vector3> MoveAsync(Direction towards, int steps = 1);

        /// <summary>
        /// Moves the entity to a specific position.
        /// </summary>
        /// <param name="to">The position where to move the entity.</param>
        /// <returns>The new position of the entity.</returns>
        Vector3 SetPosition(Vector3 to);

        /// <summary>
        /// Asynchronously moves the entity to a specific position.
        /// </summary>
        /// <param name="to">The position where to move the entity.</param>
        /// <returns>The new position of the entity.</returns>
        Task<Vector3> SetPositionAsync(Vector3 to);
    }
}