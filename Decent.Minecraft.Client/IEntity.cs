using System.Numerics;
using System.Threading.Tasks;

namespace Decent.Minecraft.Client
{
    public interface IEntity
    {
        EntityType Type { get; }

        Vector3 GetDirection();
        Vector3 GetPosition();
        Task<Vector3> GetPositionAsync();
        Vector3 GetTilePosition();
        Task<Vector3> GetTilePositionAsync();
        Vector3 Move(Direction towards, int steps = 1);
        Task<Vector3> MoveAsync(Direction towards, int steps = 1);
        Vector3 SetPosition(Vector3 to);
        Task<Vector3> SetPositionAsync(Vector3 to);
    }
}